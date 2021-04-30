using Messenger.Core;

namespace Messenger.Server
{
    // delegate for sending request from user enviroment to server
    public delegate void RequestHandler(UserEnvironment sender, Request request);
}
