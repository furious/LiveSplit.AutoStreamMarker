LiveSplit.AutoStreamMarker
========================
Auto Stream Marker is a [LiveSplit](http://livesplit.org/) component that automatically marks every run in your stream/recording VOD.

Features
--------
* Marks start and end of every run for easy highlight later
* Optionally mark resets and individual splits
* Optionally create a chapter marker in OBS's own recording via obs-websocket
* Optionally log stream sessions to a local file with timestamps (`H:MM:SS description`), whenever OBS is streaming (`yyyy-MM-dd-streaming.txt`)
* Falls back to writing the mark to its own log file (`yyyy-MM-dd-recording.txt`) if OBS rejects the chapter marker - most commonly because its recording container doesn't support chapters (anything other than Hybrid MP4 or Hybrid MOV)

Installation
------------
1. [Download Auto Stream Marker](https://github.com/furious/LiveSplit.AutoStreamMarker/releases/latest)
2. Close LiveSplit if it was open and place `LiveSplit.AutoStreamMarker.dll` in the `Components` **directory** of LiveSplit.
3. Start LiveSplit and add **Auto Stream Marker** to your LiveSplit **layout** (It is in the "**Other**" category)
4. Edit the component settings, connect your Twitch account.

How to use
----------
1. Start your stream as usually
2. Every run will be marked using the "broadcast marker" feature
3. You can check if its correctly marking your runs in the Twitch dashboard
4. If you enable the OBS options in the component settings, marks are also written as chapter markers in OBS's recording and/or appended to a per-date log file. If OBS rejects a chapter marker (check its recording output settings - only Hybrid MP4 and Hybrid MOV support chapters), the mark is written to a separate recording log file instead

Requirements
------------
* .NET 4.8.1 (Windows 10 or later)
* [LiveSplit](http://livesplit.org/) 1.6 or later
* [OBS Studio](https://obsproject.com/) with obs-websocket enabled (only required for the OBS chapter marker and stream session log features)

Credits
-------
**FURiOUS**: [Website](https://furious.pro)
