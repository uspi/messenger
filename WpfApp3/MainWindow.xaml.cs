using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Markup;
using System.Threading.Tasks;
using System.Xml;
using System.Threading;
using System.Xml.Linq;

namespace WPFClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {   
        public object frameworkElementDataContext
        {
            get => (FrameworkElement)base.DataContext;
            set => base.DataContext = value; 
        }

        public MainWindow()
        {    
            InitializeComponent();
            frameworkElementDataContext = new ViewModel(DataContext);
            //DataContext = new VeiwModel();
        }
        

        //everything below should be removed (needed for debug)




        ////Метод для открытия и скрытия экрана
        //private void ScreenOpen(Border screen)
        //{       
        //    //делаем все экраны невидимыми    
        //    LoginScreen.Visibility = Visibility.Hidden;
        //    ChatScreen.Visibility = Visibility.Hidden;
        //    ContactsScreen.Visibility = Visibility.Hidden;

        //    //делаем видимым необходиый экран
        //    screen.Visibility = Visibility.Visible;
        //}

        ////кнопка подтверждения ввода логина и пароля
        //private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Task.Run(() =>
        //        {
        //            //если кнопка нажата впервые
        //            if (mess == null)
        //            {
        //                mess = new Messenger(LoginBox.Text, PasswordBox.Text);
        //                mess.Connect();
        //                mess.Auth();
        //            }

        //            //если кнопка нажата повторно
        //            else
        //            {
        //                mess.Auth();
        //            }
        //        });
        //    }

        //    //открытие скрытой строки для отображения на экране ошибки
        //    catch (Exception ex)
        //    {
        //        DevLine.Visibility = Visibility.Visible;
        //        DevLine.Text = ex.ToString();
        //        return;
        //    }
        //    finally
        //    {
        //        ScreenOpen(ContactsScreen);
        //    }

        //    DevLine.Visibility = Visibility.Visible;
        //    DevLine.Text = "Работа с вашими данными";
        //}

        //private void LoginBox_Initialized(object sender, EventArgs e)
        //{
        //    //length input login
        //    LoginBox.MaxLength = 40;
        //}

        //private void PasswordBox_Initialized(object sender, EventArgs e)
        //{
        //    //length input password
        //    LoginBox.MaxLength = 40;
        //}

        //private void SendButton_Click(object sender, RoutedEventArgs e)
        //{

        //}
        //private void ContactsList_SelectionChanged(object sender, RoutedEventArgs e)
        //{

        //}   
    }
}
