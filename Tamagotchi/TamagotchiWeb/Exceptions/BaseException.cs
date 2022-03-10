using System.Runtime.Serialization;

namespace TamagotchiWeb.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}