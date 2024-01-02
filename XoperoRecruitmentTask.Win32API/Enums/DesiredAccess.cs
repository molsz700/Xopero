namespace XoperoRecruitmentTask.Win32API.Enums
{
    [Flags]
    public enum DesiredAccess : uint
    {
        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000
    }
}
