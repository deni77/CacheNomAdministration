using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheNomAdministration.WebApi.Models
{
    public enum ResultType
    {
        OK = 0,
        Warning = 1,
        Error = 2
    }

    public class ResultOfAction
    {
        public ResultType Type { get; set; }
        public string Message { get; set; }

        public string Decoration
        {
            get
            {
                switch (this.Type)
                {
                    case ResultType.OK:
                        return "success";
                    case ResultType.Warning:
                        return "warning";
                    case ResultType.Error:
                        return "danger";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}