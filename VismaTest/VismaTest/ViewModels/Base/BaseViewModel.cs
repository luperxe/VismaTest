using VismaTest.Languages;
using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace VismaTest.ViewModels.Base
{
    public class BaseViewModel : BindableObject
    {
        #region Vars
        protected INavigationService navigationService;
        protected IHttpService webClient;
        protected IVTService vtService;
        public INavigation Navigation;
        protected ICommand backCommand;
        protected bool isBusy;
        protected string title;
        protected DateTime timeTranscurido;
        #endregion

        #region Init
        public BaseViewModel(INavigationService navigationService, IHttpService webClient, IVTService vtService)
        {
            this.navigationService = navigationService;
            this.webClient = webClient;
            this.vtService = vtService;
            this.timeTranscurido = DateTime.Now;
        }

        /// <summary>
        /// Evento lanzando justo antes de que el Page sea bindeado "page.BindingContext = viewModel"
        /// Corresponde a OnCreate en Android y ViewDidLoad en iOS
        /// </summary>
        public virtual  Task FormsViewDidLoadAsync(object parameters)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// Evento lanzando justo después de que el Page sea bindeado "page.BindingContext = viewModel"
        /// Corresponde a OnStart/OnResume en Android y ViewWillAppear en iOS
        /// </summary>
        public virtual Task FormsViewDidAppear(object parameters)
        {

            return Task.FromResult(false);
        }

        /// <summary>
        /// Método invocado con el evento "appearing" de Xamarin Forms Page. Equivaldría a un
        /// posterior OnResume (Android) o ViewDidAppear (iOS)
        /// </summary>
        public virtual Task FormsViewWillAppear(object navigationData)
        {

            return Task.FromResult(false);
        }

        /// <summary>
        /// Evento lanzando antes de salir de la pantalla
        /// </summary>
        public virtual Task FormsViewDisappearing()
        {
            TimeSpan resta = DateTime.Now - timeTranscurido;
            return Task.FromResult(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicador si la aplicación está esperando el servicio
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Titulo de la pantalla que se muestra en la parte superior de la aplicación
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Obtiene el texto desde recursos para la key pasada (index).
        /// </summary>
        /// <param name="index">La "KEY" del texto de recursos que queramos obtener.</param>
        /// <returns>El texto traducido a la cultura establecida.</returns>
        public string this[string index]
        {
            get
            {

                return LocalizableStrings.ResourceManager.GetString(index);
            }
        }
        #endregion


        #region Other

        public IVTService GetvtService()
        {
            return this.vtService;
        }

        #endregion
    }
}
