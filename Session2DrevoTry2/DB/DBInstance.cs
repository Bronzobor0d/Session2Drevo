using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2DrevoTry2.DB
{
    public class DBInstance
    {
        private static _2025rchContext instance;

        public static _2025rchContext GetInstance()
        {
            if (instance == null)
                instance = new _2025rchContext();
            return instance;
        }
    }
}
