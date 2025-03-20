namespace ContainerManager.exceptions;

public class WrongProductTypeException : Exception
{
    public WrongProductTypeException(string message)
        : base(message)
    {
    }
}