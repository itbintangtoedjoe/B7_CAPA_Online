using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B7_CAPA_Online.Models
{
    public class EvaluatorAttirbute
    {
       
    }
    public class EvaluatorReviewAttribute
    {
        public string TypeKajian { get; set; }
        public string Option { get; set; }
        public string SP { get; set; }
        public string NoCAPA { get; set; }
        public string RentangPencegahan { get; set; }
        public string RentangPerbaikan { get; set; }
        public string EMPID { get; set; }
        public string RecordID { get; set; }
    }
}