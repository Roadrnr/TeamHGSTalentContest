using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;
using TeamHGSTalentContest.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamHGSTalentContest.api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(Rank newRank)
        {
            if (newRank.SubmissionId == 0 && newRank.Value == 0) return BadRequest();

            var createRank = new Rank
            {
                Value = newRank.Value,
                RankedBy = User.Identity.Name,
                SubmissionId = newRank.SubmissionId
            };
            _context.Add(createRank);
            await _context.SaveChangesAsync();

            var rankCount = 0;
            var rankAverage = 0.0;
            var ranks = await _context.Rankings.Where(r => r.SubmissionId == newRank.SubmissionId).ToListAsync();
            if(ranks != null)
            {
                rankCount = ranks.Count;
                rankAverage = ranks.Select(r => r.Value).Average();
            }
            var rankVm = new RankViewModel
            {
                SubmissionId = createRank.SubmissionId,
                User = createRank.RankedBy,
                Value = createRank.Value,
                Average = rankAverage,
                RankCount = rankCount,
                UserRankId = createRank.Id
            };

            return Ok(rankVm);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, int value)
        {
            var existingRank = await _context.Rankings.SingleOrDefaultAsync(r => r.Id == id);
            if (existingRank == null) return BadRequest();

            existingRank.Value = value;
            _context.Update(existingRank);
            await _context.SaveChangesAsync();

            var rankCount = 0;
            var rankAverage = 0.0;
            var ranks = await _context.Rankings.Where(r => r.SubmissionId == existingRank.SubmissionId).ToListAsync();
            if (ranks != null)
            {
                rankCount = ranks.Count;
                rankAverage = ranks.Select(r => r.Value).Average();
            }
            var rankVm = new RankViewModel
            {
                SubmissionId = existingRank.SubmissionId,
                User = existingRank.RankedBy,
                Value = existingRank.Value,
                Average = rankAverage,
                RankCount = rankCount,
                UserRankId = existingRank.Id
            };

            return Ok(rankVm);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
