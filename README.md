# Ergo Testy Jemné Motoriky

This application is intended for ergotherapists who work with tests of manual dexterity, specifically Nine Hole Peg Test, Purdue Pegboard Test, Box and Block Test.
The implementation is tightly coupled with the Czech extended versions of manuals for these tests.

## Features

The application offers many feature for test administration:
- Displaying text and audio instructions.
- Displaying rules for managing situations that may happen during testing.
- Writing measured values, notes.
- Handling trial annulment.
- Video recording.
- Calculation of standard deviation scores.
- Generation of text intended for medical documentation.
- Export of results to CSV, TXT and MP4 files.
- Interpretation of test results and their comparison.
- Settings for font size and dark mode.

## Installation

Building of the application can be done using the .NET CLI tool.
```bash
dotnet publish --configuration Release --output ./publish -r win-x64 --self-contained /p:PublishSingleFile=true
```
After that the application can be executed by anyone with Windows 10/11.

## Testing

Testing is done by using the .NET CLI tool:
```bash
dotnet test TestAdministration.sln
```
