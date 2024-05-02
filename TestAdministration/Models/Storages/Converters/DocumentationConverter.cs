using System.ComponentModel;
using System.Globalization;
using TestAdministration.Models.Data;
using static TestAdministration.Models.Storages.Records.CsvRecordConfiguration;

namespace TestAdministration.Models.Storages.Converters;

/// <summary>
/// A class for converting test results to text form intended
/// for copying to medical documentation.
/// </summary>
public class DocumentationConverter
{
    public string Convert(Test test) => test.Type switch
    {
        TestType.Nhpt => _nhptTemplate(test),
        TestType.Ppt => _pptTemplate(test),
        TestType.Bbt => _bbtTemplate(test),
        _ => throw new InvalidEnumArgumentException(
            nameof(test.Type),
            System.Convert.ToInt32(test.Type),
            typeof(TestType)
        )
    };

    private static string _nhptTemplate(Test test) =>
        $"""
         Devítikolíkový test (NHPT):
         Jedná se o standardizovaný test hodnotící jemnou motoriku. Úkolem testované osoby je co nejrychleji umístit 9 kolíků do otvorů v testovací desce a zase je vrátit zpět do zásobníku v co nejrychlejším čase.

         Dominantní HK (dle psaní):
         {_sectionTemplate("sekundy", test, 0, false)}

         Nedominantní HK (dle psaní):
         {_sectionTemplate("sekundy", test, 1, false)}

         Psaný komentář:
         {CsvConversionHelper.CreateNotes(test)}
         """;

    private static string _pptTemplate(Test test) =>
        $"""
         Purdue Pegboard Test (PPT):
         Jedná se o standardizovaný test hodnotící jemnou motoriku, který simuluje manuální práci v továrně. Úkolem testované osoby je v dílčích subtestech vždy za určitý čas správně umístit přesně dle instrukcí co nejvíce součástek na testovací desku.

         Dominantní HK (dle psaní):
         {_sectionTemplate("počet kolíků", test, 0, true)}

         Nedominantní HK (dle psaní):
         {_sectionTemplate("počet kolíků", test, 1, true)}

         Obě HK:
         {_sectionTemplate("počet párů součástek", test, 2, true)}

         DOMINANTNÍ + NEDOMINANTNÍ + OBĚ RUCE:
         {_sectionTemplate("počet součástek", test, 3, true)}

         Kompletování:
         {_sectionTemplate("počet součástek", test, 4, true)}

         Psaný komentář:
         {CsvConversionHelper.CreateNotes(test)}
         """;

    private static string _bbtTemplate(Test test) =>
        $"""
         Box and Block Test (BBT):
         Jedná se o standardizovaný test hodnotící jemnou a hrubou motoriku. Úkolem testované osoby je za určitý čas přemístit přesně dle instrukcí co nejvíce kostek z jedné strany testovací krabice přes přepážku na její druhou stranu.

         Dominantní HK (dle psaní):
         {_sectionTemplate("počet kostek", test, 0, false)}

         Nedominantní HK (dle psaní):
         {_sectionTemplate("počet kostek", test, 1, false)}

         Psaný komentář:
         {CsvConversionHelper.CreateNotes(test)}
         """;

    private static string _sectionTemplate(string valueType, Test test, int section, bool isZeroIndexed)
    {
        var offset = isZeroIndexed ? 0 : 1;

        return $"""
                pokus	výsledek ({valueType})	SDS	(interpretace dle normy)
                {_trialRowTemplate(test, section, 0, offset)}
                {_trialRowTemplate(test, section, 1, offset)}
                {_trialRowTemplate(test, section, 2, offset)}
                {_averageRowTemplate(test, section)}
                """;
    }

    private static string _trialRowTemplate(Test test, int section, int trial, int offset) =>
        $"{trial + 1}." + '\t' +
        _formatValue(test.Sections[section].Trials[trial + offset].Value) + '\t' +
        _formatValue(test.Sections[section].Trials[trial + offset].SdScore) + '\t' +
        _formatValue(test.Sections[section].Trials[trial + offset].NormDifference);

    private static string _averageRowTemplate(Test test, int section) =>
        "průměr:" + '\t' +
        _formatValue(test.Sections[section].AverageValue) + '\t' +
        _formatValue(test.Sections[section].AverageSdScore) + '\t' +
        _formatValue(test.Sections[section].AverageNormDifference);

    private static string _formatValue(float? value) =>
        value is not null
            ? value.Value.ToString(FloatFormat, new CultureInfo(Culture))
            : string.Empty;
}