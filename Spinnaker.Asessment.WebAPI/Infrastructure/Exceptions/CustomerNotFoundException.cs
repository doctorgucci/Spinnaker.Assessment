﻿namespace Spinnaker.Asessment.WebAPI.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when a customer is not found.
    /// </summary>
    public class CustomerNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class.
        /// </summary>
        public CustomerNotFoundException() : base("Customer not found.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public CustomerNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
