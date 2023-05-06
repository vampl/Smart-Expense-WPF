using System.Windows;

namespace SmartExpense.MVVM.View;

public partial class TransactionFormView : Window
{
    public TransactionFormView()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}