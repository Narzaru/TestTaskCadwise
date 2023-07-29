namespace Atm.AtmModel.Implementation;

public class Operation
{
    private OnAccept _onAccept;
    private OnDecline _onDecline;

    public delegate void OnAccept();

    public delegate void OnDecline();

    public Operation(OnAccept onAccept, OnDecline onDecline)
    {
        _onAccept = onAccept;
        _onDecline = onDecline;
    }

    public void Accept()
    {
        _onAccept();
    }

    public void Decline()
    {
        _onDecline();
    }
}