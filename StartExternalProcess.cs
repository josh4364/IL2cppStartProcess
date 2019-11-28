#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local
// ReSharper disable MemberCanBePrivate.Local

namespace _Scripts.Utils
{
	public static class StartExternalProcess
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CreateProcessW(
			string lpApplicationName,
			[In] string lpCommandLine,
			IntPtr procSecAttrs,
			IntPtr threadSecAttrs,
			bool bInheritHandles,
			ProcessCreationFlags dwCreationFlags,
			IntPtr lpEnvironment,
			string lpCurrentDirectory,
			ref STARTUPINFO lpStartupInfo,
			ref PROCESS_INFORMATION lpProcessInformation
		);

		[StructLayout(LayoutKind.Sequential)]
		private struct PROCESS_INFORMATION
		{
			internal IntPtr hProcess;
			internal IntPtr hThread;
			internal uint dwProcessId;
			internal uint dwThreadId;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct STARTUPINFO
		{
			internal uint cb;
			internal IntPtr lpReserved;
			internal IntPtr lpDesktop;
			internal IntPtr lpTitle;
			internal uint dwX;
			internal uint dwY;
			internal uint dwXSize;
			internal uint dwYSize;
			internal uint dwXCountChars;
			internal uint dwYCountChars;
			internal uint dwFillAttribute;
			internal uint dwFlags;
			internal ushort wShowWindow;
			internal ushort cbReserved2;
			internal IntPtr lpReserved2;
			internal IntPtr hStdInput;
			internal IntPtr hStdOutput;
			internal IntPtr hStdError;
		}

		[Flags]
		private enum ProcessCreationFlags : uint
		{
			NONE = 0,
			CREATE_BREAKAWAY_FROM_JOB = 0x01000000,
			CREATE_DEFAULT_ERROR_MODE = 0x04000000,
			CREATE_NEW_CONSOLE = 0x00000010,
			CREATE_NEW_PROCESS_GROUP = 0x00000200,
			CREATE_NO_WINDOW = 0x08000000,
			CREATE_PROTECTED_PROCESS = 0x00040000,
			CREATE_PRESERVE_CODE_AUTHZ_LEVEL = 0x02000000,
			CREATE_SECURE_PROCESS = 0x00400000,
			CREATE_SEPARATE_WOW_VDM = 0x00000800,
			CREATE_SHARED_WOW_VDM = 0x00001000,
			CREATE_SUSPENDED = 0x00000004,
			CREATE_UNICODE_ENVIRONMENT = 0x00000400,
			DEBUG_ONLY_THIS_PROCESS = 0x00000002,
			DEBUG_PROCESS = 0x00000001,
			DETACHED_PROCESS = 0x00000008,
			EXTENDED_STARTUPINFO_PRESENT = 0x00080000,
			INHERIT_PARENT_AFFINITY = 0x00010000
		}

		public static uint Start(string path, string dir, bool hidden = false)
		{
			ProcessCreationFlags flags = hidden ? ProcessCreationFlags.CREATE_NO_WINDOW : ProcessCreationFlags.NONE;
			STARTUPINFO startupinfo = new STARTUPINFO
			{
				cb = (uint)Marshal.SizeOf<STARTUPINFO>()
			};
			PROCESS_INFORMATION processinfo = new PROCESS_INFORMATION();
			if (!CreateProcessW(null, path, IntPtr.Zero, IntPtr.Zero, false, flags, IntPtr.Zero, dir, ref startupinfo, ref processinfo))
			{
				throw new Win32Exception();
			}

			return processinfo.dwProcessId;
		}
	}
}
#endif
