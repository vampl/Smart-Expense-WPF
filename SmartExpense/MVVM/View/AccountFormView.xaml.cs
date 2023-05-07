using System;
using System.Windows;
using SmartExpense.MVVM.Model;

namespace SmartExpense.MVVM.View;

public partial class AccountFormView : Window
{
    private Action<AccountModel> _callback;
    
    public AccountFormView(Action<AccountModel> callback)
    {
        InitializeComponent();
        _callback = callback;
    }

    private void AddRowButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (isCorrectData())
        {
            _callback(new AccountModel
            {
                Title = TitleBox.Text,
                Type = TypeComboBox.SelectionBoxItem.ToString()!,
                Description = DescriptionTextBox.Text,
                Amount = Convert.ToDecimal(AmountTextBox.Text),
            });
            
            Close();
        }
        else {
            MessageBox.Show("Перевірте введені дані!");
        }
    }
    
    private bool isCorrectData()
    {
        return decimal.TryParse(AmountTextBox.Text, out var amount);
    }
}