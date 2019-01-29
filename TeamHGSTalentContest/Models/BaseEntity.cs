using System;
using System.ComponentModel.DataAnnotations;

namespace TeamHGSTalentContest.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required] 
        public Guid StringId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
    }
}
