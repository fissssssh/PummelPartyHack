using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PummelPartyHack.Core.WindowsAPI;
namespace PummelPartyHack.Core
{
    public static class WindowsAPIHelper
    {
        public static IntPtr GetModuleAddress(IntPtr hProcess, string moduleName, int offset)
        {
            var module = EnumProcessModules(hProcess).FirstOrDefault(m => m.Name == moduleName);
            if (module == null)
            {
                return IntPtr.Zero;
            }
            return module.Handle + offset;
        }

        public static IntPtr GetAddressByOffset(IntPtr hProcess, IntPtr address, params int[] offsets)
        {
            if (!TryReadProcessMemoryInt64(hProcess, address, out var next))
            {
                throw new Exception($"read memory failed, error code: {GetLastError()}");
            }
            address = (IntPtr)next;
            if (!offsets.Any())
            {
                return address;
            }
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                if (!TryReadProcessMemoryInt64(hProcess, address + offsets[i], out next))
                {
                    throw new Exception($"read memory failed, error code: {GetLastError()}");
                }
                address = (IntPtr)next;
            }
            return address + offsets[offsets.Length - 1];
        }

        #region Read Memory Helpers
        public static byte GetByte(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryByte(hProcess, address, out var value))
            {
                throw new Exception($"Get byte value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static short GetInt16(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryInt16(hProcess, address, out var value))
            {
                throw new Exception($"Get short value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static int GetInt32(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryInt32(hProcess, address, out var value))
            {
                throw new Exception($"Get int value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static long GetInt64(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryInt64(hProcess, address, out var value))
            {
                throw new Exception($"Get long value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static ushort GetUInt16(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryUInt16(hProcess, address, out var value))
            {
                throw new Exception($"Get ushort value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static uint GetUInt32(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryUInt32(hProcess, address, out var value))
            {
                throw new Exception($"Get uint value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static ulong GetUInt64(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryUInt64(hProcess, address, out var value))
            {
                throw new Exception($"Get ulong value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static float GetSingle(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemorySingle(hProcess, address, out var value))
            {
                throw new Exception($"Get float value error, error code: {GetLastError()}");
            }
            return value;
        }
        public static double GetDouble(IntPtr hProcess, IntPtr address)
        {
            if (!TryReadProcessMemoryDouble(hProcess, address, out var value))
            {
                throw new Exception($"Get double value error, error code: {GetLastError()}");
            }
            return value;
        }
        #endregion

        #region Write Memory Helpers
        public static byte SetByte(IntPtr hProcess, IntPtr address, byte value)
        {
            if (!TryWriteProcessMemoryByte(hProcess, address, value))
            {
                throw new Exception($"Set byte value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static short SetInt16(IntPtr hProcess, IntPtr address, short value)
        {
            if (!TryWriteProcessMemoryInt16(hProcess, address, value))
            {
                throw new Exception($"Set short value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static int SetInt32(IntPtr hProcess, IntPtr address, int value)
        {
            if (!TryWriteProcessMemoryInt32(hProcess, address, value))
            {
                throw new Exception($"Set int value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static long SetInt64(IntPtr hProcess, IntPtr address, long value)
        {
            if (!TryWriteProcessMemoryInt64(hProcess, address, value))
            {
                throw new Exception($"Set long value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static ushort SetUInt16(IntPtr hProcess, IntPtr address, ushort value)
        {
            if (!TryWriteProcessMemoryUInt16(hProcess, address, value))
            {
                throw new Exception($"Set ushort value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static uint SetUInt32(IntPtr hProcess, IntPtr address, uint value)
        {
            if (!TryWriteProcessMemoryUInt32(hProcess, address, value))
            {
                throw new Exception($"Set uint value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static ulong SetUInt64(IntPtr hProcess, IntPtr address, ulong value)
        {
            if (!TryWriteProcessMemoryUInt64(hProcess, address, value))
            {
                throw new Exception($"Set ulong value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static float SetSingle(IntPtr hProcess, IntPtr address, float value)
        {
            if (!TryWriteProcessMemorySingle(hProcess, address, value))
            {
                throw new Exception($"Set float value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        public static double SetDouble(IntPtr hProcess, IntPtr address, double value)
        {
            if (!TryWriteProcessMemoryDouble(hProcess, address, value))
            {
                throw new Exception($"Set double value error, value: {value}, error code: {GetLastError()}");
            }
            return value;
        }
        #endregion
    }
}
