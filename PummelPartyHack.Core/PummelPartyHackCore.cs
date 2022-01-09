using System;
using System.Diagnostics;
using System.Linq;
using static PummelPartyHack.Core.WindowsAPIHelper;

namespace PummelPartyHack.Core
{
    public sealed class PummelPartyHackCore : IDisposable
    {
        private const string ProcessName = "PummelParty";
        private const string ModuleName = "mono-2.0-bdwgc.dll";
        private const int ModuleOffset = 0x003AC2D4;

        private Process process;
        private IntPtr trophyAddress;
        private bool disposedValue;

        public static PummelPartyHackCore Instance { get; } = new PummelPartyHackCore();
        private PummelPartyHackCore()
        {
        }

        private void EnsureInit()
        {
            if (process != null && process.Handle != IntPtr.Zero)
            {
                return;
            }
            process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            if (process == null)
            {
                throw new InvalidOperationException("Pummel Party is not running");
            }
            var moduleBaseAddress = GetModuleAddress(process.Handle, ModuleName, ModuleOffset);
            trophyAddress = GetAddressByOffset(process.Handle, moduleBaseAddress, 0x548, 0xF98);
        }

        public int GetTrophyCount()
        {
            EnsureInit();
            return GetInt32(process.Handle, trophyAddress);
        }

        public void SetTrophyCount(int value)
        {
            EnsureInit();
            SetInt32(process.Handle, trophyAddress, value);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    process.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~PummelPartyHackCore()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
