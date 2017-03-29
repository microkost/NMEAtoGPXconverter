namespace MS_NMEAconverter
{
    public sealed class NMEAStandartSentence : NMEASentence
    {
        public TalkerIdentifiers TalkerID { get; set; }
        public SentenceIdentifiers SentenceID { get; set; }
    }
}