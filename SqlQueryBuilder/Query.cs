using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryBuilder
{

    public enum Operators
    {
        Equals        = 0,
        NotEquals     = 1,
        Grather       = 2,
        Lower         = 3,
        GratherEquals = 4,
        LowerEquals   = 5
    }

    public class Query
    {
        private string _tSql;

        public string TSql
        {
            get { return _tSql; }
            set { _tSql = value; }
        }
        
        
        internal void Clear()
        {
            _tSql = "";
        }

        internal void RemoveLastComma()
        {
            _tSql  =  _tSql.TrimEnd(' ',',');
            _tSql += " ";
        }

        public override string ToString()
        {
            return _tSql;
        }

    }
}
