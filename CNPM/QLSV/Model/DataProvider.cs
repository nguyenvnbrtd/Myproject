using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public QLSVEntities DB { get; set; }

        public static DataProvider Ins {
            get {
                if (_ins == null) _ins = new DataProvider();
                    return _ins;
            }
            set {
                _ins = value;
            }
        }
        private DataProvider() { DB = new QLSVEntities(); }

    }
}
