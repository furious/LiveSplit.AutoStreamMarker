using System;
using System.IO;

namespace LiveSplit.UI.Components
{
    // One instance per independent OBS output (streaming vs. recording) - each writes its own file.
    public class StreamSessionLogger
    {
        private readonly string FileSuffix;
        private readonly object WriteLock = new object();

        private bool WasActive;
        private string CurrentFilePath;
        private DateTime SessionStartUtc;

        public StreamSessionLogger(string fileSuffix)
        {
            FileSuffix = fileSuffix;
        }

        // durationMs/queryTimeUtc calibrate the session start once; each mark's elapsed time
        // then comes from its own markTimeUtc against that fixed anchor.
        public void AppendMark(bool isActive, double durationMs, DateTime queryTimeUtc, DateTime markTimeUtc, string folder, string description)
        {
            if (!isActive)
            {
                WasActive = false;
                return;
            }

            lock (WriteLock)
            {
                if (!WasActive)
                {
                    SessionStartUtc = queryTimeUtc - TimeSpan.FromMilliseconds(Math.Max(0, durationMs));
                    Directory.CreateDirectory(folder);
                    CurrentFilePath = Path.Combine(folder, SessionStartUtc.ToLocalTime().ToString("yyyy-MM-dd") + "-" + FileSuffix + ".txt");

                    File.AppendAllText(CurrentFilePath, string.Format(
                        "=== Session started {0:yyyy-MM-dd HH:mm:ss} ==={1}",
                        SessionStartUtc.ToLocalTime(), Environment.NewLine
                    ));

                    WasActive = true;
                }

                TimeSpan elapsed = markTimeUtc - SessionStartUtc;
                if (elapsed < TimeSpan.Zero)
                {
                    elapsed = TimeSpan.Zero;
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
