namespace MyShop.Domain.Lazy;

public interface IValueHolder<T>
{
  T GetValue(object parameter);
}

public class ValueHolder<T>(Func<object, T> getValueDelegate) : IValueHolder<T>
{
  private T? _value;
  private readonly Func<object, T> _getValueDelegate = getValueDelegate;

  public T GetValue(object parameter)
  {
    return _value ??= _getValueDelegate(parameter);
  }
}
