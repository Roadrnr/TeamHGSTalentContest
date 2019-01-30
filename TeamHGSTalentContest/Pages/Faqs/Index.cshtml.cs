﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages.Faqs
{
    public class IndexModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public IndexModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Faq> Faq { get;set; }

        public async Task OnGetAsync()
        {
            Faq = await _context.Faqs.OrderBy(r => r.Order).ToListAsync();
        }
    }
}
