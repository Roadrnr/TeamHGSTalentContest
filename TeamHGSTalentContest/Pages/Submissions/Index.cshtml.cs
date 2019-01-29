using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Services;
using TeamHGSTalentContest.ViewModels;

namespace TeamHGSTalentContest.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAzureStorageService _storage;
        private readonly ILogger _logger;

        public IndexModel(ApplicationDbContext context, IAzureStorageService storage, ILogger<IndexModel> logger)
        {
            _context = context;
            _storage = storage;
            _logger = logger;
        }

        public IList<SubmissionViewModel> Submission { get;set; }

        public async Task OnGetAsync()
        {
            var submissions = await _context.Submissions.Include(e => e.Location).Where(e => !e.Archive).ToListAsync();
            var vm = submissions.Select(e => new SubmissionViewModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                ManagerName = e.ManagerName,
                DateCreated = e.DateCreated,
                Talent = e.Talent,
                FileName = e.FileName,
                Id = e.Id,
                LocationName = e.Location.Name,
                ImageConsent = e.ImageConsent,
                Archive = e.Archive
            }).OrderByDescending(e => e.DateCreated).ToList();
            Submission = vm;
        }

        public async Task OnGetViewArchiveAsync()
        {
            var submissions = await _context.Submissions.Include(e => e.Location).Where(e => e.Archive).ToListAsync();
            var vm = submissions.Select(e => new SubmissionViewModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                ManagerName = e.ManagerName,
                DateCreated = e.DateCreated,
                Talent = e.Talent,
                FileName = e.FileName,
                Id = e.Id,
                LocationName = e.Location.Name,
                ImageConsent = e.ImageConsent,
                Archive = e.Archive
            }).OrderByDescending(e => e.DateCreated).ToList();
            Submission = vm;
        }

        public async Task<IActionResult> OnGetArchiveAsync(int id)
        {
            var archiveEntry = await _context.Submissions.SingleOrDefaultAsync(c => c.Id == id);
            archiveEntry.Archive = true;
            _context.Update(archiveEntry);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{User.Identity.Name} archived entryid: {archiveEntry.Id}");
            return RedirectToAction(nameof(OnGetAsync));
        }

        public async Task<IActionResult> OnGetRestoreAsync(int id)
        {
            var archiveEntry = await _context.Submissions.SingleOrDefaultAsync(c => c.Id == id);
            archiveEntry.Archive = false;
            _context.Update(archiveEntry);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{User.Identity.Name} restored entryid:{archiveEntry.Id}");
            return RedirectToAction(nameof(OnGetAsync));
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var archiveEntry = await _context.Submissions.SingleOrDefaultAsync(c => c.Id == id);
            var result = await _storage.DeleteFile(archiveEntry.FileName, "talentcontest");
            if (result)
            {
                _logger.LogInformation($"{User.Identity.Name} deleted {archiveEntry.FileName} from Azure");
            } else
            {
                _logger.LogWarning($"{User.Identity.Name} Unsuccessfully deleted {archiveEntry.FileName} from Azure");
            }
            
            _context.Remove(archiveEntry);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{User.Identity.Name} deleted entryId: {archiveEntry.Id}");
            return RedirectToAction(nameof(OnGetAsync));
        }

    }
}
