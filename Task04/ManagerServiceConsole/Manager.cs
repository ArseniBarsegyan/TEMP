using System;
using FileHelpers;

namespace ManagerServiceConsole
{
    public class Manager
    {
        [FieldFixedLength(5)]
        public Guid Id;

        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Both)]
        public string Name;

        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime AddedTime;
    }
}
