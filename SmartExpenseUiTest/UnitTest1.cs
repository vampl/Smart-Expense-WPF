
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using Window = System.Windows.Window;

namespace SmartExpenseUiTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoginTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var loginTxtBox = mainWindow
            .FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("LoginTextBox")))
            ?.AsTextBox();
        var passwordTxtBox = mainWindow.FindFirstDescendant(cf =>
            cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("PasswordTextBox")))?.AsTextBox();
        var loginBtn = mainWindow
            .FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("LoginButton")))
            ?.AsButton();

        loginTxtBox?.Enter("vampl");
        passwordTxtBox?.Enter("password");

        loginBtn?.Invoke();

        // mainWindow = application.GetMainWindow(automation);

        var button = mainWindow.FindFirstDescendant(cf =>
            cf.ByControlType(ControlType.RadioButton).And(cf.ByAutomationId("MainButton")))?.AsButton();

        application.Kill();
        application.Dispose();
        automation.Dispose();

        if (button == null)
            Assert.Fail("Не відбулось входу в систему.");

        Assert.Pass("Відбувся вхід в систему.");
    }

    [Test]
    public void MenuTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var menuButtonNames = new List<string> { "MainButton", "TransactionButton", "AccountButton" };
        var contentGridNames = new List<string>
            { "HTransactionDataGrid", "TTransactionDataGrid", "AccountDataGrid" };
        var isExist = new List<bool> { false, false, false };

        for (var index = 0; index < menuButtonNames.Count; index++)
        {
            var button = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(menuButtonNames[index]))
                ?.AsRadioButton();
            button?.Click();
            application.WaitWhileMainHandleIsMissing();
            var content = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(contentGridNames[index]))?.AsGrid();

            if (button != null && content != null)
                isExist[index] = true;
        }

        application.Kill();
        application.Dispose();
        automation.Dispose();

        if (isExist.Contains(false))
            Assert.Fail($"Сторінка {contentGridNames[isExist.IndexOf(false)]} - не завантажилась");

        Assert.Pass("Усі сторінки завантажились!");
    }

    [Test]
    public void CloseTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var closeBtn = mainWindow
            .FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("CloseButton")))
            ?.AsButton();

        closeBtn?.Invoke();

        var anyButton = mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.RadioButton))?.AsButton();

        application.Kill();
        application.Dispose();
        automation.Dispose();

        if (anyButton == null)
            Assert.Pass("Додаток закрився.");

        Assert.Fail("Додаток не закрився");
    }

    [Test]
    public void MinimizeTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var minimizeButton = mainWindow
            .FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("MinimizeButton")))
            ?.AsButton();

        minimizeButton?.Invoke();

        // application.Kill();
        // application.Dispose();
        // automation.Dispose();
        
        if (mainWindow.IsAvailable)
        {
             Assert.Pass("Додаток згорнувся.");
        }
        Assert.Fail("Додаток не згорнувся");
    }
    
    [Test]
    public void MaximizeTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var height = mainWindow.ActualHeight;
        var width = mainWindow.ActualWidth;
        
        Mouse.MoveTo(100, 100);
        mainWindow.Move(100, 100);
        Mouse.DoubleClick();
        
        var newHeight = mainWindow.ActualHeight;
        var newWidth = mainWindow.ActualWidth;

        if (Math.Abs(height - newHeight) > 10 & Math.Abs(width - newWidth) > 10)
        {
            Assert.Fail("Додаток збільшився.");
        }
        Assert.Pass("Додаток не збільшився.");
    }

    [STAThread]
    [Test]
    public void MoveWindowTest()
    {
        var application = Application.Launch(@"SmartExpense.exe");
        var automation = new UIA3Automation();
        var mainWindow = application.GetMainWindow(automation);

        var startPoint = mainWindow.GetClickablePoint();
        Mouse.Position = startPoint;
        mainWindow.Focus();
        application.WaitWhileBusy(new TimeSpan(0, 0, 0, 15));
        var endPoint = new Point(600, 450);
        Mouse.Drag(startPoint, endPoint);
        
        mainWindow.Focus();
        var currentPosition = mainWindow.GetClickablePoint();
        application.WaitWhileBusy(new TimeSpan(0, 0, 0, 15));

        application.Kill();
        application.Dispose();
        automation.Dispose();

        if (currentPosition != startPoint)
        {
            Assert.Pass("Додаток перемістився.");
        }
        Assert.Fail("Додаток не перемістився.");
    }
}