public abstract class MaybeWithError<ValueT, ErrorT>
{
    public MaybeWithError<ValueT2, ErrorT> Bind<ValueT2>(
        System.Func<ValueT,
        MaybeWithError<ValueT2, ErrorT>> func) => this switch
        {
            ValueWrapper<ValueT, ErrorT> valueWrapper => func(valueWrapper.Value),
            ErrorWrapper<ValueT, ErrorT> errorWrapper =>
                new ErrorWrapper<ValueT2, ErrorT>(errorWrapper.Error),
            _ => throw new System.Exception("Unreachable")
        };

    public abstract void CallIfHasValue(System.Action<ValueT> action);
}

public class ValueWrapper<ValueT, ErrorT> : MaybeWithError<ValueT, ErrorT>
{
    public ValueWrapper(ValueT value) => Value = value;
    public ValueT Value { get; }

    public override void CallIfHasValue(System.Action<ValueT> action) =>
        action(Value);
}

public class ErrorWrapper<ValueT, ErrorT> : MaybeWithError<ValueT, ErrorT>
{
    public ErrorWrapper(ErrorT error) => Error = error;
    public ErrorT Error { get; }

    public override void CallIfHasValue(System.Action<ValueT> action) { }
}