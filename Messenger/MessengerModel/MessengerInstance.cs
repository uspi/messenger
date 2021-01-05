using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Messenger
{
    class MessengerInstance : INotifyPropertyChanged
    {
        //сеть между юзером и сервером
        private Network network;
        public Network Network { get => network; private set => network = value; }

        //текущий пользователь
        User user;

        public event PropertyChangedEventHandler PropertyChanged;  
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public MessengerInstance(string _login, string _password)
        {
            user = new User(_login, _password);
            network = new Network();
        }

        //empty constructor for debug, should be removed
        public MessengerInstance()
        {
        }

        //подключится к серверу и создать интерфейс работы с ним
        public void Connect()
        {
            //подключение к серверу
            try
            {  
                network.ConnectionStart();
            }
            catch(Exception e){ throw e; }            
        }

        //аутентификация пользователя у сервера
        public bool Auth()
        { 
            //отправка авторизационных данных
            network.ConnectionSend(user.Login);
            //ответ сервера
            if (network.ConnectionReceive() == "confirmedUser")
            { return true; }

            return false;
        }

        //public bool LoginCheck()
        //{
        //    return false;
        //}
        //public bool PasswordCheck()
        //{
        //    return false;
        //}
    }
}
