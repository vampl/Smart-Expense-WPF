using System.Data;
using System.Windows;
using System.Windows.Controls;
using SmartExpense.MVVM.ViewModel;

namespace SmartExpense.MVVM.View;

public partial class TransactionView : UserControl
{ 
    public TransactionView()
    {
        InitializeComponent();
    }

    private void AddRowButton_OnClick(object sender, RoutedEventArgs e)
    {
        new TransactionFormView().Show();
    }
}