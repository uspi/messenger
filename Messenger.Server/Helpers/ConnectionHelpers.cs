
namespace Messenger.Server
{
    /// <summary>
    /// Extensions for working with logical server functions
    /// </summary>
    public static class ConnectionHelpers
    {
        /// <summary>
        /// Adds a new user to the list of <see cref="Server.ServerUsers"/> 
        /// connected to the server
        /// </summary>
        /// <param name="server">Server for user connection</param>
        /// <param name="userEnvironment">The user to add</param>
        public static void AddConnection(this Server server, UserEnvironment userEnvironment)
        {
            server.ServerUsers.Add(userEnvironment);
        }

        /// <summary>
        /// Removes a user from the <see cref="Server.ServerUsers"/> of current users
        /// </summary>
        /// <param name="server">Server for user connection</param>
        /// <param name="userEnvironment">The user to add</param>
        public static void RemoveConnection(this Server server, UserEnvironment userEnvironment)
        {
            if (userEnvironment == null)
            {
                return;
            }

            server.ServerUsers.Remove(userEnvironment);
        }
    }
}
