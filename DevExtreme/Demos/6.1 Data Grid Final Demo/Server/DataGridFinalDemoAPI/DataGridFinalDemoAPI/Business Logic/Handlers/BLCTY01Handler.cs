using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using DataGridFinalDemoAPI.Models;

namespace DataGridFinalDemoAPI.Business_Logic.Handlers
{
    public class BLCTY01Handler : ICTY01Service
    {
        private readonly List<CTY01> _lstCTY01;

        public BLCTY01Handler()
        {
            _lstCTY01 = new List<CTY01>()
            {
                new() { Y01F01 = 1, Y01F02 = "Ahmedabad" },
                new() { Y01F01 = 2, Y01F02 = "Rajkot" },
                new() { Y01F01 = 3, Y01F02 = "Surat" },
                new() { Y01F01 = 4, Y01F02 = "Vadodara" },
                new() { Y01F01 = 5, Y01F02 = "Surendranagar" }
            };
        }

        public CTY01 Get(int id)
        {
            return _lstCTY01.Find(cty => cty.Y01F01 == id);
        }

        public List<CTY01> GetAll()
        {
            return _lstCTY01;
        }
    }
}
