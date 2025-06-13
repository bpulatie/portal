using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Web;
using log4net;

namespace SPA
{
    class msSQL : SPA.spaDB
    {
        ILog logger = log4net.LogManager.GetLogger("SPALog");
        private string myMoniker = "msSQL";
        private string _connectionString = string.Empty;
        private List<spaColumn> spaColumns = new List<spaColumn>();

        public msSQL(string database_connection)
            : base(database_connection)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[database_connection].ConnectionString;
        }

        public msSQL(string database_connection, string database_table)
            : base(database_connection, database_table)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[database_connection].ConnectionString;

            spaColumns = GetTableMetaData(database_table);
        }


        public override List<spaColumn> GetTableMetaData(string Table)
        {
            logger.Debug(myMoniker + ".GetTableMetaData: Starting");

            List<spaColumn> spaColumns = new List<spaColumn>();

            ArrayList Params = new ArrayList();

            string SQL = @"
SELECT		c.TABLE_NAME as table_name, c.COLUMN_NAME as column_name, c.DATA_TYPE as data_type, 
			c.IS_NULLABLE as nullable,
			coalesce(OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey'),0) as primary_key,
			c.COLUMN_DEFAULT as default_value
FROM		information_schema.columns c
LEFT JOIN	INFORMATION_SCHEMA.KEY_COLUMN_USAGE k
ON			c.table_name = k.table_name
AND			c.COLUMN_NAME = k.COLUMN_NAME
AND			k.CONSTRAINT_NAME like 'PK%'
WHERE		c.table_name = '" + Table + "'";

            SqlDataReader myDR = GetReader(SQL, Params);

            while (myDR.Read())
            {
                spaColumn col = new spaColumn();
                col.Table = Table;
                col.Field = myDR["column_name"].ToString();
                col.Type = myDR["data_type"].ToString();
                if (myDR["primary_key"].ToString() == "1")
                    col.Key = "PK";
                else
                    col.Key = string.Empty;
                col.Default = myDR["default_value"].ToString();
                col.Extra = string.Empty;

                spaColumns.Add(col);
            }
            myDR.Close();

            logger.Debug(myMoniker + ".GetTableMetaData: Ending");

            return spaColumns;
        }

        public SqlDataReader GetReader(string SQL, ArrayList Params)
        {
            SqlDataReader result;

            try
            {
                // Create Instance of Connection and Command Object
                SqlConnection myConnection = new SqlConnection(_connectionString);
                SqlCommand myCommand = new SqlCommand("", myConnection);

                myCommand.CommandText = SQL;

                // Add any parameters
                myCommand.Parameters.Clear();
                myCommand.Parameters.AddRange(Params.ToArray());

                // Execute the command
                myConnection.Open();
                result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

                myCommand.Parameters.Clear();

            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".GetReader: Error - " + ex.Message);
                logger.Error(myMoniker + ".GetReader: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error(myMoniker + ".GetReader: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in GetReader"));
            }

            // Return the datareader 
            return result;
        }

        public override DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

            try
            {
                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter myCommand = new SqlDataAdapter("", myConnection))
                    {
                        myCommand.SelectCommand.CommandText = SQL;

                        // Add any parameters
                        myCommand.SelectCommand.Parameters.Clear();
                        myCommand.SelectCommand.Parameters.AddRange(Params.ToArray());

                        // Create and Fill the DataSet
                        myCommand.Fill(result);

                        myCommand.SelectCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".GetDataSet: Error - " + ex.Message);
                logger.Error(myMoniker + ".GetDataSet: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error(myMoniker + ".GetDataSet: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in GetDataSet"));
            }

            // Return the DataSet
            return result;
        }

        public override List<T> GetList<T>(string SQL, ArrayList Params)
        {
            List<T> result = new List<T>();

            Type t = typeof(T);
            
            DataSet ds = GetDataSet(SQL, Params);

            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                object o = Activator.CreateInstance(t);
                myFill(o, ds, x);
                result.Add((T)o);
            }

            return result;
        }

        public override int ExecuteSQL(string SQL, ArrayList Params)
        {
            int result = 0;

            try
            {
                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("", myConnection))
                    {
                        // The SQL
                        myCommand.CommandText = SQL;

                        // Add any parameters
                        myCommand.Parameters.AddRange(Params.ToArray());

                        // Execute the command
                        myConnection.Open();
                        result = myCommand.ExecuteNonQuery();

                        myCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".ExecuteSQL: Error - " + ex.Message);
                logger.Error(myMoniker + ".ExecuteSQL: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error(myMoniker + ".ExecuteSQL: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in ExecuteSQL"));
            }

            return result;
        }

        public override int ExecuteStoredProcedure(string SQL, ArrayList Params)
        {
            int spTimeout = 30;

            try
            {
                spTimeout = Int32.Parse(ConfigurationManager.AppSettings["spTimeout"].ToString());
            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".ExecuteStoredProcedure: Error converting spTimeout - " + ex.Message);
            }

            int result = 0;

            try
            {
                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("", myConnection))
                    {
                        // The SQL
                        myCommand.CommandText = SQL;

                        // Add any parameters
                        myCommand.Parameters.AddRange(Params.ToArray());

                        // Set the command timeout - default 30 secs override in webconfig
                        myCommand.CommandTimeout = spTimeout;
                        logger.Error(myMoniker + ".ExecuteStoredProcedure: spTimeout - " + myCommand.CommandTimeout.ToString());

                        // Execute the command
                        myConnection.Open();
                        result = myCommand.ExecuteNonQuery();

                        myCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".ExecuteStoredProcedure: Error - " + ex.Message);
                logger.Error(myMoniker + ".ExecuteStoredProcedure: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error(myMoniker + ".ExecuteStoredProcedure: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in ExecuteStoredProcedure"));
            }

            return result;
        }

        public override DataSet GetPagedDataSet(string SQL, ArrayList Params, int pageNo, int rows)
        {
            string offSet = string.Empty;
            string count_sql = GetCountSQL(SQL);

            if (pageNo > 1 || rows > 1)
            {
                offSet = @"
OFFSET ((" + (pageNo - 1).ToString() + ") * " + rows.ToString() + @") ROWS
FETCH NEXT " + rows.ToString() + @" ROWS ONLY; 
";
            }

            string sql = SQL + offSet + count_sql;
            
            DataSet result = new DataSet();

            try
            {
                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter myCommand = new SqlDataAdapter("", myConnection))
                    {
                        myCommand.SelectCommand.CommandText = sql;
                        myCommand.SelectCommand.CommandTimeout = 60;

                        // Add any parameters
                        myCommand.SelectCommand.Parameters.AddRange(Params.ToArray());

                        // Create and Fill the DataSet
                        myCommand.Fill(result);

                        myCommand.SelectCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(myMoniker + ".GetPagedDataSet: Error - " + ex.Message);
                logger.Error(myMoniker + ".GetPagedDataSet: SQL - " + sql);
                foreach (SqlParameter p in Params)
                {
                    logger.Error(myMoniker + ".GetPagedDataSet: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in GetPagedDataSet"));
            }

            // Return the DataSet
            return result;
        }

        public override string GetParameterCharacter()
        {
            return "@";
        }

        public override object CreateParameter(string name, Type myType, object value)
        {
            // Create Parameter
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@" + name;

            //Set the data type for the parameter
            if (myType == typeof(Nullable<int>) || myType == typeof(int))
            {
                param.SqlDbType = SqlDbType.Int;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<DateTime>) || myType == typeof(DateTime))
            {
                param.SqlDbType = SqlDbType.DateTime;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<bool>) || myType == typeof(bool))
            {
                param.SqlDbType = SqlDbType.Char;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                {
                    if ((Boolean)value == true)
                        param.Value = 1;
                    else
                        param.Value = 0;
                }
            }
            else if (myType == typeof(Nullable<decimal>) || myType == typeof(decimal))
            {
                param.SqlDbType = SqlDbType.Decimal;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<Byte>[]) || myType == typeof(Byte[]))
            {
                param.SqlDbType = SqlDbType.Image;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else
            {
                param.SqlDbType = SqlDbType.VarChar;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value.ToString();
            }

            return (object)param;
        }

        public override object Get(dynamic myObject)
        {
            DataSet ds = _Get(myObject);

            if (ds.Tables[0].Rows.Count > 0)
            {
                myFill(myObject, ds, 0);
                return myObject;
            }

            return myObject;
        }

        public override object Save(dynamic myObject)
        {
            DataSet ds = _Get(myObject);

            if (ds.Tables[0].Rows.Count > 0)
            {
                _Update(myObject, ds);
            }
            else
            {
                _Insert(myObject);
            }

            return myObject;
        }

        public override object Delete(dynamic myObject)
        {
            ArrayList myParams = new ArrayList();
            string myKeyList = string.Empty;

            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (PropertyInfo info in fieldInfo)
            {
                foreach (spaColumn col in spaColumns)
                {
                    if (col.Field.ToUpper() == info.Name.ToUpper() && col.Key == "PK")
                    {
                        myKeyList += " AND " + info.Name + " = @" + info.Name;
                        myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                    }
                }
            }

            string SQL = "DELETE FROM " + myObject.GetType().Name + " ";
            SQL += "WHERE 1=1 " + myKeyList;

            ExecuteSQL(SQL, myParams);

            // Clear out the object?
            foreach (PropertyInfo info in fieldInfo)
            {
                info.SetValue(myObject, null, null);
            }

            return myObject;
        }

        private void myFill(dynamic myObject, DataSet ds, int row)
        {
            PropertyInfo myProperty;
            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                myProperty = myObject.GetType().GetProperty(col.ColumnName);

                if (myProperty != null)
                {
                    if (ds.Tables[0].Rows[row][col.ColumnName] == DBNull.Value)
                    {
                        bool canBeNull = !myProperty.PropertyType.IsValueType ||
                                          myProperty.PropertyType.IsGenericType &&
                                          myProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                        if (canBeNull)
                            myProperty.SetValue(myObject, null, null);
                    }
                    else
                    {
                        switch (myProperty.PropertyType.Name)
                        {
                            default:
                                myProperty.SetValue(myObject, ds.Tables[0].Rows[row][col.ColumnName], null);
                                break;
                        }

                    }
                }
            }

        }

        private DataSet _Get(dynamic myObject)
        {
            ArrayList myParams = new ArrayList();
            string myKeyList = string.Empty;

            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (PropertyInfo info in fieldInfo)
            {
                foreach (spaColumn col in spaColumns)
                {
                    if (col.Field.ToUpper() == info.Name.ToUpper() && col.Key == "PK")
                    {
                        myKeyList += " AND " + info.Name + " = @" + info.Name;
                        myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                    }
                }
            }

            string SQL = "SELECT * FROM " + myObject.GetType().Name + " ";
            SQL += "WHERE 1=1 " + myKeyList;

            return GetDataSet(SQL, myParams);
        }

        private void _Insert(dynamic myObject)
        {
            string session_id = Guid.Empty.ToString();
            string user_id = Guid.Empty.ToString();

            if (myObject.GetType().Name != "sys_session" && myObject.GetType().Name != "sys_import_log")
            {
                if (HttpContext.Current.Request.Cookies["spa"] == null)
                    throw new Exception("Object Insert: No Session");

                session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
                user_id = HttpContext.Current.Request.Cookies["spa"]["user"];
            }

            ArrayList myParams = new ArrayList();

            string myFieldList = string.Empty;
            string myValueList = string.Empty;

            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (PropertyInfo info in fieldInfo)
            {
                if (myFieldList == string.Empty)
                {
                    myFieldList += info.Name;
                    myValueList += "@" + info.Name;
                }
                else
                {
                    myFieldList += ", " + info.Name;
                    myValueList += ", @" + info.Name;
                }

                switch (info.Name)
                {
                    case "creation_date":
                        myParams.Add(CreateParameter("creation_date", typeof(DateTime), DateTime.UtcNow));
                        break;

                    case "modified_date":
                        myParams.Add(CreateParameter("modified_date", typeof(DateTime), DateTime.UtcNow));
                        break;

                    case "creation_user_id":
                        myParams.Add(CreateParameter("creation_user_id", typeof(String), user_id));
                        break;

                    case "modified_user_id":
                        myParams.Add(CreateParameter("modified_user_id", typeof(String), user_id));
                        break;

                    default:
                        myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                        break;
                }
            }

            string SQL = "INSERT into " + myObject.GetType().Name + " ";
            SQL += "(" + myFieldList + ") ";
            SQL += "VALUES (" + myValueList + ")";

            ExecuteSQL(SQL, myParams);
        }

        private void _Update(dynamic myObject, DataSet ds)
        {
            string session_id = Guid.Empty.ToString();
            string user_id = Guid.Empty.ToString();

            if (myObject.GetType().Name != "sys_import_log")
            {
                if (HttpContext.Current.Request.Cookies["spa"] == null)
                    throw new Exception("Object Insert: No Session");

                session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
                user_id = HttpContext.Current.Request.Cookies["spa"]["user"];
            }

            ArrayList myParams = new ArrayList();
            //myParams.Add(DB.CreateParameter("modified_date", "DateTime", DateTime.UtcNow));

            string myFieldList = string.Empty; // "modified_date = " + sParamChar + "modified_date";
            string myKeyList = string.Empty;

            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (PropertyInfo info in fieldInfo)
            {
                foreach (spaColumn col in spaColumns)
                {
                    if (col.Field.ToUpper() == info.Name.ToUpper())
                    {
                        if (col.Key == "PK")
                        {
                            myKeyList += " AND " + info.Name + " = @" + info.Name;
                            myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                        }
                        else
                        {
                            switch (info.Name)
                            {
                                case "modified_date":
                                    if (myFieldList == string.Empty)
                                        myFieldList += info.Name + " = @" + info.Name;
                                    else
                                        myFieldList += ", " + info.Name + " = @" + info.Name;
                                    myParams.Add(CreateParameter("modified_date", typeof(DateTime), DateTime.UtcNow));
                                    break;

                                case "modified_user_id":
                                    if (myFieldList == string.Empty)
                                        myFieldList += info.Name + " = @" + info.Name;
                                    else
                                        myFieldList += ", " + info.Name + " = @" + info.Name;
                                    myParams.Add(CreateParameter("modified_user_id", typeof(String), user_id));
                                    break;

                                case "creation_date":
                                    break;

                                case "creation_user_id":
                                    break;

                                default:
                                    if (ds.Tables[0].Rows[0][info.Name] == DBNull.Value)
                                    {
                                        switch (info.PropertyType.ToString())
                                        {
                                            case "System.DateTime":
                                                if (info.GetValue(myObject, null) != DateTime.MinValue)
                                                {
                                                    if (myFieldList == string.Empty)
                                                        myFieldList += info.Name + " = @" + info.Name;
                                                    else
                                                        myFieldList += ", " + info.Name + " = @" + info.Name;

                                                    myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                                                }
                                                break;

                                            default:
                                                if (info.GetValue(myObject, null) != null)
                                                {
                                                    if (myFieldList == string.Empty)
                                                        myFieldList += info.Name + " = @" + info.Name;
                                                    else
                                                        myFieldList += ", " + info.Name + " = @" + info.Name;

                                                    myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (info.PropertyType.ToString() == "System.Byte[]")
                                        {
                                            if (ds.Tables[0].Rows[0][info.Name] != info.GetValue(myObject, null))
                                            {
                                                if (myFieldList == string.Empty)
                                                    myFieldList += info.Name + " = @" + info.Name;
                                                else
                                                    myFieldList += ", " + info.Name + " = @" + info.Name;

                                                myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                                            }
                                        }

                                        if (ds.Tables[0].Rows[0][info.Name].ToString() != info.GetValue(myObject, null).ToString())
                                        {
                                            if (myFieldList == string.Empty)
                                                myFieldList += info.Name + " = @" + info.Name;
                                            else
                                                myFieldList += ", " + info.Name + " = @" + info.Name;

                                            myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                                        }
                                    }
                                    break;

                            }
                        }
                    }
                }
            }

            string SQL = "UPDATE " + myObject.GetType().Name + " ";
            SQL += "SET " + myFieldList + " ";
            SQL += "WHERE 1=1 " + myKeyList;

            ExecuteSQL(SQL, myParams);
        }

        public override bool CompareValues(string dataType, object val1, object val2)
        {

            switch (dataType)
            {
                case "DateTime":
                    if (DateTime.Parse(val1.ToString()) == DateTime.Parse(val2.ToString()))
                        return true;
                    break;

                case "Boolean":
                    if ((Boolean.Parse(val2.ToString()) == true) && (val1.ToString() == "1"))
                        return true;
                    if ((Boolean.Parse(val2.ToString()) == false) && (val1.ToString() == "0"))
                        return true;
                    break;

                case "int":
                    if (int.Parse(val1.ToString()) == int.Parse(val2.ToString()))
                        return true;
                    break;

                case "decimal":
                    if (Decimal.Parse(val1.ToString()) == Decimal.Parse(val2.ToString()))
                        return true;
                    break;

                case "Byte[]":
                    if (Byte.Parse(val1.ToString()) == Byte.Parse(val2.ToString()))
                        return true;
                    break;

                default:
                    if (val1.ToString() == val2.ToString())
                        return true;
                    break;

            }

            return false;
        }

        private string GetOrderBy(string SQL)
        {
            string sOrderBy = SQL.ToUpper();
            int iPos = sOrderBy.IndexOf("ORDER BY");

            if (iPos > 0)
                return SQL.Substring(iPos + 8);
            else
                return string.Empty;
        }                                                                                           

        private string RemoveOrderBy(string SQL, string OrderBy)
        {
            string sSQL = SQL.ToUpper();
            int iPos = sSQL.IndexOf("ORDER BY");

            if (iPos > 0)
                return SQL.Substring(0, iPos);
            else
                return SQL;
        }

        private string GetCountSQL(string SQL)
        {
            string sql = SQL;
            
            string sSQL = SQL.ToUpper();
            int iPos = sSQL.IndexOf("ORDER BY");
            if (iPos > 0)
                sql = SQL.Substring(0, iPos);

            sSQL = sql.ToUpper();
            iPos = sSQL.IndexOf(";WITH");
            if (iPos > 0)
            {
                iPos = sql.LastIndexOf('*');
                sql = ReplaceLastOccurrence(sql, "*", "count(*) as row_count");
            }
            else
            {
                sql = @"
;SELECT count(*) as row_count FROM ( " + sql + " ) as count_sql ";
            }

            return sql;
        }

        private string ReplaceLastOccurrence(string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);
            return source.Remove(place, find.Length).Insert(place, replace);
        }
    }
}
