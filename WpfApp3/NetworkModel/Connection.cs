using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WPFClient.NetworkItems
{
    /// <summary>
    /// Представительский уровень
    /// Отвечает за кодирование данных,
    /// полученных от прикладного уровня(class network) в
    /// представление, готовое к передаче по сети и наоборот.
    /// </summary>
    class Connection : IClientConnection
    {
        public IPEndPoint ipEndPoint;
        //public Socket sListener;
        public Socket handler;

        public Connection(string _host, int _port)
        {
            IPAddress iPAddress;
            //если в _host записано localhost
            if (!IPAddress.TryParse(_host, out iPAddress))
            {
                IPHostEntry ipHost = Dns.GetHostEntry(_host);
                ipEndPoint = new IPEndPoint(ipHost.AddressList[0], _port);
            }
            //если в _host записан ip
            else
            {
                ipEndPoint = new IPEndPoint(iPAddress, _port);
            }
        }      
        public void Connect()
        {
            handler = new Socket(ipEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            handler.Connect(ipEndPoint);
        }
        public void Close()
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
        public void Send(string reply)
        {
            byte[] msg = Encoding.UTF8.GetBytes(reply);
            handler.Send(msg);
        }
        public string Receive(int quantityByte, int indexFirstByte)
        {
            byte[] bytes = new byte[quantityByte];
            int bytesRec = handler.Receive(bytes);
            return Encoding.UTF8.GetString(bytes, indexFirstByte, bytesRec);
        }
    }
}
