using TestAdministration.Models.Data;
using TestAdministration.Models.Services;
using TestAdministration.Models.Storages;
using TestAdministration.Models.Storages.Converters;

namespace TestAdministration.ViewModels.Results;

public class ResultsViewModelFactory(
    ITestStorage testStorage,
    DocumentationConverter documentationConverter,
    AgeCalculatorService ageCalculatorService
)
{
    public ResultsViewModel Create(Patient patient, Test test, Action<Patient, Test, List<string>> onSaveTest)
    {
        var patientAge = ageCalculatorService.Calculate(patient);
        var previousTest = testStorage.GetLastTestByPatientId(test.Type, patient.Id);

        return new ResultsViewModel(
            documentationConverter,
            patient,
            patientAge,
            test,
            previousTest,
            onSaveTest
        );
    }
}