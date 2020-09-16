using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLYNOBORDERS.SelfB2B.Framework.Object
{
    public class Result<T>
    {
        public T Data { get; set; }

        public bool HasError { get; set; }

        public string Message { get; set; }
    }
}
