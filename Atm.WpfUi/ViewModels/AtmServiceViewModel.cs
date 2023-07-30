using System.Collections.ObjectModel;
using System.Linq;
using Atm.AtmModel.Implementation;
using Atm.AtmModel.Services;

namespace Atm.WpfUi.ViewModels;

public class AtmServiceViewModel : ViewModelBase
{
    public AtmServiceViewModel(AtmController atm)
    {
        Moneys = new ObservableCollection<MoneyStack>();
        _atm = atm;
        _atm.PropertyChanged += (sender, arg) =>
        {
            var name = arg.PropertyName;
            if (name != nameof(_atm.Moneys)) return;
            Moneys.Clear();
            foreach (var money in _atm.Moneys)
            {
                Moneys.Add(money);
            }

            Amount = Moneys.Sum(stack => stack.Denomination * stack.Quantity);
        };

        foreach (var money in _atm.Moneys)
        {
            Moneys.Add(money);
        }
        Amount = Moneys.Sum(stack => stack.Denomination * stack.Quantity);
    }

    public ObservableCollection<MoneyStack> Moneys { get; set; }
    public decimal Amount { get; set; }

    private AtmController _atm;
}