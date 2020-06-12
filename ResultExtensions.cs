using System;
using MonadicTypes;

namespace ReactiveExtensions
{
    public static class ResultExtensions
    {
        public static Result<T,TError> ToResult<T, TError>(this Maybe<T> maybe, TError errorMessage) where T : class where TError : class
        {
            if (maybe.HasNoValue)
                return Result<T,TError>.Fail<T>(errorMessage);

            return Result<T, TError>.Ok(maybe.Value);
        }

        public static Result<TOutput, TError> OnSuccess<TInput, TError, TOutput>(this Result<TInput, TError> result, Func<TInput, TOutput> func) where TError : class
        {
            if (result.IsFailure)
                return Result<TOutput, TError>.Fail<TOutput>(result.Error);

            return Result<TInput, TError>.Ok(func(result.Value));
        }

        public static Result<T, TError> Ensure<T, TError>(this Result<T, TError> result, Func<T, bool> predicate, TError errorMessage) where TError : class
        {
            if (result.IsFailure)
                return result;

            if (!predicate(result.Value))
                return Result<T, TError>.Fail<T>(errorMessage);

            return result;
        }

        public static Result<K, TError> Map<T, TError, K>(this Result<T, TError> result, Func<T, K> func) where TError : class
        {
            if (result.IsFailure)
                return Result<K, TError>.Fail<K>(result.Error);

            return Result<K, TError>.Ok(func(result.Value));
        }

        public static Result<T, TError> OnSuccess<T, TError>(this Result<T, TError> result, Action<T> action) where TError : class
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }

            return result;
        }

        public static T OnBoth<T, TError>(this Result<TError> result, Func<Result<TError>, T> func) where TError : class
        {
            return func(result);
        }

        public static Result<TError> OnSuccess<TError>(this Result<TError> result, Action action) where TError : class
        {
            if (result.IsSuccess)
            {
                action();
            }

            return result;
        }
    }
}
