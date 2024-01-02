using XoperoRecruitmentTask.Client;
using XoperoRecruitmentTask.Communication;
using XoperoRecruitmentTask.Logging;
using XoperoRecruitmentTask.Logging.Enums;
using XoperoRecruitmentTask.Server.Interfaces;

namespace XoperoRecruitmentTask.Server.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly ICommunicationService _communicationService;
        private readonly ILogMessageService _logService;

        public VolumeService(ICommunicationService communicationService, ILogMessageService logService)
        {
            _communicationService = communicationService;
            _logService = logService;
        }

        public async Task<bool> HandleRequest(VolumeRequest request, string connectionId)
        {
            try
            {
                await _communicationService.SendMessageAsync(connectionId, "report_progress", "Start of processing");

                _logService.Log(LogType.Info, $"{request.TimeStamp:yyyy-MM-dd HH:mm}\t{request.MachineName}\tSTART\n");

                foreach (var volume in request.VolumeInfos)
                {
                    _logService.Log(LogType.Info, $"Disk number: {volume.DiskNumber}\tStarting offset: {volume.StartingOffset}\tExtent length: {volume.ExtentLength}\n");
                }

                await _communicationService.SendMessageAsync(connectionId, "report_progress", "End of processing");
            }
            catch(Exception ex)
            {
                _logService.Log(LogType.Error, ex.Message.ToString());
            }

            _logService.Log(LogType.Info, $"{request.TimeStamp:yyyy-MM-dd HH:mm}\t{request.MachineName}\tEND\n");

            return true;
        }
    }
}
