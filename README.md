# SnipX

A lightweight, native background utility that introduces automated file-saving for the Windows 10 Snip & Sketch clipboard utility (`Win + Shift + S`).

By default, Windows 10 only copies captured snips to the system clipboard, requiring manual pasting and saving via an image editor. SnipX intercepts this workflow natively, capturing the clipboard buffer and persisting it to disk automatically.

## Architecture & Features

- **Heuristic Clipboard Filtering**: SnipX selectively persists images only when the `Win + Shift + S` sequence is detected. Standard clipboard copy events (e.g., from web browsers or design software) are ignored to prevent storage bloat.
- **Asynchronous Execution**: Operates completely in the background as a headless process with zero graphical user interface (GUI) or system tray footprint.
- **Native Implementation**: Built on C# .NET leveraging low-level Win32 API hooks (`GetAsyncKeyState` and `GetClipboardSequenceNumber`) to ensure minimal latency and zero CPU idle overhead.
- **Fault-Tolerant File I/O**: Incorporates robust exception handling for file system operations. Invalid paths or permission failures silently fallback to the default directory to guarantee no data loss.

## Installation & Deployment

Two builds are provided in the Releases section to accommodate different deployment environments:

- **`SnipX-x64.exe`** (~150 KB): Framework-dependent deployment. Requires the [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
- **`SnipX_RE-x64.exe`** (~65 MB): Self-contained deployment. Pre-packaged with the .NET runtime and native dependencies. No prior framework installation required.

## Usage

1. Execute the desired binary. The process will run silently in the background.
2. Trigger the native Windows snip tool via `Win + Shift + S` and capture an area.
3. The image is immediately written to `%USERPROFILE%\Pictures\Screenshots` (or your configured target).

To terminate the process, open **Task Manager** (`taskmgr.exe`), locate the `SnipX` background process, and terminate it.

## Configuration

By default, SnipX resolves the output directory to the current user's `Pictures\Screenshots` folder. To override this behavior:

1. Create a plain text file named `savepath.txt` in the executing directory of the binary.
2. Specify the absolute path of the target directory on the first line (e.g., `D:\Output\Screenshots`).
3. Restart the `SnipX` process.

*Note: If the specified path does not exist, the application will attempt to create the directory tree recursively. If directory creation fails due to invalid characters or insufficient permissions, the application safely falls back to the default directory.*

## Build Instructions

To compile the source code, ensure the .NET 8 SDK is installed on the host machine.

```bash
# Compile standard framework-dependent binary
dotnet build -c Release

# Publish as a self-contained, single-file compressed executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true
```

## License

This project is distributed under the MIT License. See the [LICENSE](LICENSE) file for more information.
