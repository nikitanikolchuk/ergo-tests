using TestAdministration.Models.Services;
using Wpf.Ui;

namespace TestAdministration.ViewModels;

public class SettingsViewModelFactory(
    IContentDialogService contentDialogService,
    ConfigurationService configurationService,
    VideoRecorderService videoRecorderService
)
{
    public SettingsViewModel Create() => new(
        contentDialogService,
        configurationService,
        videoRecorderService
    );
}