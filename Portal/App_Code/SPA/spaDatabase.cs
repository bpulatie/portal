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
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace SPA
{
    class spaDatabase
    {
        public List<spaColumn> spaColumns = new List<spaColumn>();
        private string _provider = string.Empty;
        private spaDB _DB;

        public spaDatabase(string database_connection)
        {
            _provider = ConfigurationManager.ConnectionStrings[database_connection].ProviderName;

            switch (_provider)
            {
                case "MySql.Data.MySqlClient":
                    _DB = new mySQL(database_connection);
                    break;

                case "sqloledb":
                    _DB = new msSQL(database_connection);
                    break;

                default:
                    break;

            }
        }

        public spaDatabase(string database_connection, string database_table)
        {
            _provider = ConfigurationManager.ConnectionStrings[database_connection].ProviderName;

            switch (_provider)
            {
                case "MySql.Data.MySqlClient":
                    _DB = new mySQL(database_connection, database_table);
                    spaColumns = _DB.GetTableMetaData(database_table);
                    break;

                case "sqloledb":
                    _DB = new msSQL(database_connection, database_table);
                    spaColumns = _DB.GetTableMetaData(database_table);
                    break;

                default:
                    break;

            }
        }

        public List<T> GetList<T>(string SQL, ArrayList Params)
        {
            return _DB.GetList<T>(SQL, Params);
        }

        public DataSet GetDataSet(string SQL, ArrayList Params)
        {
            return _DB.GetDataSet(SQL, Params);
        }

        public string GetPagedDataSet(string SQL, ArrayList Params, int pageNo, int rows)
        {
            DataSet ds = _DB.GetPagedDataSet(SQL, Params, pageNo, rows);
            return GetJson(ds, pageNo, rows);
        }

        public String GetJSONDataSet(string SQL, ArrayList Params)
        {
            DataSet ds = _DB.GetDataSet(SQL, Params);
            return GetJson(ds, -1, -1);
        }

        public int ExecuteSQL(string SQL, ArrayList Params)
        {
            return _DB.ExecuteSQL(SQL, Params);
        }

        public int ExecuteStoredProcedure(string SQL, ArrayList Params)
        {
            return _DB.ExecuteStoredProcedure(SQL, Params);
        }

        public string GetParameterCharacter()
        {
            return _DB.GetParameterCharacter();
        }

        public object CreateParameter(string name, Type myType, object value)
        {
            return _DB.CreateParameter(name, myType, value);
        }

        public bool CompareValues(string dataType, object val1, object val2)
        {
            return _DB.CompareValues(dataType, val1, val2);
        }

        public string GetJson(DataSet ds, int pageNo, int rows)
        {
             int Count = ds.Tables[0].Rows.Count;
            double Pages = 1;
            string more = "n";

            if (pageNo > 0)
            {
                Count = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                Pages = Math.Ceiling((double)Count / (double)rows);

                if (Pages > pageNo)
                    more = "y";
            }

            int CurrentRow = 0;
            if (Count == 0)
                CurrentRow = -1;

            string json = "{";

            json += "\"PageNo\": \"" + pageNo.ToString() + "\", ";
            json += "\"PageSize\": \"" + rows.ToString() + "\", ";
            json += "\"PageCount\": \"" + Pages.ToString() + "\", ";
            json += "\"RowCount\": \"" + Count.ToString() + "\", ";
            json += "\"CurrentRow\": \"" + CurrentRow.ToString() + "\", ";
            json += "\"MoreData\": \"" + more + "\", ";

            // Get Column Data
            json += "\"MetaData\": ";

            Dictionary<string, object> meta = new Dictionary<string, object>();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.DataType.Name == "UInt64")
                    meta.Add(dc.ColumnName.Trim(), "Boolean");
                else
                    meta.Add(dc.ColumnName.Trim(), dc.DataType.Name);
            }

            json += JsonConvert.SerializeObject(meta);

            json += ", ";

            // Create an Empty Row
            json += "\"EmptyRow\": ";

            Dictionary<string, object> emptyRow = new Dictionary<string, object>();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                emptyRow.Add(dc.ColumnName.Trim(), "");
            }

            json += JsonConvert.SerializeObject(emptyRow);

            json += ", ";

            // Get Data
            json += "\"Rows\": ";

            List<Dictionary<string, object>> records = new List<Dictionary<string, object>>();
            Dictionary<string, object> record = null;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                record = new Dictionary<string, object>();
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    if (col.DataType.Name == "UInt64")
                    {
                        if (dr[col].ToString() == "1")
                            record.Add(col.ColumnName.Trim(), true);
                        else
                            record.Add(col.ColumnName.Trim(), false);
                    }
                    else
                        record.Add(col.ColumnName.Trim(), dr[col]);
                }
                records.Add(record);
            }

            json += JsonConvert.SerializeObject(records); 

            json += "}";

            return json;
        }

        public object Get(object myObject)
        {
            return _DB.Get(myObject);
        }

        public object Save(object myObject)
        {
            return _DB.Save(myObject);
        }

        public object Delete(object myObject)
        {
            return _DB.Delete(myObject);
        }

    }

}
