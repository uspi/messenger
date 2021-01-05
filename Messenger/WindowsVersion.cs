using System.Runtime.InteropServices;

namespace Messenger
{
    //the basis for the following code was taken from here
    //https://github.com/dotnet/corefx/tree/a75d30306975040b0e22390d04a3b3de094d1817/src/Common/src/Interop/Windows/NtDll
    //and 
    //https://www.codeproject.com/Articles/678606/Part1-Overcoming-Windows-8-1s-deprecation-of-GetVe#xx5080848xx
    /// <summary>
    /// Returns information about the version of the current Windows operating system. 
    /// Operating systems before and after Windows 10 supported. 
    /// Information is extracted directly from PEB(Process Environment Block) without switching to kernel mode.
    /// </summary>
    class WindowsVersion
    {
        internal RTL_OSVERSIONINFOEX WindowsVersionInfo { get; }
        
        private int RtlGetVersionRequestStatus { get; set; }

        public int MajorVersion { get => (int)WindowsVersionInfo.dwMajorVersion; }
        public int MinorVersion { get => (int)WindowsVersionInfo.dwMinorVersion; }
        public float Version { get => MajorVersion + MinorVersion/10; }

        public WindowsVersion()
        {
            WindowsVersionInfo = RtlGetVersion();
        }

        // members according to OSVERSIONINFOEXA structure (winnt.h)
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct RTL_OSVERSIONINFOEX
        {
            // uint same DWORD in win32 api
            internal uint dwOSVersionInfoSize;
            internal uint dwMajorVersion;
            internal uint dwMinorVersion;
            internal uint dwBuildNumber;
            internal uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string szCSDVersion;
        }

        // function supported ntdll.dll library
        [DllImport("ntdll.dll", ExactSpelling = true)]
        private static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

        //user-friendly method for obtaining os version information
        private RTL_OSVERSIONINFOEX RtlGetVersion()
        {
            var osVersionInfo = new RTL_OSVERSIONINFOEX();

            // important action
            osVersionInfo.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osVersionInfo);

            RtlGetVersionRequestStatus = RtlGetVersion(out osVersionInfo);

            return osVersionInfo;
        }

        public override string ToString()
        {
            if (RtlGetVersionRequestStatus == 0)
            {
                return string.Format("{0} {1}.{2}.{3} {4}",
                    "Microsoft Windows", 
                    WindowsVersionInfo.dwMajorVersion, WindowsVersionInfo.dwMinorVersion, 
                    WindowsVersionInfo.dwBuildNumber, WindowsVersionInfo.szCSDVersion);
            }
            else return "Microsoft Windows";      
        }
    }
}
