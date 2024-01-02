namespace XoperoRecruitmentTask.Win32API.Enums
{
    [Flags]
    public enum ShareMode : uint
    {
        FILE_SHARE_NONE = 0x0,
        FILE_SHARE_READ = 0x1,
        FILE_SHARE_WRITE = 0x2,
        FILE_SHARE_DELETE = 0x4,
    }
}
