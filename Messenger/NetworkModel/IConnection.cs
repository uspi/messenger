
namespace Messenger
{
    interface IConnection
    {
        void Send(string reply);
        string Receive(int quantityByte, int indexFirstByte);
        void Close();
    }
}
