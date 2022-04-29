using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class ListPICModel
    {
       
        public string NIK { get; set; }
        public string PIC { get; set; }
        public string Departemen { get; set; }
        public string Email { get; set; }
    }

    public class ListPIC
    {
        public static DataTable DT = new DataTable(typeof(ListPICModel).Name);
        public DataTable ToDataTable<T>(List<T> items)
        {
            ClearDT();
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
                for (int i = DT.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = DT.Rows[i];
                    if (dr["PIC"].ToString() == values[0].ToString())
                    {
                        return DT;
                    }
                }
                DT.Rows.Add(values);
            }
            return DT;
        }

        public DataTable DeleteRows(string pic)
        {
            for (int i = DT.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = DT.Rows[i];
                if (dr["PIC"].ToString() == pic)
                {
                    dr.Delete();
                }
            }
            DT.AcceptChanges();
            return DT;
        }

        public void ClearDT()
        {
            DT.Clear();
        }
    }
}