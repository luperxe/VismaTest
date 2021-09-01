using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Models.Enum
{
    public enum CustomHttpMethod
    {
        /// <summary>
        /// Requests to retrieve resource representation/information only.
        /// </summary>
        Get,

        /// <summary>
        /// Replaces all current representations of the target resource with the request payload.
        /// </summary>
        Put,

        /// <summary>
        /// Used to submit an entity to the specified resource.
        /// </summary>
        Post,

        /// <summary>
        /// Used to apply partial modifications to a resource.
        /// </summary>
        Patch,

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        Delete
    }
}
