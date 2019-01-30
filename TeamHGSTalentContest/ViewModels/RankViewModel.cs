using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamHGSTalentContest.ViewModels
{
    public class RankViewModel
    {
        public int SubmissionId { get; set; }

        public int UserRankId { get; set; }

        public string User { get; set; }

        public int Value { get; set; }

        public double Average { get; set; }

        public int RankCount { get; set; }

    }
}
