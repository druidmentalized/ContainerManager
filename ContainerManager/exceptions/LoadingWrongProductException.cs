namespace ContainerManager.exceptions;

public class LoadingWrongProductException : Exception
{
    public LoadingWrongProductException(string message)
        : base(message)
    {
    }
}