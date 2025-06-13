using System;
using System.Web.Services;
using System.Reflection;
using Newtonsoft.Json;


/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class dataset_Services : System.Web.Services.WebService
{
    DataLayer.sys_session oSession = new DataLayer.sys_session();

    public dataset_Services()
    {
    }

    [WebMethod]
    public void GenericUpdate(dynamic Updates)
    {
        SPA.spaResponse myResponse = new SPA.spaResponse();

        try
        {
            for (int x = 0; x < Updates.Length; x++)
            {
                string myAction = Updates[x]["Action"].ToString();  // Add, Update or Delete

                // Create the correct Data Object
                string myObject = "Objects." + Updates[x]["TableName"].ToString();
                object o = Activator.CreateInstance(Type.GetType(myObject));

                // Determine the PK and set the value
                string myPK = Updates[x]["KeyName"].ToString();
                string myPKValue = Updates[x]["KeyValue"].ToString();

                // If its an Add generate a new id
                if (myAction == "Add")
                    myPKValue = Guid.NewGuid().ToString();

                // Save the id in the response object (for the client)
                myResponse.data = myPKValue;

                // Set the PK value in the Data Object
                SetValue(o, o.GetType().GetProperty(myPK), myPKValue);

                // If the Action is not an Add populate the Data Object from the database
                // by calling the Get method
                if (myAction != "Add")
                {
                    MethodInfo getMethod = o.GetType().GetMethod("Get");
                    getMethod.Invoke(o, null);
                }

                // If the Action is Delete call the Data Objects Delete method
                if (myAction == "Delete")
                {
                    MethodInfo deleteMethod = o.GetType().GetMethod("Delete");
                    deleteMethod.Invoke(o, null);
                }
                else
                {
                    //Otherwise update matching properties between the Data Object and 
                    //the stuff from the client
                    for (int i = 0; i < Updates[x]["Columns"].Length; i++)
                    {
                        string myColumn = Updates[x]["Columns"][i]["Column"].ToString();
                        string myColumnValue = Updates[x]["Columns"][i]["Value"].ToString();

                        if (myColumn != myPK)
                            SetValue(o, o.GetType().GetProperty(myColumn), myColumnValue);
                    }

                    // Call the Data Objects Save method
                    MethodInfo saveMethod = o.GetType().GetMethod("Save");
                    saveMethod.Invoke(o, null);
                }
            }

            // Respond with a good result
            myResponse.result = true;
            myResponse.message = "OK";
        }
        catch (Exception ex)
        {
            // We hit an error so respond with the error
            myResponse.result = false;

            if (ex.InnerException != null)
                myResponse.message = ex.InnerException.Message;
            else
                myResponse.message = ex.Message;
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    private void SetValue(object o, PropertyInfo info, string value)
    {
        // Convert the string from the client into the 
        // correct Data Object property type
        if (info.PropertyType == typeof(Nullable<Guid>) || info.PropertyType == typeof(Guid))
        {
            Guid myGuid = Guid.Empty;
            try
            {
                myGuid = Guid.Parse(value);
            }
            catch { };
            info.SetValue(o, myGuid, null);
        }
        else if (info.PropertyType == typeof(Nullable<DateTime>) || info.PropertyType == typeof(DateTime))
        {
            DateTime myDate = DateTime.MinValue;
            try
            {
                myDate = DateTime.Parse(value);
            }
            catch { };
            info.SetValue(o, myDate, null);
        }
        else if (info.PropertyType == typeof(Nullable<int>) || info.PropertyType == typeof(int))
        {
            int myInt = 0;
            try
            {
                myInt = Int32.Parse(value);
            }
            catch { };
            info.SetValue(o, myInt, null);
        }
        else if (info.PropertyType == typeof(Nullable<Decimal>) || info.PropertyType == typeof(Decimal))
        {
            decimal myDec = 0;
            try
            {
                myDec = Decimal.Parse(value);
            }
            catch { };
            info.SetValue(o, myDec, null);
        }
        else if (info.PropertyType == typeof(Nullable<long>) || info.PropertyType == typeof(long))
        {
            long myLong = 0;
            try
            {
                myLong = long.Parse(value);
            }
            catch { };
            info.SetValue(o, myLong, null);
        }
        else if (info.PropertyType == typeof(Nullable<Double>) || info.PropertyType == typeof(Double))
        {
            Double myDouble = 0;
            try
            {
                myDouble = Double.Parse(value);
            }
            catch { };
            info.SetValue(o, myDouble, null);
        }
        else if (info.PropertyType == typeof(Nullable<Boolean>) || info.PropertyType == typeof(Boolean))
        {
            Boolean myBool = false;
            try
            {
                if (value.ToLower() == "true")
                    myBool = true;
            }
            catch { };
            info.SetValue(o, myBool, null);
        }
        else
        {
                info.SetValue(o, value, null);
        }
        

/*
        switch (info.PropertyType.Name)
        {
            case "Guid":
                Guid myGuid = Guid.Empty;
                try
                {
                    myGuid = Guid.Parse(value);
                }
                catch { };
                info.SetValue(o, myGuid, null);
                break;

            case "DateTime":
                DateTime myDate = DateTime.MinValue;
                try
                {
                    myDate = DateTime.Parse(value);
                }
                catch { };
                info.SetValue(o, myDate, null);
                break;

            case "Int32":
                int myInt = 0;
                try
                {
                    myInt = Int32.Parse(value);
                }
                catch { };
                info.SetValue(o, myInt, null);
                break;

            case "Decimal":
                decimal myDec = 0;
                try
                {
                    myDec = Decimal.Parse(value);
                }
                catch { };
                info.SetValue(o, myDec, null);
                break;

            case "Int64":
                long myLong = 0;
                try
                {
                    myLong = long.Parse(value);
                }
                catch { };
                info.SetValue(o, myLong, null);
                break;

            case "Double":
                Double myDouble = 0;
                try
                {
                    myDouble = Double.Parse(value);
                }
                catch { };
                info.SetValue(o, myDouble, null);
                break;

            case "Boolean":
                Boolean myBool = false;
                try
                {
                    myBool = Boolean.Parse(value);
                }
                catch { };
                info.SetValue(o, myBool, null);
                break;

            default:
                info.SetValue(o, value, null);
                break;
        }
 */
    }
}
