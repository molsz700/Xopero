using System.Runtime.InteropServices;

namespace XoperoRecruitmentTask.Win32API.Dtos
{
    [StructLayout(LayoutKind.Sequential)]
    public class VolumeDiskExtentDTO
    {
        public uint DiskNumber;
        public long StartingOffset;
        public long ExtentLength;

    }
}
