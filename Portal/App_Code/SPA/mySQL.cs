using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace SPA
{
    class mySQL : spaDB
    {
        private string _connectionString = string.Empty;
        private List<spaColumn> spaColumns = new List<spaColumn>();

        public mySQL(string database_connection)
            : base(database_connection)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[database_connection].ConnectionString;
        }

        public mySQL(string database_connection, string database_table)
            : base(database_connection, database_table)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[database_connection].ConnectionString;
            spaColumns = GetTableMetaData(database_table);
        }

        public override List<spaColumn> GetTableMetaData(string Table)
        {
            List<spaColumn> spaColumns = new List<spaColumn>();
            ArrayList Params = new ArrayList();

            string SQL = "SHOW COLUMNS FROM " + Table;
            MySqlDataReader myDR = GetReader(SQL, Params);

            while (myDR.Read())
            {
                spaColumn col = new spaColumn();
                col.Table = Table;
                col.Field = myDR["Field"].ToString();
                col.Type = myDR["Type"].ToString();
                if (myDR["Key"].ToString() == "PRI")
                    col.Key = "PK";
                else
                    col.Key = string.Empty;
                col.Default = myDR["Default"].ToString();
                col.Extra = myDR["Extra"].ToString();

                spaColumns.Add(col);
            }
            myDR.Close();

            return spaColumns;
        }

        private MySqlDataReader GetReader(string SQL, ArrayList Params)
        {
            // Create Instance of Connection and Command Object
            MySqlConnection myConnection = new MySqlConnection(_connectionString);
            MySqlCommand myCommand = new MySqlCommand("", myConnection);

            myCommand.CommandText = SQL;

            // Add any parameters
            myCommand.Parameters.Clear();
            myCommand.Parameters.AddRange(Params.ToArray());

            // Execute the command
            myConnection.Open();
            MySqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader 
            return result;
        }

        public override DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

            // Create Instance of Connection and Command Object
            using (MySqlConnection myConnection = new MySqlConnection(_connectionString))
            {
                using (MySqlDataAdapter myCommand = new MySqlDataAdapter("", myConnection))
                {
                    myCommand.SelectCommand.CommandText = SQL;

                    // Add any parameters
                    myCommand.SelectCommand.Parameters.Clear();
                    myCommand.SelectCommand.Parameters.AddRange(Params.ToArray());

                    // Create and Fill the DataSet
                    myCommand.Fill(result);
                }
            }

            // Return the DataSet
            return result;
        }

        public override int ExecuteSQL(string SQL, ArrayList Params)
        {
            // Create Instance of Connection and Command Object
            using (MySqlConnection myConnection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand myCommand = new MySqlCommand("", myConnection))
                {
                    // The SQL
                    myCommand.CommandText = SQL;

                    // Add any parameters
                    myCommand.Parameters.Clear();
                    myCommand.Parameters.AddRange(Params.ToArray());

                    // Execute the command
                    myConnection.Open();
                    return myCommand.ExecuteNonQuery();
                }
            }
        }

        public override int ExecuteStoredProcedure(string SQL, ArrayList Params)
        {
            // Create Instance of Connection and Command Object
            using (MySqlConnection myConnection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand myCommand = new MySqlCommand("", myConnection))
                {
                    // The SQL
                    myCommand.CommandText = SQL;

                    // Add any parameters
                    myCommand.Parameters.Clear();
                    myCommand.Parameters.AddRange(Params.ToArray());

                    // Execute the command
                    myConnection.Open();
                    return myCommand.ExecuteNonQuery();
                }
            }
        }
        
        public override DataSet GetPagedDataSet(string SQL, ArrayList Params, int pageNo, int rows)
        {
            int iStart = 0;
            int iRows = 50;

            if (rows > 0 && rows < 101)
                iRows = rows;

            if (pageNo > 1)
                iStart = (iRows * (pageNo - 1));

            string sLimit = "LIMIT " + iStart.ToString() + ", " + iRows.ToString();

            SQL += SQL += " " + sLimit;

            string sql = SQL.Replace("SELECT", "SELECT SQL_CALC_FOUND_ROWS");
            sql += "; SELECT  FOUND_ROWS() as row_count";

            DataSet result = new DataSet();

            // Create Instance of Connection and Command Object
            using (MySqlConnection myConnection = new MySqlConnection(_connectionString))
            {
                using (MySqlDataAdapter myCommand = new MySqlDataAdapter("", myConnection))
                {
                    myCommand.SelectCommand.CommandText = sql;

                    // Add any parameters
                    myCommand.SelectCommand.Parameters.Clear();
                    myCommand.SelectCommand.Parameters.AddRange(Params.ToArray());

                    // Create and Fill the DataSet
                    myCommand.Fill(result);
                }
            }

            // Return the DataSet
            return result;
        }

        public override object CreateParameter(string name, Type myType, object value)
        {
            // Create Parameter
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = "?" + name;

            //Set the data type for the parameter
            if (myType == typeof(Nullable<DateTime>) || myType == typeof(DateTime))
            {
                param.MySqlDbType = MySqlDbType.DateTime;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<bool>) || myType == typeof(bool))
            {
                param.MySqlDbType = MySqlDbType.Bit;
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
            else if (myType == typeof(Nullable<int>) || myType == typeof(int))
            {
                param.MySqlDbType = MySqlDbType.Int32;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<decimal>) || myType == typeof(decimal))
            {
                param.MySqlDbType = MySqlDbType.Decimal;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<byte>) || myType == typeof(byte))
            {
                param.MySqlDbType = MySqlDbType.LongBlob;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else
            {
                param.MySqlDbType = MySqlDbType.VarChar;
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
                myFill(myObject, ds);
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
                        myKeyList += " AND " + info.Name + " = ?" + info.Name;
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

        private void myFill(dynamic myObject, DataSet ds)
        {
            PropertyInfo myProperty;
            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();
            
            foreach (DataColumn col in ds.Tables[0].Columns)
            {
                myProperty = myObject.GetType().GetProperty(col.ColumnName);

                if (myProperty != null)
                {
                    if (ds.Tables[0].Rows[0][col.ColumnName] == DBNull.Value)
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
                            case "Guid":
                                Guid myGuid = Guid.Empty;
                                try
                                {
                                    myGuid = Guid.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myGuid, null);
                                break;

                            case "DateTime":
                                DateTime myDate = DateTime.MinValue;
                                try
                                {
                                    myDate = DateTime.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myDate, null);
                                break;

                            case "Int32":
                                int myInt = 0;
                                try
                                {
                                    myInt = Int32.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myInt, null);
                                break;

                            case "Decimal":
                                decimal myDec = 0;
                                try
                                {
                                    myDec = Decimal.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myDec, null);
                                break;

                            case "Int64":
                                long myLong = 0;
                                try
                                {
                                    myLong = long.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myLong, null);
                                break;

                            case "Double":
                                Double myDouble = 0;
                                try
                                {
                                    myDouble = Double.Parse(ds.Tables[0].Rows[0][col.ColumnName].ToString());
                                }
                                catch { };
                                myProperty.SetValue(myObject, myDouble, null);
                                break;

                            case "Boolean":
                                Boolean myBool = true;
                                if (ds.Tables[0].Rows[0][col.ColumnName].ToString() == "1")
                                    myBool = false;
                                myProperty.SetValue(myObject, myBool, null);
                                break;

                            case "Byte[]":
                                byte[] myByte = null;
                                try
                                {
                                    myByte = (byte[])ds.Tables[0].Rows[0][col.ColumnName];
                                }
                                catch { };
                                myProperty.SetValue(myObject, myByte, null);
                                break;

                            default:
                                myProperty.SetValue(myObject, ds.Tables[0].Rows[0][col.ColumnName].ToString(), null);
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
                        myKeyList += " AND " + info.Name + " = ?" + info.Name;
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
            ArrayList myParams = new ArrayList();

            string myFieldList = string.Empty;
            string myValueList = string.Empty;

            PropertyInfo[] fieldInfo = myObject.GetType().GetProperties();

            foreach (PropertyInfo info in fieldInfo)
            {
                if (myFieldList == string.Empty)
                {
                    myFieldList += info.Name;
                    myValueList += "?" + info.Name;
                }
                else
                {
                    myFieldList += ", " + info.Name;
                    myValueList += ", ?" + info.Name;
                }

                if (info.Name == "creation_date")
                    myParams.Add(CreateParameter("creation_date", typeof(DateTime), DateTime.UtcNow));
                else
                    myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
            }

            string SQL = "INSERT into " + myObject.GetType().Name + " ";
            SQL += "(" + myFieldList + ") ";
            SQL += "VALUES (" + myValueList + ")";

            ExecuteSQL(SQL, myParams);
        }

        private void _Update(dynamic myObject, DataSet ds)
        {
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
                            myKeyList += " AND " + info.Name + " = ?" + info.Name;
                            myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[0][info.Name] == DBNull.Value)
                            {
                                if (info.GetValue(myObject, null) != null)
                                {
                                    if (myFieldList == string.Empty)
                                        myFieldList += info.Name + " = ?" + info.Name;
                                    else
                                        myFieldList += ", " + info.Name + " = ?" + info.Name;

                                    myParams.Add(CreateParameter(info.Name, info.PropertyType, DBNull.Value));
                                }
                            }
                            else
                            {
                                // Need to account for differing datatypes
                                if (CompareValues(info.PropertyType, ds.Tables[0].Rows[0][info.Name], info.GetValue(myObject, null)) != true)
                                {
                                    if (myFieldList == string.Empty)
                                        myFieldList += info.Name + " = ?" + info.Name;
                                    else
                                        myFieldList += ", " + info.Name + " = ?" + info.Name;

                                    myParams.Add(CreateParameter(info.Name, info.PropertyType, info.GetValue(myObject, null)));
                                }
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

        private bool CompareValues(Type dataType, object val1, object val2)
        {

            switch (dataType.Name)
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
    }

}
