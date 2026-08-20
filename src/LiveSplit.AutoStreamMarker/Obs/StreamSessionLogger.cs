using System;
using System.IO;

namespace LiveSplit.UI.Components
{
    /// <summary>
    /// Tracks an OBS output's active/inactive transitions and appends each
    /// mark to a local, per-date log file as "H:MM:SS Description" - one
    /// instance per independent OBS output (e.g. streaming vs. recording),
    /// since each has its own timeline and start time.
    /// </summary>
    public class StreamSessionLogger
    {
        private readonly object WriteLock = new object();

        private bool WasActive;
        private string CurrentFilePath;

        /// <summary>
        /// Appends a mark line to today's (or the current session's) log
        /// file if the output is active. Does nothing otherwise.
        /// </summary>
        /// <param name="isActive">Whether the OBS output is currently running.</param>
        /// <param name="durationMs">How long the output has been running, in milliseconds.</param>
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
