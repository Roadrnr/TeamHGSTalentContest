namespace TeamHGSTalentContest.Models
{
    public class Submission : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public string ManagerName { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Talent { get; set; }
        public string FileName { get; set; }
        public bool ImageConsent { get; set; }

        public bool Archive { get; set; }

    }
}
