namespace ContainerManager.exceptions;

public class TemperatureDiscrepancyException : Exception
{
    public TemperatureDiscrepancyException(string message)
        : base(message)
    {
    }
}