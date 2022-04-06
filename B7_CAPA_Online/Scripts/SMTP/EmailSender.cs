using B7_CAPA_Online.Models;
using Dapper;
using System;
using System.Collections.Generic;
using B7_CAPA_Online.Scripts;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace B7_CAPA_Online.Scripts.SMTP
{
    public class EmailSender
    {
        DataAccess.DataAccess DAL = new DataAccess.DataAccess();
        public string SendEmail(Dictionary<string, object> dic)
        {
            try
            {
                //var dictionary = new Dictionary<string, string>
                //{
                //    {"Nama_Aplikasi", "CAPA" },
                //    {"Kategori", "PICReminder" },
                //    {"KategoriCAPA", Model.KategoriCAPA },
                //    {"ToEmpName", Model.PIC_CAPA },
                //    {"NO_CAPA", Model.NO_CAPA},
                //    {"TriggerCAPA", Model.TriggerCAPA},
                //    {"Lokasi", Model.Lokasi},
                //    {"StatusCAPA", 1},
                //    {"PIC", Model.PIC_ID},                
                //};

                var msg = new MailMessage();
                //msg.From = new MailAddress("it.bintang7@gmail.com", "CAPA B7 Mailing System");
                msg.From = new MailAddress("notification@bintang7.com", "B7 Connect Mailing System");
                var obj = ParsingJson(dic["Recipient"].ToString());
                dic.Add("NO_CAPA", obj[0].NO_CAPA.ToString());
                dic.Add("Hyperlink", "https://portal.bintang7.com/B7_CAPA_Online/Login");
                dic.Remove("Recipient");
                var parameters = new DynamicParameters(dic);
                Email emailData = new Email();
                string eData = DAL.StoredProcedure(parameters, "[dbo].[SP_EMAIL_SENDER]");
                var emailCC = GetCCEmail(dic);
                dynamic emailList = JsonConvert.DeserializeObject<List<Email>>(eData);
                emailData.EmailSubject = emailList[0].EmailSubject;
                emailData.EmailBody = emailList[0].EmailBody;
                SmtpClient mailObj = new SmtpClient("mail.kalbe.co.id");

                msg.Body = emailData.EmailBody;
                msg.Subject = emailData.EmailSubject;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;
                //msg.To.Add("dani.pernando@bintang7.com");                            
                msg.To.Add(obj[0].Email);
                foreach(var CC in emailCC) // looping cc 
                {
                    msg.CC.Add(CC.Email); 
                }              
                mailObj.Send(msg);
                return "success";
            }
            catch (Exception ex)
            {
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("UpdateBy", dic["CreateBy"]);
                dictionary.Add("NO_CAPA", dic["NO_CAPA"]);
                dictionary.Add("ErrorLog", ex.ToString());
                dictionary.Add("StatusID", 1);
                DynamicParameters dynamicParameters = new DynamicParameters(dictionary);
                DAL.StoredProcedure(dynamicParameters, "[dbo].[SP_ERROR_HISTORY]");
                return ex.ToString();
            }
        }

        public List<Recipients> ParsingJson(string json)
        {
            var list = new List<Recipients>();
            var objects = JsonConvert.DeserializeObject(json); // parse as array  
            foreach (var item in ((JArray)objects))
            {
                list.Add(new Recipients { NO_CAPA = item.Value<string>("NoCAPA"), Email = item.Value<string>("Email") });
            }
            return list;
        }

        public List<Recipients> GetCCEmail(Dictionary<string,object> dictionary)
        {
            var dic = new Dictionary<string, object>
            {
                {"NO_CAPA", dictionary["NO_CAPA"]}                
            };
            var list = new List<Recipients>();
            var param = new DynamicParameters(dic);
            string data = DAL.StoredProcedure(param, "[dbo].[SP_EMAIL_CC]");
            var objects = JsonConvert.DeserializeObject(data);
            foreach (var item in ((JArray)objects))
            {
                list.Add(new Recipients {Email = item.Value<string>("Email") });
            }
            return list;
        }

    }
}