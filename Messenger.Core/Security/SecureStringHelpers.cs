using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Messenger.Core
{
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Put the <see cref="SecureString"/> into unmanaged memory. Using the pointer <see cref="IntPtr"/>
        /// we read it into the managed memory, namely in the <see cref="string"/>
        /// </summary>
        /// <returns>Unsecure managed <see cref="string"/></returns>
        public static string Unsecure(this SecureString secureString)
        // extension method: secure string to string
        {
            // check for availability
            if (secureString == null) { return string.Empty; }

            // pointer unsecure string in memory
            var unmanagedString = IntPtr.Zero;
            try
            {
                // unsecure and put it into unmanaged memory
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                // copy in managed string
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
