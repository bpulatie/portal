using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

namespace SPA
{
    public class spaBase
    {
        private spaDatabase DB;

        public spaBase(string database_connection, string database_table)
        {
            DB = new spaDatabase(database_connection, database_table);
        }

        public void Get()
        {
            DB.Get(this);
        }

        public void Save()
        {
            PropertyInfo myProperty = this.GetType().GetProperty("client_id");

            if (myProperty != null)
            {
                if (myProperty.GetValue(this, null).ToString() == Guid.Empty.ToString())
                {
                    if (System.Web.HttpContext.Current.Request.Cookies["spa"]["client"].ToString() != null)
                    {
                        myProperty.SetValue(this, Guid.Parse(System.Web.HttpContext.Current.Request.Cookies["spa"]["client"].ToString()), null);
                    }
                }
            }

            Before_Save();
            DB.Save(this);
            After_Save();
        }

        public void Delete()
        {
            Before_Delete();
            DB.Delete(this);
            After_Delete();
        }

        public virtual void Before_Save()
        {

        }

        public virtual void After_Save()
        {

        }

        public virtual void Before_Delete()
        {

        }

        public virtual void After_Delete()
        {

        }
    }
}
