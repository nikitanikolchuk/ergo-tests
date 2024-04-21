namespace TestAdministration.Models.Storages.Mappers;

/// <summary>
/// A class for custom exceptions caused by invalid CSV file
/// formats or other conversion problems.
/// </summary>
public class CsvConverterException : Exception
{
    public CsvConverterException()
    {
    }

    public CsvConverterException(string message)
        : base(message)
    {
    }

    public CsvConverterException(string message, Exception inner)
        : base(message, inner)
    {
    }
}