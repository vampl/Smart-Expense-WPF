using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace SmartExpense.MVVM.View;

public partial class TransactionFormView : Window
{
    public TransactionFormView()
    {
        InitializeComponent();
        var ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
        ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        Thread.CurrentThread.CurrentCulture = ci;
        
        DatePicker.DisplayDate = DateTime.Now;
    }
}