using System.Runtime.CompilerServices;

namespace EFSamples.Services.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException(string recordType, string columnName, string duplicatedValue)
    {
        RecordType = recordType;
        ColumnName = columnName;
        DuplicatedValue = duplicatedValue;
    }

    public DuplicateRecordException(string recordType, string columnName, string duplicatedValue,
        Exception innerException) : base(message: string.Empty, innerException)
    {
        
    }

    public override string ToString()
    {
        return $"A(n) {RecordType} with {ColumnName} {DuplicatedValue} already exists.";
    }

    public string RecordType { get; set; }

    public string ColumnName { get; set; }

    public string DuplicatedValue { get; set; }
}