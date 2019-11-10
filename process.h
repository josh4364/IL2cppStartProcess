#pragma once

#include <stdio.h>
#include <Windows.h>

__declspec(dllexport) DWORD StartProcess(const wchar_t* command, const wchar_t* type);
__declspec(dllexport) DWORD StartProcessHidden(const wchar_t* command, const wchar_t* type);
__declspec(dllexport) int KillProcess(DWORD pid);
__declspec(dllexport) LPTSTR GetCommandLineArgs(void);
