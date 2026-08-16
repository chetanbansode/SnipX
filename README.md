# SnipX 📸

**The missing auto-save feature for Windows 10 Snip & Sketch.**

If you use Windows 10, you probably love the `Win + Shift + S` shortcut for quick screenshots. But there's a catch: Windows copies the snip to your clipboard, but **it doesn't save it as a file automatically**. You have to open an image editor, paste it, and manually save it every single time. 

**SnipX** (X for Windows 10) runs invisibly in the background and fixes this natively. It effortlessly catches your `Win + Shift + S` screenshots and immediately saves them as PNG files directly to your **Pictures\Screenshots** folder.

## ✨ Features
- **Smart Filtering**: Only saves actual screenshots triggered by `Win + Shift + S`. It completely ignores normal images you copy from Chrome, Photoshop, or WhatsApp, keeping your computer clutter-free.
- **Zero Interruption**: Your screenshots are still copied to your clipboard normally, so you can paste them immediately into your chats or documents, while a backup is quietly saved.
- **Ultra-Lightweight**: Uses 0% CPU and a microscopic amount of memory. It runs completely invisibly with no system tray icon or annoying console window.
- **Native & Efficient**: Built in C# .NET using lightweight Win32 API hooks (`GetAsyncKeyState` and `GetClipboardSequenceNumber`), meaning it won't trigger anti-cheat software or cause input lag.

## 🚀 Which version should I download?
Head over to the Releases tab and choose the version that fits your needs:
- **`SnipX-x64.exe`** (Tiny ~150KB): Requires the **.NET 8 Desktop Runtime** installed on your PC.
- **`SnipX_RE-x64.exe`** (Large ~65MB): A fully self-contained package. No installation or .NET required. Just download and run!

## 🚀 How to use
1. Download and run your preferred `.exe` from the Releases page. (Nothing will appear on your screen, it runs silently in the background).
2. Press `Win + Shift + S` and take a snip.
3. Check your **Pictures\Screenshots** folder! You'll find a new `Screenshot_YYYY-MM-DD...png` waiting for you.

To stop the background process, open **Task Manager** (`Ctrl + Shift + Esc`), find `SnipX` in your Background Processes, and click "End task".

## ⚙️ Custom Save Directory
By default, SnipX saves screenshots to your **Pictures\Screenshots** folder. If you want to change this to a custom folder (e.g. `D:\MySnips`):
1. Create a simple text file named `savepath.txt` in the exact same folder where your `SnipX.exe` is located.
2. Open `savepath.txt` and paste the full path of your desired folder on the first line.
3. Restart SnipX!

## 🛠️ Building from source
Make sure you have the .NET 8 SDK installed.

```bash
# Build normally (Framework Dependent)
dotnet build -c Release

# Build a standalone compressed executable (No .NET installation required for end-users)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true
```

## 🤝 Contributing
Contributions, issues, and feature requests are welcome! Feel free to check the issues page.

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
