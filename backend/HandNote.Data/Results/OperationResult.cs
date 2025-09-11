using System;
using System.Collections.Generic;

namespace HandNote.Data.Results
{
    /// <summary>
    /// Unified result pattern for all operations across the application
    /// </summary>
    public sealed class OperationResult<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public string Message { get; }
        public IReadOnlyList<string> Errors { get; }

        private OperationResult(bool isSuccess, T? data, string message, IReadOnlyList<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message ?? string.Empty;
            Errors = errors ?? Array.Empty<string>();
        }

        #region Success Factory Methods

        public static OperationResult<T> Success(T data, string message = "Operation completed successfully")
        {
            if (data == null && !typeof(T).IsValueType && Nullable.GetUnderlyingType(typeof(T)) == null)
                throw new ArgumentNullException(nameof(data), "Data cannot be null for successful operation");

            return new OperationResult<T>(true, data, message);
        }

        public static OperationResult<T> Success(string message = "Operation completed successfully")
        {
            return new OperationResult<T>(true, default(T), message);
        }

        #endregion

        #region Failure Factory Methods

        public static OperationResult<T> Failure(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Failure message cannot be null or empty", nameof(message));

            return new OperationResult<T>(false, default(T), message);
        }

        public static OperationResult<T> Failure(string message, IEnumerable<string> errors)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Failure message cannot be null or empty", nameof(message));

            var errorList = errors?.ToArray() ?? Array.Empty<string>();
            return new OperationResult<T>(false, default(T), message, errorList);
        }

        public static OperationResult<T> Failure(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            return new OperationResult<T>(false, default(T), exception.Message);
        }

        #endregion

        #region Implicit Operators for Convenience

        public static implicit operator bool(OperationResult<T> result) => result.IsSuccess;

        public static implicit operator T?(OperationResult<T> result) => result.Data;

        #endregion

        #region Utility Methods

        /// <summary>
        /// Execute an action if the operation was successful
        /// </summary>
        public OperationResult<T> OnSuccess(Action<T> action)
        {
            if (IsSuccess && Data != null)
                action(Data);
            return this;
        }

        /// <summary>
        /// Execute an action if the operation failed
        /// </summary>
        public OperationResult<T> OnFailure(Action<string> action)
        {
            if (!IsSuccess)
                action(Message);
            return this;
        }

        /// <summary>
        /// Transform the data type while preserving result state
        /// </summary>
        public OperationResult<TNew> Map<TNew>(Func<T, TNew> mapper)
        {
            if (!IsSuccess)
                return OperationResult<TNew>.Failure(Message, Errors);

            try
            {
                var newData = mapper(Data!);
                return OperationResult<TNew>.Success(newData, Message);
            }
            catch (Exception ex)
            {
                return OperationResult<TNew>.Failure($"Mapping failed: {ex.Message}");
            }
        }

        #endregion

        public override string ToString()
        {
            var status = IsSuccess ? "Success" : "Failure";
            return $"{status}: {Message}";
        }
    }

    /// <summary>
    /// Non-generic version for operations that don't return data
    /// </summary>
    public sealed class OperationResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public IReadOnlyList<string> Errors { get; }

        private OperationResult(bool isSuccess, string message, IReadOnlyList<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Message = message ?? string.Empty;
            Errors = errors ?? Array.Empty<string>();
        }

        public static OperationResult Success(string message = "Operation completed successfully")
            => new(true, message);

        public static OperationResult Failure(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Failure message cannot be null or empty", nameof(message));
            return new(false, message);
        }

        public static OperationResult Failure(string message, IEnumerable<string> errors)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Failure message cannot be null or empty", nameof(message));

            var errorList = errors?.ToArray() ?? Array.Empty<string>();
            return new(false, message, errorList);
        }

        public static implicit operator bool(OperationResult result) => result.IsSuccess;

        public override string ToString()
        {
            var status = IsSuccess ? "Success" : "Failure";
            return $"{status}: {Message}";
        }
    }
}