﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;

namespace TeamHGSTalentContest.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get;set; }

        public async Task OnGetAsync()
        {
            Location = await _context.Locations.ToListAsync();
        }
    }
}
