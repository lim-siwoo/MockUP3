using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace UIUIIUIUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private DeploymentInfo _currentDeployment;
        private Random _random = new Random();

        public MainWindow()
        {
            this.InitializeComponent();

            // 초기 배포 정보 로드
            LoadDeploymentInfo();

            // 앱 시작 시 새 버전 알림 표시 (목업용)
            SimulateNewVersionDeployment();
        }

        private void LoadDeploymentInfo()
        {
            // 실제 구현에서는 로컬 저장소나 서버에서 배포 정보를 가져옴
            // 여기서는 목업 데이터 사용
            _currentDeployment = new DeploymentInfo
            {
                Version = GetAppVersion(),
                DeploymentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DeviceId = "EDGE-DEVICE-" + _random.Next(1000, 9999).ToString(),
                Status = DeploymentStatus.Success,
                Changelog = "• 보안 업데이트 적용\n• UI 성능 개선\n• 배터리 소모량 최적화\n• 네트워크 연결 안정성 향상"
            };

            UpdateUI();
        }

        private string GetAppVersion()
        {

            return "1.0.0";
        }

        private void UpdateUI()
        {
            // UI 업데이트
            VersionTextBlock.Text = _currentDeployment.Version;
            DeploymentTimeTextBlock.Text = _currentDeployment.DeploymentTime;
            DeviceIdTextBlock.Text = _currentDeployment.DeviceId;

            // 배포 상태에 따라 표시 변경
            switch (_currentDeployment.Status)
            {
                case DeploymentStatus.Success:
                    StatusIndicator.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.LimeGreen);
                    DeploymentStatusTextBlock.Text = "성공";
                    DeploymentStatusTextBlock.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.LimeGreen);
                    break;

                case DeploymentStatus.Failed:
                    StatusIndicator.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.Red);
                    DeploymentStatusTextBlock.Text = "실패";
                    DeploymentStatusTextBlock.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.Red);
                    break;

                case DeploymentStatus.InProgress:
                    StatusIndicator.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.Orange);
                    DeploymentStatusTextBlock.Text = "진행 중";
                    DeploymentStatusTextBlock.Foreground = new Microsoft.UI.Xaml.Media.SolidColorBrush(
                        Microsoft.UI.Colors.Orange);
                    break;
            }

            ChangelogTextBlock.Text = _currentDeployment.Changelog;
        }

        private async void SimulateNewVersionDeployment()
        {
            // 목업용: 앱 시작 시 새 버전 알림 표시
            await Task.Delay(2000);
            NewVersionNotification.Visibility = Visibility.Visible;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // 목업용: 새로고침 시 배포 시간 업데이트
            _currentDeployment.DeploymentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UpdateUI();
        }

        private void DismissNotification_Click(object sender, RoutedEventArgs e)
        {
            NewVersionNotification.Visibility = Visibility.Collapsed;
        }
    }

    public enum DeploymentStatus
    {
        Success,
        Failed,
        InProgress
    }

    public class DeploymentInfo
    {
        public string Version { get; set; } = string.Empty;
        public string DeploymentTime { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public DeploymentStatus Status { get; set; }
        public string Changelog { get; set; } = string.Empty;
    }
}
