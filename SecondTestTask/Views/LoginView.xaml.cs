using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace SecondTestTask.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = m_numberRegex.IsMatch(e.Text);
    }

    private Regex m_numberRegex = new(@"[^0-9]+");
}