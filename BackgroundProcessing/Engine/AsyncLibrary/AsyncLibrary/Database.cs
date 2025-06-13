using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace AsyncLibrary
{

    public class Database
    {
        String _connectionString = String.Empty;

        public Database(String databaseName)
        {
            string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncProcessor.exe");

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
            AppSettingsSection appSettings = configuration.AppSettings;

            _connectionString = appSettings.Settings[databaseName].Value;
        }

        public DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

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

            // Return the DataSet
            return result;
        }

        public void ExecuteSQL(string SQL, ArrayList Params)
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
                    myCommand.ExecuteNonQuery();

                    myCommand.Parameters.Clear();
                }
            }
        }

        public int ExecuteStoredProcedure(string SQL, ArrayList Params)
        {
            int spTimeout = 30;
            int result = 0;

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

                    // Execute the command
                    myConnection.Open();
                    result = myCommand.ExecuteNonQuery();

                    myCommand.Parameters.Clear();
                }
            }

            return result;
        }

        public object CreateParameter(string name, Type myType, object value)
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

    }
}
