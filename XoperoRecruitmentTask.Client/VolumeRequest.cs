using XoperoRecruitmentTask.Win32API.Dtos;

namespace XoperoRecruitmentTask.Client
{
    public class VolumeRequest
    {
        public string MachineName { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<VolumeInfoDTO> VolumeInfos { get; set; }
    }
}
