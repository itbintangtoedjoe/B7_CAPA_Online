using B7_CAPA_Online.Models;
using Dapper;
using System;
using System.Collections.Generic;
using B7_CAPA_Online.Scripts;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Mail;

namespace B7_CAPA_Online.Scripts.SMTP
{
    public class EmailSender
    {
        DataAccess.DataAccess DAL = new DataAccess.DataAccess();
        public string SendEmail(DALModel Model)
        {
            try
            {
                var dictionary = new Dictionary<string, object>
                {
                    {"Nama_Aplikasi", "CAPA" },
                    {"Kategori", "PICReminder" },
                    {"KategoriCAPA", Model.KategoriCAPA },
                    {"ToEmpName", Model.PIC_CAPA },
                    {"NO_CAPA", Model.NO_CAPA},
                    {"TriggerCAPA", Model.TriggerCAPA},
                    {"Lokasi", Model.Lokasi},
                    {"StatusCAPA", 1},
                    {"PIC", Model.PIC_ID},
                    {"Hyperlink", "https://google.com/"}
                };
                var parameters = new DynamicParameters(dictionary);
                Email emailData = new Email();
                string eData = DAL.StoredProcedure(parameters, "[dbo].[SP_EMAIL_SENDER]");
                dynamic emailList = JsonConvert.DeserializeObject<List<Email>>(eData);
                emailData.EmailSubject = emailList[0].EmailSubject;
                emailData.EmailBody = emailList[0].EmailBody;
                SmtpClient mailObj = new SmtpClient("mail.kalbe.co.id");
                var msg = new MailMessage();
                msg.From = new MailAddress("it.bintang7@gmail.com", "CAPA B7 Mailing System");
                msg.Body = emailData.EmailBody;
                msg.Subject = emailData.EmailSubject;
                msg.Priority = MailPriority.High;
                msg.IsBodyHtml = true;

                msg.To.Add("dani.pernando@bintang7.com"); // looping dynamic sesuai dengan banyaknya email.            
                //msg.To.Add(Model.Email);

                mailObj.Send(msg);
                return "success";
            }
            catch (Exception ex)
            {
                var dictionary = new Dictionary<string, object>();
                dictionary.Add("UpdateBy", Model.Create_By);
                dictionary.Add("NO_CAPA", Model.NO_CAPA);
                dictionary.Add("ErrorLog", ex.ToString());
                dictionary.Add("StatusID", 1);
                DynamicParameters dynamicParameters = new DynamicParameters(dictionary);
                DAL.StoredProcedure(dynamicParameters, "[dbo].[SP_ERROR_HISTORY]");
                return ex.ToString();
            }
        }

    }
}