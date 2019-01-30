using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Faq> Faqs { get; set; }
        public List<Rule> Rules { get; set; }
        public string Body { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            Faqs = await _context.Faqs.Where(c => c.IsPublic).OrderBy(c => c.Order).ToListAsync();
            Rules = await _context.Rules.OrderBy(c => c.Order).ToListAsync();
            Body = await _context.ContestInfo.Select(c => c.Body).LastOrDefaultAsync();
            return Page();
        }
    }
}
