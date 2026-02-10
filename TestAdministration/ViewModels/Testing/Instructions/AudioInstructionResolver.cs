using TestAdministration.Models.Data;
using TestAdministration.Models.Services;

namespace TestAdministration.ViewModels.Testing.Instructions;

/// <summary>
/// A class for creating <see cref="InstructionPlayerViewModel"/>
/// objects for specific instructions. The class encapsulates
/// data about an instruction page for easier audio instruction
/// resolving in view models.
/// </summary>
public class AudioInstructionResolver(
    AudioInstructionService audioService,
    TestType testType,
    Patient patient,
    int section,
    int trial
)
{
    public InstructionPlayerViewModel GetVolumeCheck(InstructionPlayerViewModel? nextPlayer = null) =>
        new(audioService, AudioInstructionService.VolumeCheckFilePath, nextPlayer);

    public InstructionPlayerViewModel Get(
        int index,
        bool isGendered = false,
        int? trialCount = null,
        InstructionPlayerViewModel? nextPlayer = null
    )
    {
        bool? isMale = isGendered ? patient.IsMale : null;
        var filePath = AudioInstructionService.GetFilePath(
            testType, patient.DominantHand, section, trial, index, isMale, trialCount
        );

        return new InstructionPlayerViewModel(audioService, filePath, nextPlayer);
    }
}