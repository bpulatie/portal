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
namespace Services
{

    public class inv_category_level
    {
        public inv_category_level()
        {
        }

        public void SetLevels(Guid client_id, int levels)
        {
            DataLayer.inv_category_level oData = new DataLayer.inv_category_level();
            List<Objects.inv_category_level> oLevels = oData.ListAll<Objects.inv_category_level>(client_id.ToString());

            int myLevel = levels;
            foreach(Objects.inv_category_level oLevel in oLevels)
            {
                if (myLevel > 0)
                {
                    oLevel.depth = myLevel;
                    if (myLevel == levels)
                        oLevel.item_level = "y";
                    else
                        oLevel.item_level = "n";

                    if (oLevel.level_name.StartsWith("Category Level "))
                    {
                        oLevel.level_name = "Category Level " + myLevel.ToString();
                    }

                    oLevel.Save();
                }
                else
                {
                    oLevel.Delete();
                }
                myLevel--;
            }

            for (int x = myLevel; x > 0; x--)
            {
                Objects.inv_category_level oLevel = new Objects.inv_category_level();
                oLevel.category_level_id = Guid.NewGuid();
                oLevel.client_id = client_id;
                if (x == levels)
                    oLevel.item_level = "y";
                else
                    oLevel.item_level = "n";

                oLevel.depth = x;
                oLevel.level_name = "Category Level " + x.ToString();

                oLevel.Save();
            }

        }
    }
}