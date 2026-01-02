namespace TagsCloudContainer;

public struct Result<T>
{
    public T Value { get; }
    public string ErrorMessage { get; }
    public bool IsSuccess => ErrorMessage == null;
    
    public Result(T value, string? errorMessage)
    {
        Value = value;
        ErrorMessage = errorMessage;
    }
    public static Result<T> Success(T value) => new(value, null);
    public static Result<T> Failure(string errorMessage) => new(default, errorMessage);
    
    public Result<TNext> Then<TNext>(Func<T, Result<TNext>> nextStep)
    {
        return IsSuccess ? nextStep(Value) : Result<TNext>.Failure(ErrorMessage);
    }
}