using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;
using TeamHGSTalentContest.Services;
using TeamHGSTalentContest.ViewModels;

namespace TeamHGSTalentContest.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAzureStorageService _storage;
        public SelectList LocationSL { get; set; }
        public CreateModel(ApplicationDbContext context, IAzureStorageService storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IActionResult> OnGet()
        {
            var locations = await _context.Locations.OrderBy(e => e.Name).ToListAsync();
            LocationSL = new SelectList(locations,nameof(Location.Id),nameof(Location.Name), "0");
            return Page();
        }

        [BindProperty]
        public SubmissionViewModel Submission { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Submission.ErrorMessage = "ModelState is not valid.";
                return Page();
            }

            var fileName =
                await _storage.StoreAndGetFile(Submission.FormFile.FileName, "talentcontest", Submission.FormFile);
            Submission.FileName = fileName;

            var sub = new Submission
            {
                FirstName = Submission.FirstName,
                LastName = Submission.LastName,
                Email = Submission.Email,
                ManagerName = Submission.ManagerName,
                LocationId = Submission.LocationId,
                PhoneNumber = Submission.PhoneNumber,
                FileName = fileName
            };
            _context.Submissions.Add(sub);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}