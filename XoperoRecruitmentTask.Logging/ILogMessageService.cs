using XoperoRecruitmentTask.Logging.Enums;

namespace XoperoRecruitmentTask.Logging
{
    public interface ILogMessageService
    {
        void Log(LogType logType, string message);
    }
}
