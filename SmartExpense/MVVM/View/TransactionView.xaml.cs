using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmartExpense.Core.Database.DataModels;

namespace SmartExpense.MVVM.View;

public partial class TransactionView : UserControl
{
    public TransactionView()
    {
        InitializeComponent();
    }
    
    /* ToDo: розробити логіку наскрізного пошуку. */
    private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        
    }
}