namespace ProjectManagement.Infrastructure.Configuration
{
    public class LoggingSettings
    {
        public const string Section = "LogSettings";
        public string LogFilePath { get; set; }
        public string LogTableName { get; set; }
    }
}
