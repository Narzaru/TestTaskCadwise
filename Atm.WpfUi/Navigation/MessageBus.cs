namespace Atm.WpfUi.Navigation;

public class MessageBus
{
    public void Set(object data)
    {
        _data = data;
    }

    public object Get()
    {
        return _data;
    }

    private object _data = null!;
}