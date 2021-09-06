﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class AnalisaKondisiModel
    {
        public string Aspect { get; set; }
        //What Should Be Happened = WSBH
        public string WSBH { get; set; }
        // What Actually Happened
        public string WAH { get; set; }
        public string Status { get; set; }
    }
    partial class ListKondisi
    {
        public static DataTable DT = new DataTable(typeof(AnalisaKondisiModel).Name);
        public DataTable ToDataTable<T>(List<T> items)
        {             
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (DT.Columns.Count <= 0 )
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