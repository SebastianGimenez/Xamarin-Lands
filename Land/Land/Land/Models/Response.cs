using System;
using System.Collections.Generic;
using System.Text;

namespace Land.Models
{
    public class Response
    {
        public bool IsSucced
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public object Result
        {
            get;
            set;
        }
    }
}
