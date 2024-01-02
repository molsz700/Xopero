using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;
using XoperoRecruitmentTask.Win32API.Enums;

namespace XoperoRecruitmentTask.Win32API
{
    public class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern FindVolumeSafeHandle FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FindNextVolume(FindVolumeSafeHandle hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FindVolumeClose(IntPtr hFindVolume);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(string lpFileName, DesiredAccess dwDesiredAccess, ShareMode dwShareMode, IntPtr lpSecurityAttributes,
                                                CreationDisposition dwCreationDisposition, FlagsAndAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(SafeFileHandle hDevice, uint ioControlCode, IntPtr inBuffer, UInt32 inBufferSize, IntPtr outBuffer,
                                                    UInt32 outBufferSize, out uint bytesReturned, IntPtr overlapped);

        public class FindVolumeSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private FindVolumeSafeHandle() : base(true) { }

            public FindVolumeSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
            {
                SetHandle(preexistingHandle);
            }

            protected override bool ReleaseHandle()
            {
                return FindVolumeClose(handle);
            }
        }
    }
}
