using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ZBase.Utilities
{
    public class Memory
    {
        private IntPtr processHandle = IntPtr.Zero;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        public Memory(string processName)
        {
            var process = Process.GetProcessesByName(processName)[0];
            processHandle = OpenProcess(0x001F0FFF, false, process.Id);
        }

        public IntPtr GetModuleAddress(string moduleName)
        {
            var process = Process.GetProcessesByName("csgo")[0];
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == moduleName)
                    return module.BaseAddress;
            }
            return IntPtr.Zero;
        }

        public T Read<T>(IntPtr address) where T : struct
        {
            var buffer = new byte[Marshal.SizeOf(typeof(T))];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead);
            return ByteArrayToStructure<T>(buffer);
        }

        public void Write<T>(IntPtr address, T value) where T : struct
        {
            var buffer = StructureToByteArray(value);
            WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out _);
        }

        private static byte[] StructureToByteArray<T>(T obj) where T : struct
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            T stuff = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return stuff;
        }
    }
}
