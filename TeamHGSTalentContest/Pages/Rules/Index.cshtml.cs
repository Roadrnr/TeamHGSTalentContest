using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages.Rules
{
    public class IndexModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public IndexModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Rule> Rule { get;set; }

        public async Task OnGetAsync()
        {
            Rule = await _context.Rules.ToListAsync();
        }
    }
}
