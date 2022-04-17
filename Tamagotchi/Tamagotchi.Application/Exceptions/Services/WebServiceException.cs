using System.Runtime.Serialization;

namespace Tamagotchi.Application.Exceptions;

[Serializable]
public class WebServiceException : BaseException
{
    public WebServiceException()
    {
    }

    public WebServiceException(string message)
        : base(message)
    {
    }

    public WebServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected WebServiceException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public WebServiceException(IEnumerable<string> list)
    {
        Errors = list;
    }

    public IEnumerable<string> Errors { get; set; }

    public override string Message
    {
        get { return Errors.Aggregate(string.Empty, (current, item) => current + item + "\n"); }
    }
}