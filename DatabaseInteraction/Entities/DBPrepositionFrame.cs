namespace NL_text_representation.DatabaseInteraction.Entities
{
    public class DBPrepositionFrame
    {
        public long ID { get; set; }
        public long PrepositionTermID { get; set; }
        public string Noun1AddMeaning { get; set; }
        public string Noun2AddMeaning { get; set; }
        public string Noun2Case { get; set; }
        public string Meaning { get; set; }
    }
}
