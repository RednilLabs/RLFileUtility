using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLFileUtility.Structs
{
    public struct MethodResults
    {
        public bool TrueFalse;
        public string Message;
        public object objectReturned;
        public DataTable dataTable;
        public DataSet dataSet;
    }
}
