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

namespace SPA
{
    class spaDB
    {
        public spaDB(string database_connection)
        {
        }

        public spaDB(string database_connection, string database_table)
        {
        }

        public virtual List<spaColumn> GetTableMetaData(string Table)
        {
            return new List<spaColumn>();
        }

        public virtual List<T> GetList<T>(string SQL, ArrayList Params)
        {
            return new List<T>();
        }

        public virtual DataSet GetDataSet(string SQL, ArrayList Params)
        {
            return new DataSet();
        }

        public virtual int ExecuteSQL(string SQL, ArrayList Params)
        {
            return 0;
        }

        public virtual int ExecuteStoredProcedure(string SQL, ArrayList Params)
        {
            return 0;
        }

        public virtual DataSet GetPagedDataSet(string SQL, ArrayList Params, int pageNo, int rows)
        {
            DataSet ds = new DataSet();
            return ds;
        }

        public virtual string GetParameterCharacter()
        {
            return "?";
        }

        public virtual object CreateParameter(string name, Type myType, object value)
        {
            var result = new object();
            return result;
        }

        public virtual bool CompareValues(String dataType, object val1, object val2)
        {
            return false;
        }

        public virtual object Get(object myObject)
        {
            return myObject;
        }

        public virtual object Save(object myObject)
        {
            return myObject;
        }

        public virtual object Delete(object myObject)
        {
            return myObject;
        }

    }

}
