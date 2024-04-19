using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;

namespace TestAdministration.ViewModels;

/// <summary>
/// A factory for runtime creation of storage type-specific
/// <see cref="MainScreenViewModel"/> objects.
/// </summary>
public class MainScreenViewModelFactory(
    UserService userService,
    TestStorageFactory testStorageFactory,
    InitContentViewModel initContentViewModel
)
{
    public MainScreenViewModel Create(StorageType storageType)
    {
        var testStorage = testStorageFactory.Create(storageType);
        return new MainScreenViewModel(userService, testStorage, initContentViewModel);
    }
}