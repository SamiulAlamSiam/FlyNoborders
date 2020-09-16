using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLYNOBORDERS.SelfB2B.Data;

namespace FLYNOBORDERS.SelfB2B.Repo
{
    public class BaseRepo
    {
        private SelfB2BDBContext _Context;

        public SelfB2BDBContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = new SelfB2BDBContext();

                return _Context;
            }
        }
    }
}
