using XoperoRecruitmentTask.Logging.Enums;

namespace XoperoRecruitmentTask.Logging
{
    public class LogMessageService : ILogMessageService
    {
        public void Log(LogType type, string message)
        {
            string filename = @"log.txt";
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, type.ToString() + " | " + message);
            }
            else
            {
                File.AppendAllText(filename, type.ToString() + " | " + message);
            }
            
        }
    }
}
