using XoperoRecruitmentTask.Client;

namespace XoperoRecruitmentTask.Server.Interfaces
{
    public interface IVolumeService
    {
        Task<bool> HandleRequest(VolumeRequest volumes, string connectionId);
    }
}
