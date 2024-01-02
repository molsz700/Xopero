namespace XoperoRecruitmentTask.Communication
{
    public interface ICommunicationService
    {
        Task SendMessageAsync(string connectionId, string method, string message);
    }
}
