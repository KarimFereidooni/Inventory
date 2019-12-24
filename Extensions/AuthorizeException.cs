namespace Inventory.Extensions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AuthorizeException : Exception
    {
        public AuthorizeException(string message)
            : base(message)
        {
        }

        public AuthorizeException()
        {
        }

        public AuthorizeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AuthorizeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
