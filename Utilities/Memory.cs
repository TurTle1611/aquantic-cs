using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZBase.Classes;

namespace ZBase.Utilities
{
    public static class Memory
    {
        public static string WindName = "Counter-Strike: Global Offensive";
        public static Process Process;
        public static IntPtr ProcessHandle;
        public static IntPtr Client;
        public static IntPtr Engine;
        public static int m_iBytesRead = 0;
        public static int m_iBytesWrite = 0;

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public static void InitializeProcess(string processName)
        {
            var process = Process.GetProcessesByName(processName)[0];
            Process = process;
            ProcessHandle = OpenProcess(0x001F0FFF, false, process.Id);
        }

        public static IntPtr GetModuleAddress(string moduleName)
        {
            foreach (ProcessModule module in Process.Modules)
            {
                if (module.ModuleName == moduleName)
                    return module.BaseAddress;
            }
            return IntPtr.Zero;
        }

        // Updated ReadMemory method using IntPtr
        public static T ReadMemory<T>(IntPtr address) where T : struct
        {
            int byteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[byteSize];
            ReadProcessMemory(ProcessHandle, address, buffer, buffer.Length, ref m_iBytesRead);
            return ByteArrayToStructure<T>(buffer);
        }

        // Updated WriteMemory method using IntPtr
        public static void WriteMemory<T>(IntPtr address, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            WriteProcessMemory(ProcessHandle, address, buffer, buffer.Length, out m_iBytesWrite);
        }

        // Helper methods
        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] StructureToByteArray<T>(T obj) where T : struct
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        internal static void WriteMemory<T>(IntPtr intPtr, byte v)
        {
            throw new NotImplementedException();
        }
    }

   
 
}
