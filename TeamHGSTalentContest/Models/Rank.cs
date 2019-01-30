namespace TeamHGSTalentContest.Models
{
    public class Rank : BaseEntity
    {
        public int Value { get; set; }

        public string RankedBy { get; set; }

        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }

    }
}
