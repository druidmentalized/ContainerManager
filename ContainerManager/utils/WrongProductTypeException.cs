namespace ContainerManager.utils;

public class WrongProductTypeException : Exception
{
    public WrongProductTypeException(string message)
        : base(message)
    {
    }
}