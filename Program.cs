using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SnipX
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        static extern uint GetClipboardSequenceNumber();

        const int VK_LWIN = 0x5B;
        const int VK_RWIN = 0x5C;
        const int VK_SHIFT = 0x10;
        const int VK_ESCAPE = 0x1B;
        const int VK_S = 0x53;

        [STAThread]
        static void Main(string[] args)
        {
            bool expectingSnip = false;
            DateTime snipRequestedTime = DateTime.MinValue;
            uint lastClipboardSeq = GetClipboardSequenceNumber();

            // Run an infinite loop to monitor keystrokes and clipboard
            while (true)
            {
                // Note: GetAsyncKeyState returns a short where the most significant bit is set if the key is down.
                bool winDown = (GetAsyncKeyState(VK_LWIN) & 0x8000) != 0 || (GetAsyncKeyState(VK_RWIN) & 0x8000) != 0;
                bool shiftDown = (GetAsyncKeyState(VK_SHIFT) & 0x8000) != 0;
                bool sDown = (GetAsyncKeyState(VK_S) & 0x8000) != 0;
                bool escDown = (GetAsyncKeyState(VK_ESCAPE) & 0x8000) != 0;

                if (escDown)
                {
                    expectingSnip = false;
                }

                if (winDown && shiftDown && sDown)
                {
                    expectingSnip = true;
                    snipRequestedTime = DateTime.Now;
                }

                uint currentClipboardSeq = GetClipboardSequenceNumber();

                if (currentClipboardSeq != lastClipboardSeq)
                {
                    lastClipboardSeq = currentClipboardSeq;

                    if (expectingSnip)
                    {
                        // Check if within timeout (e.g. 60 seconds)
                        if ((DateTime.Now - snipRequestedTime).TotalSeconds <= 60)
                        {
                            // A small delay to allow clipboard data to be fully populated by Windows
                            Thread.Sleep(200);

                            if (Clipboard.ContainsImage())
                            {
                                SaveClipboardImage();
                                expectingSnip = false; // reset after successful save
                            }
                            else
                            {
                                // The clipboard updated but did not contain an image.
                                // We keep expectingSnip = true in case Snip & Sketch clears it first,
                                // relying on the 60 second timeout or explicit Escape to cancel.
                            }
                        }
                        else
                        {
                            expectingSnip = false;
                        }
                    }
                }

                // Timeout check
                if (expectingSnip && (DateTime.Now - snipRequestedTime).TotalSeconds > 60)
                {
                    expectingSnip = false;
                }

                Thread.Sleep(50); // Sleep to prevent CPU usage
            }
        }

        static void SaveClipboardImage()
        {
            try
            {
                using (Image img = Clipboard.GetImage())
                {
                    if (img != null)
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss-fff}.png";
                        string fullPath = Path.Combine(desktopPath, fileName);
                        
                        // Create a new Bitmap to avoid any clipboard locking issues
                        using (Bitmap bmp = new Bitmap(img))
                        {
                            bmp.Save(fullPath, ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Silently ignore errors in background app
            }
        }
    }
}
