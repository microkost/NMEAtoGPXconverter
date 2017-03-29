namespace MS_NMEAconverter
{
    public sealed class NMEAProprietarySentence : NMEASentence
    {
        public string SentenceIDString { get; set; }
        public ManufacturerCodes Manufacturer { get; set; }
    }
}