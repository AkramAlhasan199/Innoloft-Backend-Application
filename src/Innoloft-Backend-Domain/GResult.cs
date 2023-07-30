using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain
{
    public class GResult<TOutcome>
    {
        public bool Succeeded { get; protected set; }

        public TOutcome Outcome { get; protected set; }
       
        public new static GResult<TOutcome> Success(TOutcome result)
        {
            try
            {
                var total = (int?)typeof(TOutcome).GetProperty("TotalCount")?.GetValue(result);
                return new GResult<TOutcome> { Succeeded = true, Outcome = result };
            }
            catch
            {
                return new GResult<TOutcome> { Succeeded = true, Outcome = result };
            }
        }
    }
}
