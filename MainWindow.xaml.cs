using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;

namespace FSUIPCRestartIssue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectClick(object sender, RoutedEventArgs e)
        {
            // Connect

            if (variableServices == null)
            {
                variableServices = new FSUIPC.MSFSVariableServices();

                var hwnd = new WindowInteropHelper(this).Handle;
                variableServices.LVARUpdateFrequency = 10;
                variableServices.LogLevel = FSUIPC.LOGLEVEL.LOG_LEVEL_INFO;

                variableServices.OnVariableListChanged += VariableServices_OnVariableListChanged;
                variableServices.OnLogEntryReceived += VariableServices_OnLogEntryReceived;

                variableServices.Init(hwnd);
            }

            variableServices.Start();
        }

        private void DisconnectClick(object sender, RoutedEventArgs e)
        {
            variableServices.Stop();
        }

        private void VariableServices_OnLogEntryReceived(object sender, FSUIPC.LogEventArgs e)
        {
            Debug.WriteLine(e.LogEntry);
        }

        private void VariableServices_OnVariableListChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("OnVariableListChanged called. LVars visible in MSFSVariableServices: {0}", variableServices.LVars.Count);
        }

        private FSUIPC.MSFSVariableServices variableServices;

    }
}
