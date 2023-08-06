using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain
{
    public class GResult<TOutcome> : GResult
    {
      
        public TOutcome Outcome { get; protected set; }

        public new static GResult<TOutcome> Success(TOutcome result)
        {
            try
            {
                var total = (int?)typeof(TOutcome).GetProperty("TotalCount")?.GetValue(result);
                return new GResult<TOutcome> { Succeeded = true, Outcome = result, Total = total };
            }
            catch
            {
                return new GResult<TOutcome> { Succeeded = true, Outcome = result };
            }
        }

        public int? Total { get; set; }

        public new static GResult<TOutcome> Failed(params string[] errors)
        {
            var result = new GResult<TOutcome> { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }
    }

    public class GResult : IEquatable<GResult>
    {
        protected List<string> _errors = new List<string>();


        public bool Succeeded { get; protected set; }
        public object ErrorOutcome { get; protected set; }

        public IEnumerable<string> Errors => _errors;

        public static GResult Success { get; } = new GResult { Succeeded = true };

        public static GResult Failed(params string[] errors)
        {
            var result = new GResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }
        public static GResult Failed(object outcome)
        {
            var result = new GResult { Succeeded = false };
            result.ErrorOutcome = outcome;
            return result;
        }

        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", Errors.ToList()));
        }

        public bool Equals(GResult other)
        {
            if (ReferenceEquals(this, null) && ReferenceEquals(other, null))
            {
                return true;
            }

            if (ReferenceEquals(this, null) || ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.Succeeded && other.Succeeded)
            {
                return true;
            }

            if (this.Errors.SequenceEqual(other.Errors))
            {
                return true;
            }
            return false;
        }
    }
}
