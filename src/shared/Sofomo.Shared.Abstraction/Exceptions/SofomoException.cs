namespace Sofomo.Shared.Abstraction.Exceptions;

public class SofomoException : Exception
{
    public SofomoException(string message) : base(message)
    {
    }
}