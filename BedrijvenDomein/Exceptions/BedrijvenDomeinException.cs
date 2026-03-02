using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenBL.Exceptions
{
    public class BedrijvenDomeinException : Exception
    {
        public List<string> Errors { get; set; }    
        public BedrijvenDomeinException()
        {
        }

        public BedrijvenDomeinException(string? message) : base(message)
        {
        }

        public BedrijvenDomeinException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public BedrijvenDomeinException(List<string> errors)
        {
            Errors = errors;
        }
    }
}
