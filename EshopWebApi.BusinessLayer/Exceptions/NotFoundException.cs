using System;

namespace EshopWebApi.BusinessLayer.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with specified error message
        /// </summary>
        /// <param name="message">Error message</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class
        /// </summary>
        public NotFoundException() : base("Record not found")
        {
        }
    }
}
