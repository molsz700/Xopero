//using XoperoRecruitmentTask.Win32API.Dtos;

namespace XoperoRecruitmentTask.Logging.Dtos
{
    public class ClientLogDTO
    {
        public DateTime TimeStamp { get; set; }
        public string MachineName { get; set; }
        //public List<VolumeInfoDTO> Volumes { get; set; }
        public string Message { get; set; }
    }
}
