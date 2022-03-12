using System.Runtime.Serialization;

namespace TamagotchiWeb.Exceptions;

[Serializable]
public class UnauthorizeException : Exception
{
    public UnauthorizeException()
    {
    }

    public UnauthorizeException(string message)
        : base(message)
    {
    }

    public UnauthorizeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected UnauthorizeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}