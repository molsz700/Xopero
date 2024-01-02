using System.Runtime.InteropServices;
using System.Text;
using XoperoRecruitmentTask.Logging;
using XoperoRecruitmentTask.Win32API.Dtos;
using XoperoRecruitmentTask.Win32API.Enums;
using static XoperoRecruitmentTask.Win32API.NativeMethods;

namespace XoperoRecruitmentTask.Win32API
{
    public class VolumeService : IVolumeService
    {
        
        private readonly ILogMessageService _logService;

        public VolumeService(ILogMessageService log)
        {
            _logService = log;
        }
        
        internal const uint IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS = 0x00560000;

        public List<VolumeInfoDTO> GetVolumes()
        {
            var result = new List<VolumeInfoDTO>();

            const uint bufferLength = 1024;
            StringBuilder volume = new StringBuilder((int)bufferLength, (int)bufferLength);
            
            using (FindVolumeSafeHandle volumeHandle = FindFirstVolume(volume.Append("\\sadasd"), bufferLength))
            {
                if (volumeHandle.IsInvalid)
                {
                    Console.WriteLine($"Error reading volumes");
                    _logService.Log(Logging.Enums.LogType.Error, "Error while reading volumes");
                }
                else
                {
                    do
                    { 
                        var safeFile = CreateFile(volume.ToString().TrimEnd('\\'), 0, ShareMode.FILE_SHARE_READ, nint.Zero,
                            CreationDisposition.OPEN_EXISTING, FlagsAndAttributes.FILE_FLAG_BACKUP_SEMANTICS, nint.Zero);

                        if (safeFile.IsInvalid)
                        {
                            _logService.Log(Logging.Enums.LogType.Error, "Error while creating safe file");
                        }
                        else
                        {
                            var vol = new VolumeDiskExtentsDTO();
                            var outBufferSize = (uint)Marshal.SizeOf(vol);
                            var outBuffer = Marshal.AllocHGlobal((int)outBufferSize);
                            uint bytesReturned = 0;

                            var control = DeviceIoControl(safeFile, IOCTL_VOLUME_GET_VOLUME_DISK_EXTENTS, 0, 0, outBuffer, outBufferSize, out bytesReturned, 0);

                            if (control)
                            {
                                Marshal.PtrToStructure(outBuffer, vol);

                                result.Add(new VolumeInfoDTO()
                                {
                                    DiskNumber = vol.VolumeInfo.DiskNumber,
                                    StartingOffset = (vol.VolumeInfo.StartingOffset/1024/1024).ToString(),
                                    ExtentLength = (vol.VolumeInfo.ExtentLength/1024/1024).ToString(),
                                });
                            }                            
                        }

                    } while (FindNextVolume(volumeHandle, volume, bufferLength));
                }

                return result.OrderBy(x => x.DiskNumber).ThenBy(x => x.StartingOffset).ToList();
            }
        }
    }
}
