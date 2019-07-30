using System;

namespace MonadicTypes
{
    public class Result<TError> where TError : class
    {
        public bool IsSuccess { get; }
        public TError Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, Maybe<TError> error)
        {
            if (isSuccess && error.HasValue)
                throw new InvalidOperationException();
            if (!isSuccess && error.HasNoValue)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error.Value;
        }

        public static Result<TError> Fail(TError error)
        {
            return new Result<TError>(false, error);
        }

        public static Result<T, TError> Fail<T>(TError message)
        {
            return new Result<T, TError>(default, false, message);
        }

        public static Result<TError> Ok()
        {
            return new Result<TError>(true, null);
        }

        public static Result<T, TError> Ok<T>(T value)
        {
            return new Result<T, TError>(value, true, default);
        }

        public static Result<TError> Combine(params Result<TError>[] results)
        {
            foreach (Result<TError> result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }
    }


    public class Result<T, TError> : Result<TError> where TError : class
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, bool isSuccess, TError error)
            : base(isSuccess, error)
        {
            _value = value;
        }
    }
}

