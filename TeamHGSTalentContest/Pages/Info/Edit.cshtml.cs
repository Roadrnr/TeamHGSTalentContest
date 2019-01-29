using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages.Info
{
    public class EditModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public EditModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContestInfo ContestInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContestInfo = await _context.ContestInfo.FirstOrDefaultAsync(m => m.Id == id);

            if (ContestInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ContestInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoExists(ContestInfo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InfoExists(int id)
        {
            return _context.ContestInfo.Any(e => e.Id == id);
        }
    }
}
