using PummelPartyHack.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace PummelPartyHack.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged, IValidationExceptionHandler
    {
        private readonly PummelPartyHackCore core;
        private int trophyCount;
        public MainWindowViewModel()
        {
            core = PummelPartyHackCore.Instance;
            GetTrophyCountCommand = new RelayCommand(_ => true, _ => GetTrophyCount());
            SetTrophyCountCommand = new RelayCommand(_ => IsValid, _ => SetTrophyCount());
            OpenBrowserCommand = new RelayCommand(_ => true, parameters =>
            {
                if (parameters is Uri uri)
                {
                    Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GetTrophyCountCommand { get; }
        public ICommand SetTrophyCountCommand { get; }
        public ICommand OpenBrowserCommand { get; }
        public int TrophyCount
        {
            get => trophyCount; set
            {
                trophyCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrophyCount)));
            }
        }

        public bool IsValid { get; set; } = true;

        public void GetTrophyCount()
        {
            TrophyCount = core.GetTrophyCount();
        }

        public void SetTrophyCount()
        {
            core.SetTrophyCount(TrophyCount);
            MessageBox.Show("修改奖杯数量成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
