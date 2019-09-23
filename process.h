#pragma once

#include <stdio.h>
#include <Windows.h>

__declspec(dllexport) DWORD StartProcess(const char* command, const char* type);
__declspec(dllexport) DWORD StartProcessHidden(const char* command, const char* type);
__declspec(dllexport) int KillProcess(DWORD pid);
__declspec(dllexport) LPTSTR GetCommandLineArgs(void);
