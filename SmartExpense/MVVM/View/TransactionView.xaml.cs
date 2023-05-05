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

    private void DeleteRowButton_OnClick(object sender, RoutedEventArgs e)
    {
        var row = (DataRowView)TransactionDataGrid.SelectedItem;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        TransactionDataGrid.BeginEdit();
    }
}