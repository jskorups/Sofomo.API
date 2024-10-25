using System.Net;

namespace Sofomo.Shared.Abstraction.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
