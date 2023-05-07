using System;
using System.Windows;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.View;

public partial class TransactionFormView : Window
{
    private Action<TransactionModel> _callback;
    
    public TransactionFormView(Action<TransactionModel> callback)
    {
        InitializeComponent();
        _callback = callback;
    }

    private void AddRowButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (isCorrectData())
        {
            _callback(new TransactionModel
            {
                Amount = Convert.ToDecimal(AmountTextBox.Text),
                Type = TypeComboBox.SelectionBoxItem.ToString()!,
                CreationDate = Convert.ToDateTime(DatePicker.Text),
                Description = DiscriptionTextBox.Text,
                AccountTitle = AccountsComboBox.SelectionBoxItem.ToString()!
            });
            
            Close();
        }
        else {
            MessageBox.Show("Перевірте введені дані!");
        }
    }

    private bool isCorrectData()
    {
        return Decimal.TryParse(AmountTextBox.Text, out var amount);
    }
}