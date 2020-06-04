using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Schema.Models
{
    public class Values
    {
        public IList<string> value { get; set; }
    }

    public class Data
    {
        public IList<Values> values { get; set; }
    }

    public class Root
    {
        public Data data { get; set; }
    }
}
