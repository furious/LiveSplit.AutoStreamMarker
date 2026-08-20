using System;
using System.IO;

namespace LiveSplit.UI.Components
{
    // Use one instance per independent OBS output (streaming vs. recording) - each has its own timeline.
    public class StreamSessionLogger
    {
        private readonly object WriteLock = new object();

        private bool WasActive;
        private string CurrentFilePath;

        public void AppendMark(bool isActive, double durationMs, string folder, string description)
        {
            if (!isActive)
            {
                WasActive = false;
                return;
            }

            TimeSpan elapsed = TimeSpan.FromMilliseconds(Math.Max(0, durationMs));

            lock (WriteLock)
            {
                if (!WasActive)
                {
                    DateTime sessionStart = DateTime.Now - elapsed;
                    Directory.CreateDirectory(folder);
                    CurrentFilePath = Path.Combine(folder, sessionStart.ToString("yyyy-MM-dd") + ".txt");

                    File.AppendAllText(CurrentFilePath, string.Format(
                        "=== Session started {0:yyyy-MM-dd HH:mm:ss} ==={1}",
                        sessionStart, Environment.NewLine
                    ));

                    WasActive = true;
                }

                File.AppendAllText(CurrentFilePath, string.Format(
                    "{0} {1}{2}",
                    FormatTimestamp(elapsed), description, Environment.NewLine
                ));
            }
        }

        private static string FormatTimestamp(TimeSpan elapsed)
        {
            return string.Format("{0}:{1:00}:{2:00}", (int)elapsed.TotalHours, elapsed.Minutes, elapsed.Seconds);
        }
    }
}
