using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using TestAdministration.Models.Services;
using Wpf.Ui.Input;

namespace TestAdministration.ViewModels.Results;

/// <summary>
/// A view model for picking files to be added to the test data
/// directory.Is intended for video files but accepts all file
/// types.
/// </summary>
public class ResultFilesBoxViewModel : ViewModelBase
{
    private const string RecordingName = "Nahrávka";

    public List<string> FilePaths { get; } = _getFilePaths();
    public List<string> FileNames => _getFileNames();
    public bool IsEmpty => FilePaths.Count == 0;
    public bool IsNotEmpty => !IsEmpty;

    public ICommand OnAddFiles => new RelayCommand<object?>(_ => _onAddFiles());
    public ICommand OnRemoveFile => new RelayCommand<string>(_onRemoveFile);

    private static List<string> _getFilePaths() =>
        File.Exists(VideoRecorderService.TempRecordingFilePath)
            ? [VideoRecorderService.TempRecordingFilePath]
            : [];

    private List<string> _getFileNames() =>
        FilePaths
            .Select(path =>
                path == VideoRecorderService.TempRecordingFilePath
                    ? RecordingName
                    : Path.GetFileName(path)
            )
            .ToList();

    private void _onAddFiles()
    {
        OpenFileDialog openFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Filter = "Video Soubory (*.mp4,*.avi,*.mov,*.wmv,*.mkv,*.flv)|*.mp4;*.avi;*.mov;*.wmv;*.mkv;*.flv|" +
                     "Všechny soubory|*.*",
            Multiselect = true
        };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (openFileDialog.FileNames.Length == 0)
        {
            return;
        }

        foreach (var fileName in openFileDialog.FileNames)
        {
            if (fileName is null)
            {
                continue;
            }

            FilePaths.Add(fileName);
            OnPropertyChanged(nameof(FileNames));
            OnPropertyChanged(nameof(IsEmpty));
            OnPropertyChanged(nameof(IsNotEmpty));
        }
    }

    private void _onRemoveFile(string? name)
    {
        if (name is null)
        {
            return;
        }

        var filePath = FilePaths.Find(path =>
            path == VideoRecorderService.TempRecordingFilePath || Path.GetFileName(path) == name
        );
        if (filePath is null)
        {
            return;
        }

        FilePaths.Remove(filePath);
        OnPropertyChanged(nameof(FileNames));
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(IsNotEmpty));
    }
}