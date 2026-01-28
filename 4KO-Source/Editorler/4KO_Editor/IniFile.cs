namespace _4KO_Editor
{
    public class IniFile
    {
        string Path;
        [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, System.Text.StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath) { Path = new System.IO.FileInfo(IniPath).FullName; }
        public void Write(string Section, string Key, string Value) { WritePrivateProfileString(Section, Key, Value, Path); }
        public string Read(string Section, string Key)
        {
            var RetVal = new System.Text.StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
    }
}
