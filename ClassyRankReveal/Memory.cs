using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ClassyRankReveal
{
    public class Memory
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess( int processAccess, bool bInheritHandle,  int processId);


        [DllImport("kernel32.dll", SetLastError = true)] static extern bool ReadProcessMemory( IntPtr hProcess,int lpBaseAddress, [Out] byte[] lpBuffer, int dwSize,out int  lpNumberOfBytesRead);


        public Process TargProc { get; set; } = null;
        public IntPtr Handle { get; set; } = IntPtr.Zero;
        public Memory(Process TargetProcess)
        {
            this.TargProc = TargetProcess;
            Init();
        }


        public void Init()
        {
            Handle = OpenProcess(0x00000010, false, TargProc.Id);
        }

        public int ReadInt(int Address)
        {
            byte[] IntBuf = new byte[4];
            int Read = 0;
            ReadProcessMemory(Handle, Address,  IntBuf, 4, out Read);
            if (Read == 4)
            {
                return BitConverter.ToInt32(IntBuf,0);
            }
            return 0;
        }

        public string ReadString(int memaddress, int len, bool unicode)
        {
            byte[] byteinfo = new byte[len];
            int read = 0;
            ReadProcessMemory(Handle, memaddress, byteinfo, len,out  read);
            if (read == len)
            {
                int End = 0;
                for (int i = 0; i < byteinfo.Length; i++)
                {
                    if (byteinfo[i] == 0)
                    {
                        End = i;
                        break;
                    }
                }
                byte[] FinalString = new byte[End];

                Buffer.BlockCopy(byteinfo, 0, FinalString, 0, End);
                if (unicode)
                {
                    return Encoding.Unicode.GetString(FinalString);
                }
                else
                {
                    return Encoding.ASCII.GetString(FinalString);
                }
            }
            return "";

        }
        public int GetModuleAddress(string Module)
        {
            foreach (ProcessModule mod in TargProc.Modules)
            {
                if (mod.ModuleName.ToLower() == Module.ToLower())
                {
                    return mod.BaseAddress.ToInt32();
                }
            }
            return 0;
        }




    }
}
