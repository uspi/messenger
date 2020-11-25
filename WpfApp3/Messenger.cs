using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.MessengerItems;

namespace WPFClient
{
    class Messenger
    {
        //сеть между юзером и сервером
        private Network network;
        public Network Network { get => network; private set => network = value; }

        //текущий пользователь
        User user;

        public Messenger(string _login, string _password)
        {
            user = new User(_login, _password);
            network = new Network();
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
            //отправка логина и получение подтверждения получения
            network.ConnectionSend(user.login);
            if (network.ConnectionReceive() != "receivedLogin")
            {
                return false;
            }

            //отправка пароля и получение подтверждения получения
            network.ConnectionSend(user.password);
            if (network.ConnectionReceive() != "receivedPassword")
            {
                return false;
            }

            //проверка ответа сервера
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
