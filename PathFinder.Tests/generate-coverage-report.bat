
@echo off
SETLOCAL EnableDelayedExpansion

REM Define the HTML report directory
SET "HTML_REPORT_DIR=.\CoverageReport"

REM Step 1: Run tests and create coverage reports
dotnet test --collect:"XPlat Code Coverage" --results-directory:"%HTML_REPORT_DIR%\CoverageFiles"

REM Step 2: Create a new HTML coverage report using ReportGenerator
IF NOT EXIST "%HTML_REPORT_DIR%" mkdir "%HTML_REPORT_DIR%"
reportgenerator -reports:"%HTML_REPORT_DIR%\CoverageFiles\**\coverage.cobertura.xml" -targetdir:"%HTML_REPORT_DIR%" -reporttypes:Html

echo Coverage report generated at %HTML_REPORT_DIR%
ENDLOCAL
