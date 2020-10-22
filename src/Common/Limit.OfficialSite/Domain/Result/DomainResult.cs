using System;
using System.Collections.Generic;
using System.Linq;

namespace Limit.OfficialSite.Domain.Result
{
    public class DomainResult
    {
        private static  DomainResult SuccessResult => new DomainResult { Succeeded = true };
        
        private readonly List<Exception> _errors = new List<Exception>();

         
        public bool Succeeded { get; private set; }

        public IEnumerable<Exception> Errors => _errors;

        public static DomainResult Success => SuccessResult;

        public static DomainResult Failed(params Exception[] errors)
        {
            var result = new DomainResult
            {
                Succeeded = false
            };

            if (errors != null)
            {
                result._errors.AddRange(errors);
            }

            return result;
        }

        public override string ToString()
        {
            return Succeeded ?
                "Succeeded" : 
                $"{"Failed"} : {string.Join(",", Errors.Select(x => x.Message).ToList())}";
        }
    }
}