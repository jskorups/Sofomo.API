namespace Sofomo.Shared.Abstraction.Exceptions;

public record ErrorsResponse(params Error[] Errors);

public record Error(string Code, string Message);
