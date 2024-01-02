using XoperoRecruitmentTask.Win32API.Dtos;

namespace XoperoRecruitmentTask.Win32API
{
    public interface IVolumeService
    {
        List<VolumeInfoDTO> GetVolumes();
    }
}
