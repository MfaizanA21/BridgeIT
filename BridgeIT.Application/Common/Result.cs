using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeIT.Application.Common.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public int? StatusCode { get; }

        private Result(bool isSuccess, T? value, string? error = null, int? code = null)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = error;
            StatusCode = code;
        }

        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Failure(string error, int statusCode = 400) => new(false, default, error, statusCode);

        public static Result<T> NotFound(string message = "Not found") => Failure(message, 404);
        public static Result<T> BadRequest(string message) => Failure(message, 400);
        public static Result<T> Conflict(string message) => Failure(message, 409);

        public readonly struct Unit { };
    }
}
