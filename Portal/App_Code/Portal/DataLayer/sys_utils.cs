using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace DataLayer
{

    public class sys_utils
    {
        private string db_pchar = "?";

        public sys_utils()
        {
        }

        internal bool IsNameUniqueByClient(string database, Guid client_id, string table, string pk_name, Guid pk_value, string column_name, string value)
        {
            SPA.spaDatabase DB = new SPA.spaDatabase(database);
            db_pchar = DB.GetParameterCharacter();

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));

            string SQL = @"
SELECT      *
FROM        " + table + @"
WHERE       client_id = " + db_pchar + @"client_id
AND         " + pk_name + @" <> '" + pk_value.ToString() + @"'
AND         " + column_name + @" = '" + value + @"'
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal bool IsNameUnique(string database, string table, string pk_name, Guid pk_value, string column_name, string value)
        {
            SPA.spaDatabase DB = new SPA.spaDatabase(database);
            db_pchar = DB.GetParameterCharacter();

            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        " + table + @"
WHERE       " + pk_name + @" <> '" + pk_value.ToString() + @"'
AND         " + column_name + @" = '" + value + @"'
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal bool HasDependancies(string database, string table, string column_name, Guid value)
        {
            SPA.spaDatabase DB = new SPA.spaDatabase(database);
            db_pchar = DB.GetParameterCharacter();

            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        " + table + @"
WHERE       " + column_name + @" = '" + value.ToString() + @"'
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }



        internal void DeleteDependancies(string database, string table, string column_name, Guid value)
        {
            SPA.spaDatabase DB = new SPA.spaDatabase(database);
            db_pchar = DB.GetParameterCharacter();

            ArrayList myParams = new ArrayList();

            string SQL = @"
DELETE
FROM        " + table + @"
WHERE       " + column_name + @" = '" + value.ToString() + @"'
";

            DB.ExecuteSQL(SQL, myParams);
        }
    }
}