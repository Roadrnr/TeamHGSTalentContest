namespace TeamHGSTalentContest.Models
{
    public class Faq : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
        public bool IsPublic { get; set; }

    }
}
