using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VismaTest.Constants;
using VismaTest.Helpers;
using VismaTest.Models.GoogleAuth;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices.Interfaces;
using VismaTest.ViewModels.Base;
using Xamarin.Auth;

namespace VismaTest.ViewModels.GoogleAuth
{
    public class GoogleAuthViewModel : BaseViewModel
    {
		#region properties

		private string _welcomeText;
		public string WelcomeText
		{
			get
			{
				return this._welcomeText;
			}

			set
			{
				this._welcomeText = value;
				this.OnPropertyChanged();
			}
		}

		private string _userImage;
		public string UserImage
		{
			get
			{
				return this._userImage;
			}

			set
			{
				this._userImage = value;
				this.OnPropertyChanged();
			}
		}
		#endregion

		public GoogleAuthViewModel(INavigationService navigationService, IHttpService webClient, IVTService vtService) : base(navigationService, webClient, vtService)
        {
			
        }

        public override Task FormsViewDidAppear(object parameters)
        {
            Init();
            return base.FormsViewDidAppear(parameters);
        }

        private void Init()
        {
			
			var clientId = GoogleAuthConstants.ClientId;
			var redirectUri = GoogleAuthConstants.RedirectUrl;		

			var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				GoogleAuthConstants.Scope,
				new Uri(GoogleAuthConstants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(GoogleAuthConstants.AccessTokenUrl),
				null,
				true);

			authenticator.Completed += OnAuthCompleted;
			authenticator.Error += OnAuthError;

			AuthenticationState.Authenticator = authenticator;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);
		}

		async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			User user = null;
			if (e.IsAuthenticated)
			{
				user = await vtService.GetGoogleAuthUser(e);

				if (user != null)
				{
					WelcomeText = string.Concat("Welcome ", user.Email, " !");
					UserImage = user.Picture;
				}
			}
		}

		void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}
	}
}
