﻿using Humanizer;
using Sofomo.Shared.Abstraction.Exceptions;
using System.Collections.Concurrent;
using System.Net;

namespace Sofomo.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception) =>
            exception switch
            {
                SofomoException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message)), HttpStatusCode.BadRequest),
                _ => new ExceptionResponse(new ErrorsResponse(new Error("Error", "There was an error.")), HttpStatusCode.InternalServerError)
            };

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();

            return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
        }
    }
}