using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PummelPartyHack.Core
{
    public class Module
    {
        internal Module(IntPtr handle, string name)
        {
            Handle = handle;
            Name = name;
        }

        public IntPtr Handle { get; }
        public string Name { get; }
    }
    internal static class WindowsAPI
    {

        #region Windows API
        [DllImport("kernel32", EntryPoint = "K32EnumProcessModulesEx")]
        public static extern bool EnumProcessModulesEx(IntPtr hProcess, IntPtr[] lphModule, int cb, out int lpcbNeeded, int dwFilterFlag);
        [DllImport("kernel32", EntryPoint = "K32GetModuleBaseName")]
        public static extern bool GetModuleBaseName(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName, int nSize);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);
        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern long GetLastError();
        #endregion

        public static IEnumerable<Module> EnumProcessModules(IntPtr pHandle)
        {
            var modules = new IntPtr[4096];
            bool success = EnumProcessModulesEx(pHandle, modules, 1024, out int modulesCount, 0x03);
            if (!success)
            {
                yield break;
            }
            var mName = new StringBuilder();
            foreach (var m in modules.Take(modulesCount))
            {
                mName.Clear();
                if (GetModuleBaseName(pHandle, m, mName, 1024))
                {
                    yield return new Module(m, mName.ToString());
                }
            }
        }
        public static bool TryReadProcessMemoryBool(IntPtr hProcess, IntPtr lpBaseAddress, out bool value)
        {
            var buffer = new byte[1];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToBoolean(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryByte(IntPtr hProcess, IntPtr lpBaseAddress, out byte value)
        {
            var buffer = new byte[1];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = buffer[0];
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryDouble(IntPtr hProcess, IntPtr lpBaseAddress, out double value)
        {
            var buffer = new byte[8];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToDouble(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryInt16(IntPtr hProcess, IntPtr lpBaseAddress, out short value)
        {
            var buffer = new byte[2];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToInt16(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryInt32(IntPtr hProcess, IntPtr lpBaseAddress, out int value)
        {
            var buffer = new byte[4];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToInt32(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryInt64(IntPtr hProcess, IntPtr lpBaseAddress, out long value)
        {
            var buffer = new byte[8];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToInt64(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemorySingle(IntPtr hProcess, IntPtr lpBaseAddress, out float value)
        {
            var buffer = new byte[4];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToSingle(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryUInt16(IntPtr hProcess, IntPtr lpBaseAddress, out ushort value)
        {
            var buffer = new byte[2];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToUInt16(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryUInt32(IntPtr hProcess, IntPtr lpBaseAddress, out uint value)
        {
            var buffer = new byte[4];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToUInt32(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryReadProcessMemoryUInt64(IntPtr hProcess, IntPtr lpBaseAddress, out ulong value)
        {
            var buffer = new byte[8];
            if (ReadProcessMemory(hProcess, lpBaseAddress, buffer, buffer.Length, out _))
            {
                value = BitConverter.ToUInt64(buffer, 0);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryWriteProcessMemoryBool(IntPtr hProcess, IntPtr lpBaseAddress, bool value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryByte(IntPtr hProcess, IntPtr lpBaseAddress, byte value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryDouble(IntPtr hProcess, IntPtr lpBaseAddress, double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }

        public static bool TryWriteProcessMemoryInt16(IntPtr hProcess, IntPtr lpBaseAddress, short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryInt32(IntPtr hProcess, IntPtr lpBaseAddress, int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryInt64(IntPtr hProcess, IntPtr lpBaseAddress, long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemorySingle(IntPtr hProcess, IntPtr lpBaseAddress, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }

        public static bool TryWriteProcessMemoryUInt16(IntPtr hProcess, IntPtr lpBaseAddress, ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryUInt32(IntPtr hProcess, IntPtr lpBaseAddress, uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
        public static bool TryWriteProcessMemoryUInt64(IntPtr hProcess, IntPtr lpBaseAddress, ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (WriteProcessMemory(hProcess, lpBaseAddress, bytes, bytes.Length, out _))
            {
                return true;
            }
            return false;
        }
    }
}
