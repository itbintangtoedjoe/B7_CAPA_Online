using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class ListDepartemenModel
    {
        public string Departemen { get; set; }
        public string Lokasi { get; set; }
    }

    partial class ListDepartemen
    {
        public static DataTable DT = new DataTable(typeof(ListDepartemenModel).Name);
        
        public DataTable ToDataTable<T>(List<T> items)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (DT.Columns.Count <= 0)
            {
                foreach (PropertyInfo prop in Props)
                {
                    DT.Columns.Add(prop.Name);
                }
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                DT.Rows.Add(values);
            }
            return DT;
        }

        public void ClearDT()
        {
            DT.Clear();
        }
    }
    
}