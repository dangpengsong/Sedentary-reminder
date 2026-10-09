@echo off
cd /d "%~dp0"
title Sedentary Reminder 构建脚本

set "MSBUILD=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
if not exist "%MSBUILD%" (
    echo 未找到 MSBuild：%MSBUILD%
    pause
    exit /b 1
)

tasklist /FI "IMAGENAME eq Reminder.exe" 2>nul | "%SystemRoot%\System32\find.exe" /I "Reminder.exe" >nul
if not errorlevel 1 (
    echo 检测到 Reminder 正在运行，请先从系统托盘退出，再重新运行本脚本。
    pause
    exit /b 1
)

echo === 正在编译 Release 版本（无输出即表示成功，约需十几秒） ===
"%MSBUILD%" "Reminder.sln" -p:Configuration=Release -nologo -v:quiet -clp:ErrorsOnly
if errorlevel 1 (
    echo.
    echo 编译失败，请查看上方错误信息。
    pause
    exit /b 1
)

echo === 正在更新根目录 Reminder.exe ===
copy /y "Reminder\bin\Release\Reminder.exe" "Reminder.exe" >nul
if errorlevel 1 (
    echo 复制失败，请确认 Reminder.exe 未被占用。
    pause
    exit /b 1
)

echo.
echo 构建完成：%CD%\Reminder.exe
pause
