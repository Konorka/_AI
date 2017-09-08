using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace SpaceBattle.Common
{
    public class IniFile
    {
        public string path;
        object lockObj = new object();
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>

        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            lock (lockObj)
            {
                WritePrivateProfileString(Section, Key, Value, this.path);
            }
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string Def)
        {
            StringBuilder temp = new StringBuilder(255);
            lock (lockObj)
            {
                int i = GetPrivateProfileString(Section, Key, Def, temp, 255, this.path);
            }
            return temp.ToString();
        }

        public int IniReadInt(string Section, string Key, int Def)
        {
            return int.Parse(IniReadValue(Section, Key, Def.ToString()));
        }
        public float IniReadFloat(string Section, string Key, float Def)
        {
            return float.Parse(IniReadValue(Section, Key, Def.ToString()));
        }
    }

}
