using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Ui.Views;

public partial class MainWindow : Window
{
    private Regex m_numberRegex = new(@"[^0-9]+");

    public MainWindow()
    {
        InitializeComponent();
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = m_numberRegex.IsMatch(e.Text);
    }
}