using Microsoft.AspNetCore.SignalR;

namespace XoperoRecruitmentTask.Communication
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IHubContext<ReportHub> _hubContext;

        public CommunicationService(IHubContext<ReportHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(string connectionId, string method, string message)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(method, message);
        }
    }
}
