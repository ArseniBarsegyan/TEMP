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

namespace CloudBox.Wpf.Menu
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : UserControl, ISwitchable
    {
        public Register()
        {
            InitializeComponent();
        }

        //Back to main page
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        //Register User if not exists
        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
