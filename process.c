#include "process.h"

DWORD StartProcess(const char* dir, const char * command)
{
	PROCESS_INFORMATION pi;
	STARTUPINFO si;
	ZeroMemory(&si, sizeof(si));
	si.cb = sizeof(si);
	ZeroMemory(&pi, sizeof(pi));

	printf("Create process with (%s%s) .\n", dir, command);

	if (!CreateProcess(
		NULL,   // No module name (use command line)
		command,        // Command line
		NULL,           // Process handle not inheritable
		NULL,           // Thread handle not inheritable
		FALSE,          // Set handle inheritance to FALSE
		0,              // No creation flags
		NULL,           // Use parent's environment block
		dir,           // Use parent's starting directory 
		&si,            // Pointer to STARTUPINFO structure
		&pi)           // Pointer to PROCESS_INFORMATION structure
		)
	{
		return GetLastError();
	}


	return pi.dwProcessId;
}

DWORD StartProcessHidden(const char* dir, const char * command)
{
	PROCESS_INFORMATION pi;
	STARTUPINFO si;
	ZeroMemory(&si, sizeof(si));
	si.cb = sizeof(si);
	ZeroMemory(&pi, sizeof(pi));

	printf("Create process with (%s%s) .\n", dir, command);

	if (!CreateProcess(
		NULL,   // No module name (use command line)
		command,        // Command line
		NULL,           // Process handle not inheritable
		NULL,           // Thread handle not inheritable
		FALSE,          // Set handle inheritance to FALSE
		CREATE_NO_WINDOW,
		NULL,           // Use parent's environment block
		dir,           // Use parent's starting directory 
		&si,            // Pointer to STARTUPINFO structure
		&pi)           // Pointer to PROCESS_INFORMATION structure
		)
	{
		return GetLastError();
	}


	return pi.dwProcessId;
}

LPTSTR GetCommandLineArgs()
{
	return GetCommandLine();
}


int KillProcess(DWORD pid)
{
	HANDLE phandle = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid);

	if (!phandle)
	{
		printf("Getting the process with pid (%d) failed with code (%d).\n", pid, GetLastError());
		return -1;
	}
	if (!TerminateProcess(phandle, 0))
	{
		printf("Killing failed (%d).\n", GetLastError());
	}
	CloseHandle(phandle);
	return 0;
}
