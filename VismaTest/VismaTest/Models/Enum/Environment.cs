using System;
using System.Collections.Generic;
using System.Text;

namespace VismaTest.Models.Enum
{
    /// <summary>
    /// Enumera todos los posibles entornos disponibles para la aplicación
    /// </summary>
    public enum Environment
    {
        /// <summary>
        /// Entorno de desarrollo.
        /// </summary>
        Des = 1,
        /// <summary>
        /// Entorno de integración.
        /// </summary>
        Pre = 2,
        /// <summary>
        /// Entorno de producción.
        /// </summary>
        Pro = 3,
    }
}
