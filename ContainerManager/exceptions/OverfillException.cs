namespace ContainerManager.exceptions;

public class OverfillException : Exception
{
    public OverfillException(string message)
        : base(message)
    {
    }
}