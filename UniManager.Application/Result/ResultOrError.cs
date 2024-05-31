namespace UniManager.Application.Result
{
    public readonly struct ResultOrError<TValue>
    {
        public bool IsError { get; }
        public List<Error>? Errors { get; }
        public TValue Value { get; }

        private ResultOrError(List<Error>? errors)
        {
            IsError = true;
            Errors = errors;
            Value = default!;
        }

        private ResultOrError(TValue value)
        {
            IsError = false;
            Errors = null;
            Value = value;
        }

        public static ResultOrError<TValue> Success(TValue value)
        {
            return new ResultOrError<TValue>(value);
        }

        public static ResultOrError<TValue> Failure(List<Error>? errors)
        {
            return new ResultOrError<TValue>(errors);
        }

        public static ResultOrError<TValue> Failure(Error error)
        {
            return new ResultOrError<TValue>(new List<Error> { error });
        }

    }
}
