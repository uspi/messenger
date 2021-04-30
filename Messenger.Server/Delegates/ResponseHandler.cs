using Messenger.Core;

namespace Messenger.Server
{
    // delegate for sending request from server to user enviroment 
    public delegate void ResponseHandler(UserEnvironment sender, Response request);
}
