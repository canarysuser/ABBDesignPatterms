using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class RelayCommand : ICommand
    {
        Action<object> _execute;
        Func<object,bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object,bool> canExecute)
        {
            _execute = execute; _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        private ICommand _messageCommand; 
        public ICommand MessageCommand
        {
            get
            {
                if(_messageCommand==null)
                {
                    _messageCommand = new RelayCommand(
                        (param)=>SayHello(), 
                        (param)=>false
                    );
                }
                return _messageCommand;
            }
        }
        public void SayHello()
        {
            MessageBox.Show("Button Clicked.");
        }
    }
}
