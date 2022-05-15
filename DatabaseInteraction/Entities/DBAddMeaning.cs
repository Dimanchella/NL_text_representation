namespace NL_text_representation.DatabaseInteraction.Entities
{
    public class DBAddMeaning
    {
        public long ID { get; set; }
        public long MainMeaningID { get; set; }
        public string Meaning { get; set; }
    }
}
