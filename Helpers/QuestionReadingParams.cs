using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_HOCTIENGANH.Helpers
{
    public class QuestionReadingParams : PaginationParams
    {
        public string ToeicNumber { get; set; }
        public string ToeicPart { get; set; }
    }
}
