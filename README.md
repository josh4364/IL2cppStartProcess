# IL2cppStartProcess
direct call to win api to start a process, because il2cpp doesn't support the Process class yet when converting C#.

Process.h, Process.c
Direct win32 calls to start a process, works in a il2cpp build to start a subprocess and kill it when needed. These were written in a vcpp 2017 project, so add them to one and compile it.

InternalServerProcess.cs
Example how to use it from a unity project (Its not pretty or safe, just left over from when we prototyped this).
DLLImport is called with just the dll name no extention, this allows it to find the dll correctly on linux. (Omit Extention = DLLImport works everywhere)

