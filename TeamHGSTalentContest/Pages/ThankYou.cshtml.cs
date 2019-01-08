using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.ViewModels;

namespace TeamHGSTalentContest.Pages
{
    public class ThankYouModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public ThankYouModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public SubmissionViewModel Submission { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var sub = await _context.Submissions.Include(m => m.Location).FirstOrDefaultAsync(m => m.StringId.ToString() == id);
            Submission = new SubmissionViewModel
            {
                Id = sub.Id,
                Talent = sub.Talent,
                Email = sub.Email,
                FileName = sub.FileName,
                FirstName = sub.FirstName,
                LastName = sub.LastName,
                LocationName = sub.Location.Name,
                ManagerName = sub.ManagerName,
                PhoneNumber = sub.PhoneNumber,
                DateCreated = sub.DateCreated
            };

            if (Submission == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
