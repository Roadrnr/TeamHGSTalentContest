using System.IO;
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
        public SelectList LocationSl { get; set; }
        public CreateModel(ApplicationDbContext context, IAzureStorageService storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IActionResult> OnGet()
        {
            var locations = await _context.Locations.OrderBy(e => e.Name).ToListAsync();
            LocationSl = new SelectList(locations,nameof(Location.Id),nameof(Location.Name), "0");
            return Page();
        }

        [BindProperty]
        public SubmissionViewModel Submission { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var supportedTypes = new[] { "mp4", "webm", "ogg"};
            var fileExt = Path.GetExtension(Submission.FormFile.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
            {
                ModelState.AddModelError("Submission.FileName","File must be in mp4, webm, or ogg format.");
            }

            if (!Submission.ImageConsent)
            {
                ModelState.AddModelError("Submission.ImageConsent", "You must agree to the image consent.");
            }

            if (!ModelState.IsValid)
            {
                Submission.FileName = Submission.FormFile.FileName;
                var locations = await _context.Locations.OrderBy(e => e.Name).ToListAsync();
                LocationSl = new SelectList(locations, nameof(Location.Id), nameof(Location.Name), "0");
                Submission.ErrorMessage = "Your entry was not successful.";
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
                FileName = fileName,
                Talent = Submission.Talent,
                ImageConsent = Submission.ImageConsent
            };
            _context.Submissions.Add(sub);
            await _context.SaveChangesAsync();
            var guid = sub.StringId;
            return RedirectToPage("ThankYou", new { id = guid});
        }
    }
}