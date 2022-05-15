namespace NL_text_representation.DatabaseInteraction.Entities
{
    public class DBVerbPrepositionFrame
    {
        public long ID { get; set; }
        public string VerbMeaning { get; set; }
        public string VerbForm { get; set; }
        public string VerbReflection { get; set; }
        public string VerbVoice { get; set; }
        public long PrepositionTermID { get; set; }
        public string NounCase { get; set; }
        public string Meaning { get; set; }
    }
}
