using System.Linq;
using System.Windows;
using SmartExpense.Core;
using SmartExpense.MVVM.Model;
using SmartExpense.MVVM.View;

namespace SmartExpense.MVVM.ViewModel;

public class LoginViewModel : ObservableObject
{
    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string _password;

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    // Команда для виклику операції входу.
    private RelayCommand? _loginCommand;

    public RelayCommand LoginCommand =>
        _loginCommand ??= new RelayCommand(LoginAction);

    private void LoginAction(object parameter)
    {
        var currentUser = new UserModel
        {
            Username = Username,
            Password = Password
        };
        var applicationContext = new ApplicationContext();
        var canLogin = false;
        
        foreach (var user in applicationContext.Users)
        {
            if (user.Username == currentUser.Username && user.Password == currentUser.Password)
            {
                canLogin = true;
                currentUser = user;
                break;
            }
        }

        if (canLogin)
        {
            ApplicationContext.SetUser(currentUser);
        }
        else
        {
            var result = MessageBox.Show("Такого користувача не існує, бажаєте зареєструватись?", "Помилка входу",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                applicationContext.Users.Add(currentUser);
                ApplicationContext.SetUser(currentUser);
                applicationContext.SaveChanges();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }

    public LoginViewModel()
    {
    }
}