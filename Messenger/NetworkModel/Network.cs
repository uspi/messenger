using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{  
    /// <summary>
    /// Интерфейс работы с сервером
    /// </summary>
    class Network
    {
        Connection connection;

        public Connection Connection { get => connection; private set => connection = value; }

        //начало соединения, подключение и отправка пароля и логина
        public void ConnectionStart(string host = "localhost", int port = 11000)
        {
            //запись параметров хоста
            connection = new Connection(host, port);

            //создание сокета обмена
            try { connection.Connect(); }

            //отправка сообщения в вызывающую функцию
            catch (Exception e) { throw e; }
        }

        //закрытие канала tcp/ip
        public void ConnectionStop()
        {
            connection.Close();
        }
        //отправка сообщения
        public void ConnectionSend(string message)
        {
            connection.Send(message);
        }
        //получение сообщения
        public string ConnectionReceive()
        {
            return connection.Receive(1024, 0);
        }

        //static void Start()
        //{             
        //    List<string> logReceive = new List<string>();//history receive

        //    //обмен сообщениями между сервером и клиентом
        //    Messaging(client, logReceive); 
        //}

        //первое сообщению серверу


        //обмен сообщениями между сервером и клиентом
        //static void Messaging(Connection client, List<string> logReceive)
        //{
        //    while (true)
        //    {
        //        string lastReceive = null;
        //        Console.WriteLine("...");
        //        Console.Write("Cообщение: ");
        //        client.Send(Console.ReadLine());

        //        Console.WriteLine("...");
        //        Console.WriteLine(lastReceive = "Server: " + client.Receive(1024, 0));//Receive
        //        logReceive.Add(lastReceive);
        //    }
        //}
    }
}
