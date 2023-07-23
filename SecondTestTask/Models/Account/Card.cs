namespace SecondTestTask.Models.Account;

public class Card
{
    public Card(ulong id)
    {
        Id = id;
    }

    public ulong Id { get; private set; }
}