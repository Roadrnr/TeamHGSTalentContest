namespace TeamHGSTalentContest.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
