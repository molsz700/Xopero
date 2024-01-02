using System.Runtime.InteropServices;

namespace XoperoRecruitmentTask.Win32API.Dtos
{
    [StructLayout(LayoutKind.Sequential)]
    public class VolumeDiskExtentsDTO
    {
        public uint NumberOfDiskExtents;
        public VolumeDiskExtentDTO VolumeInfo;
    }
}
