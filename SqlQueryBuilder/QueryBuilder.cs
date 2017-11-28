using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlQueryBuilder
{
    public static class QueryBuilder
    {

        private const string SELECT      = " SELECT ";
        private const string FROM        = " FROM ";
        private const string WHERE       = " WHERE ";
        private const string AND         = " AND ";
        private const string OR          = " OR ";
                                         
        private const string UPDATE      = " UPDATE ";
        private const string SET         = " SET ";

        private const string INSERT_INTO = " INSERT INTO ";
        private const string VALUES      = " VALUES ";

        private const string GROUP_BY    = " GROUP BY ";
        private const string HAVING      = " HAVING ";

        private static string GetOperator(Operators op)
        {
            switch (op)
            {
                case Operators.Equals:
                    return " = ";
                case Operators.NotEquals:
                    return " != ";
                case Operators.Grather:
                    return " > ";
                case Operators.Lower:
                    return " < ";
                case Operators.GratherEquals:
                    return " >= ";
                case Operators.LowerEquals:
                    return " <= ";
                default:
                    return " = ";
            }
        }

        #region ' SELECT '
        public static Query Select(this Query query)
        {
            query.Clear();

            query.TSql = SELECT + " * ";

            return query;
        }

        public static Query Select(this Query query,params string[] fields)
        {
            query.Clear();

            query.TSql = SELECT;

            for (int i = 0; i < fields.Count(); i++)
                query.TSql += fields[i] + " , ";

            query.RemoveLastComma();

            return query;
        }
        #endregion

        #region ' FROM '

        public static Query From(this Query query, params string[] tables)
        {
            query.TSql += FROM;

            for (int i = 0; i < tables.Count(); i++)
                query.TSql += tables[i] + " , ";

            query.RemoveLastComma();

            return query;
        }

        #endregion

        #region ' WHERE '

        public static Query Where(this Query query, string field, Operators op)
        {
            query.TSql += WHERE + field + GetOperator(op) + $" @{field} ";
            return query;
        }

        #endregion

        #region ' AND_OR '

        public static Query And(this Query query,string field,Operators opt)
        {
            query.TSql += AND + field + GetOperator(opt) + $" @{field} ";
            return query;
        }

        public static Query Or(this Query query, string field, Operators opt)
        {
            query.TSql += OR + field + GetOperator(opt) + $" @{field} ";
            return query;
        }

        public static Query And(this Query query)
        {
            query.TSql += AND;
            return query;
        }
        public static Query Or(this Query query)
        {
            query.TSql += OR;
            return query;
        }

        #endregion

        #region ' SCOPS '

        public static Query BeginScope(this Query query,string tag = " ( ")
        {
            query.TSql += tag;
            return query;
        }
        public static Query EndScope(this Query query,string tag = " ) ")
        {
            query.TSql += tag;
            return query;
        }

        #endregion

        #region ' UPDATE '

        public static Query Update(this Query query,string tableName,params string[] fields)
        {
            query.Clear();

            query.TSql += $" {UPDATE} {tableName} {SET} ";

            for (int i = 0; i < fields.Count(); i++)
                query.TSql += $"{fields[i]} {GetOperator(Operators.Equals)} @{fields[i]} , ";

            query.RemoveLastComma();
            return query;
                    
        }

        #endregion

        #region ' INSERT '

        public static Query Insert(this Query query,string tableName,params string[] fields)
        {
            query.Clear();

            query.TSql = $" {INSERT_INTO} {tableName} ";

            query.BeginScope();

                for (int i = 0; i < fields.Count(); i++)
                    query.TSql += $" {fields[i]} , ";

                query.RemoveLastComma();

            query.EndScope();

            query.TSql += VALUES;

            query.BeginScope();

                for (int i = 0; i < fields.Count(); i++)
                    query.TSql += $" @{fields[i]} , ";

                query.RemoveLastComma();

            query.EndScope();

            return query;
        }

        #endregion

        #region ' GROUP BY '

        public static Query GroupBy(this Query query, params string[] fields)
        {
            query.TSql += GROUP_BY;

            query.BeginScope();

                for (int i = 0; i < fields.Count(); i++)
                    query.TSql += fields[i] + " , ";

                query.RemoveLastComma();
            

            query.EndScope();

            return query;
        }

        #endregion

        #region ' HAVING '

        public static Query Having(this Query query,string field,Operators opt)
        {
            query.TSql += $" {HAVING}  {field}  {GetOperator(opt)} @{field}";

            return query;
        }

        #endregion

    }
}
