using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fotootof.Libraries.Classes.Systems
{
    public class PowerManager
    {
        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public Int32 BatteryLifetime;
            public Int32 BatteryFullLifetime;
        }
        
        public enum ACLineStatus : byte
        {
            Battery = 0,
            AC = 1,
            Unknown = 255
        }

        /// <summary>
        /// 
        /// </summary>
        [FlagsAttribute]
        public enum BatteryFlag : byte
        {
            High = 1,
            Low = 2,
            Critical = 4,
            Charging = 8,
            NoSystemBattery = 128,
            Unknown = 255
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemPowerStatus"></param>
        /// <returns></returns>
        [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static bool GetSystemPowerStatus(SYSTEM_POWER_STATUS SystemPowerStatus);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDevice"></param>
        /// <param name="fOn"></param>
        /// <returns></returns>
        [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static bool GetDevicePowerState(IntPtr hDevice, out bool fOn);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string ReportPowerStatus()
        {
            string status = string.Empty;

            SYSTEM_POWER_STATUS powerStatus;
            powerStatus = new SYSTEM_POWER_STATUS();

            bool result = GetSystemPowerStatus(powerStatus);

            if (result)
            {
                StringBuilder statusMsg = new StringBuilder();

                statusMsg.Append("AC Power status: ");
                if ((byte)ACLineStatus.Battery == powerStatus.ACLineStatus)
                {
                    statusMsg.Append("Offline");
                }
                else if ((byte)ACLineStatus.AC == powerStatus.ACLineStatus)
                {
                    statusMsg.Append("Online");
                }
                else
                {
                    statusMsg.Append("Unknown");
                }

                statusMsg.Append(System.Environment.NewLine);
                statusMsg.Append("Battery Charge status: ");
                statusMsg.Append(powerStatus.BatteryFlag.ToString());

                statusMsg.Append(System.Environment.NewLine);
                statusMsg.Append("Battery Charged: ");
                if (255 == powerStatus.BatteryLifePercent)
                {
                    statusMsg.Append("Unknown");
                }
                else
                {
                    statusMsg.Append(
                        powerStatus.BatteryLifePercent.ToString());
                    statusMsg.Append("%");
                }

                statusMsg.Append(System.Environment.NewLine);
                statusMsg.Append("Battery Life left: ");

                if (-1 == powerStatus.BatteryLifetime)
                {
                    statusMsg.Append("Unknown");
                }
                else
                {
                    statusMsg.Append(
                        powerStatus.BatteryLifetime.ToString());
                    statusMsg.Append(" seconds");
                }

                statusMsg.Append(System.Environment.NewLine);
                statusMsg.Append("A Full battery will last: ");
                if (-1 == powerStatus.BatteryFullLifetime)
                {
                    statusMsg.Append("Unknown");
                }
                else
                {
                    statusMsg.Append(
                    powerStatus.BatteryFullLifetime.ToString());
                    statusMsg.Append(" seconds");
                }
                statusMsg.Append(System.Environment.NewLine);

                Console.Out.WriteLine(statusMsg);
                status = statusMsg.ToString();

            }
            else
            {
                Console.Out.WriteLine("Failed to get power status");
            }
            Console.Out.WriteLine();
            return status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string ReportDiskStatus()
        {
            string status = string.Empty;
            bool fOn = false;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileStream[] files = assembly.GetFiles();
            if (files.Length > 0)
            {
                IntPtr hFile = files[0].Handle;
                bool result = GetDevicePowerState(hFile, out fOn);
                if (result)
                {
                    if (fOn)
                    {
                        status = "Disk is powered up and spinning";
                    }
                    else
                    {
                        status = "Disk is sleeping";
                    }
                }
                else
                {
                    status = "Cannot get Disk Status";
                }
                Console.WriteLine(status);
            }
            return status;
        }

        const int WM_POWERBROADCAST = 0x0218;

        const int PBT_APMQUERYSUSPEND = 0x0000;
        const int PBT_APMQUERYSTANDBY = 0x0001;
        const int PBT_APMQUERYSUSPENDFAILED = 0x0002;
        const int PBT_APMQUERYSTANDBYFAILED = 0x0003;
        const int PBT_APMSUSPEND = 0x0004;
        const int PBT_APMSTANDBY = 0x0005;
        const int PBT_APMRESUMECRITICAL = 0x0006;
        const int PBT_APMRESUMESUSPEND = 0x0007;
        const int PBT_APMRESUMESTANDBY = 0x0008;
        const int PBT_APMBATTERYLOW = 0x0009;
        const int PBT_APMPOWERSTATUSCHANGE = 0x000A;
        const int PBT_APMOEMEVENT = 0x000B;
        const int PBT_APMRESUMEAUTOMATIC = 0x0012;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (WM_POWERBROADCAST == m.Msg)
            {
                // Check the status and act accordingly.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class PowerMessageFilter : IMessageFilter
        {
            protected void ReportPowerChange(int reason)
            {
                string report = string.Empty;
                switch (reason)
                {
                    case PBT_APMQUERYSUSPEND:
                        report = "Request for permission to suspend.";
                        break;
                    case PBT_APMQUERYSTANDBY:
                        report = "Request for permission to stand by.";
                        break;
                    case PBT_APMQUERYSUSPENDFAILED:
                        report = "Suspension request denied. ";
                        break;
                    case PBT_APMQUERYSTANDBYFAILED:
                        report = "Stand by request denied.";
                        break;
                    case PBT_APMSUSPEND:
                        report = "System is suspending operation ";
                        break;
                    case PBT_APMSTANDBY:
                        report = "System is standing by ";
                        break;
                    case PBT_APMRESUMECRITICAL:
                        report =
                          "Operation resuming after critical suspension.";
                        break;
                    case PBT_APMRESUMESUSPEND:
                        report = "Operation resuming after suspension.";
                        break;
                    case PBT_APMRESUMESTANDBY:
                        report = "Operation resuming after stand by.";
                        break;
                    case PBT_APMBATTERYLOW:
                        report = "Battery power is low.";
                        break;
                    case PBT_APMPOWERSTATUSCHANGE:
                        report = "Power status has changed.";
                        break;
                    case PBT_APMOEMEVENT:
                        report = "OEM-defined event occurred.";
                        break;
                    case PBT_APMRESUMEAUTOMATIC:
                        report =
                           "Operation resuming automatically after event.";
                        break;
                }
                Console.Out.WriteLine(report);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            public bool PreFilterMessage(ref Message m)
            {
                if (WM_POWERBROADCAST == m.Msg)
                {
                    Console.Out.WriteLine("Power Broadcast recieved.");
                    int reason = m.WParam.ToInt32();
                    ReportPowerChange(reason);
                }
                return false;
            }

            /// <summary>
            /// 
            /// </summary>
            [FlagsAttribute]
            public enum EXECUTION_STATE : uint
            {
                ES_SYSTEM_REQUIRED = 0x00000001,
                ES_DISPLAY_REQUIRED = 0x00000002,
                // Legacy flag, should not be used.
                // ES_USER_PRESENT   = 0x00000004,
                ES_CONTINUOUS = 0x80000000,
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="state"></param>
            /// <returns></returns>
            [DllImport("Kernel32.DLL", CharSet = CharSet.Auto, SetLastError = true)]
            private extern static EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE state);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="state"></param>
            /// <returns></returns>
            public EXECUTION_STATE SetExecutionState(EXECUTION_STATE state)
            {
                EXECUTION_STATE oldState = SetThreadExecutionState(state);
                return oldState;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void VisualizerForm_SizeChanged(object sender, EventArgs e)
            {
                if (null == power)
                {
                    return;
                }

                if (this.WindowState == FormWindowState.Maximized)
                {
                    previousState = power.SetExecutionState(PowerClass.EXECUTION_STATE.ES_DISPLAY_REQUIRED | PowerClass.EXECUTION_STATE.ES_CONTINUOUS);

                    animationTimer.Enabled = true;

                }
                else
                {
                    power.SetExecutionState(PowerClass.EXECUTION_STATE.ES_CONTINUOUS);
                    if (power.RunningOnBattery)
                    {
                        animationTimer.Enabled = false;
                    }
                }
            }
        }
    }
}