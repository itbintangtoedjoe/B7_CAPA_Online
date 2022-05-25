using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;

namespace B7_CAPA_Online.Controllers
{
   
    public class UserVendorController : Controller
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MASTERVENDOR"].ToString());
        private readonly SqlConnection logEmailConn = new SqlConnection(ConfigurationManager.ConnectionStrings["B7CONNECT"].ToString());
        readonly DataAccess DAL = new DataAccess();

        public List<UserVendor> GetAllUserVendors()
        {
            DataSet ds = new DataSet();
            List<UserVendor> allUserVendors = new List<UserVendor>();
            Vendor vendorData;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_select_all_user_vendors", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UserVendor userVendor = new UserVendor();
                    userVendor.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                    userVendor.UserID = ds.Tables[0].Rows[i]["userID"].ToString();
                    userVendor.UserVendorName = ds.Tables[0].Rows[i]["user_vendor_name"].ToString();
                    userVendor.Username = ds.Tables[0].Rows[i]["username"].ToString();
                    userVendor.Password = ds.Tables[0].Rows[i]["password"].ToString();
                    userVendor.Email = ds.Tables[0].Rows[i]["email"].ToString();

                    //string superiorName = ds.Tables[0].Rows[i]["superior_name"].ToString();
                    //if (superiorName == "" || superiorName == null)
                    //{
                    //    userVendor.SuperiorName = "-";
                    //}
                    //else
                    //{
                    //    userVendor.SuperiorName = superiorName;
                    //}
                    userVendor.CreatedBy = ds.Tables[0].Rows[i]["created_by"].ToString();
                    userVendor.UpdatedBy = ds.Tables[0].Rows[i]["updated_by"].ToString();
                    userVendor.CreationDate = DateTime.Parse(ds.Tables[0].Rows[i]["creation_date"].ToString());
                    userVendor.LastUpdatedOn = DateTime.Parse(ds.Tables[0].Rows[i]["last_updated_on"].ToString());

                    userVendor.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["is_active"].ToString());

                    vendorData = new Vendor();
                    vendorData.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["vendor_id"].ToString());
                    vendorData.VendorName = ds.Tables[0].Rows[i]["vendor_name"].ToString();
                    vendorData.VendorType = ds.Tables[0].Rows[i]["vendor_type"].ToString();
                    userVendor.VendorData = vendorData;

                    allUserVendors.Add(userVendor);
                }
                return allUserVendors;
            }
            catch
            {
                return allUserVendors;
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult Index()
        {
            List<UserVendor> allUserVendors = GetAllUserVendors();
            List<UserVendor> activeUserVendors = allUserVendors.Where(u => u.IsActive == 1).ToList();
            ViewBag.AllUserVendors = activeUserVendors;
            return View();
        }

        public ActionResult CreateUserVendor()
        {
            VendorController vendorController = new VendorController();
            List<Vendor> allVendors = vendorController.GetAllVendors();
            //List<UserVendor> allUserVendors = GetAllUserVendors();

            ViewBag.AllVendors = allVendors;
            //ViewBag.AllUserVendors = allUserVendors;
            return View();
        }
        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.VendorStoredProcedure(parameters, spname));
        }

        private string GenerateUserVendorID()
        {
            //get last id from creation date
            DateTime today = DateTime.Now;
            string todayFormat = today.Year.ToString() + today.Month.ToString().PadLeft(2,'0') + today.Day.ToString().PadLeft(2, '0');
            SqlCommand cmd = new SqlCommand("sp_generate_user_vendor_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@date", todayFormat);
            conn.Open();
            var result = cmd.ExecuteScalar();

            int newNumber = 1;
            conn.Close();

            if (result != null && result.ToString() != "")
            {
                int lastID = int.Parse(result.ToString().Substring(8));
                newNumber = ++lastID;
            }
            string newID = todayFormat + newNumber.ToString().PadLeft(3, '0');
            return newID.ToString();
        }

        public Email GetEmailBody(string userID)
        {
            DataSet ds = new DataSet();
            Email dataEmail = new Email();
            userID = "neI-v642qlw-Fr4vD241" + userID+ "9lIm";
            //char ke 20 sebanyak 11 char
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_select_email_body", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Kategori", "Account Activation");
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds);
                conn.Close();

                dataEmail.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                dataEmail.EmailSubject = ds.Tables[0].Rows[0]["email_subject"].ToString();
                dataEmail.EmailBody = ds.Tables[0].Rows[0]["email_body"].ToString();
                return dataEmail;
            }
            catch
            {
                return dataEmail;
            }
        }

        public ActionResult ActivateUserAccount(string userID)
        {
            //fniweth2398ri2fnwfowf123456789
            string realID = userID.Substring(20,11);
            List<UserVendor> allUsers = GetAllUserVendors();
            UserVendor userFound = allUsers.Find(u => u.UserID == realID);
            if (userFound == null)
            {
                return Json("Link salah/Akun tidak ditemukan", JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.ActiveNIK = realID;
                ViewBag.ActiveUser = userFound;
                return View();
            }
        }

        public ActionResult SaveNewPassword(UserVendor user)
        {
            //System.Diagnostics.Debug.WriteLine(user);
            string encryptedPassword = EncryptionHelper.Encrypt(user.Password);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update_password", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", user.ID);
                cmd.Parameters.AddWithValue("@password", encryptedPassword);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("1");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult SaveUserVendor(UserVendor userVendor)
        {
            string idSuperior = userVendor.SuperiorID;
            if (idSuperior == null||idSuperior=="")
            {
                idSuperior = "-";
            }
            try
            {
                //create new user vendor
                string newID = GenerateUserVendorID();
                string result;
                SqlCommand cmd = new SqlCommand("sp_create_user_vendor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", newID);
                cmd.Parameters.AddWithValue("@user_vendor_name", userVendor.UserVendorName);
                cmd.Parameters.AddWithValue("@vendor_id", userVendor.VendorData.ID);
                cmd.Parameters.AddWithValue("@username", userVendor.Username);
                cmd.Parameters.AddWithValue("@email", userVendor.Email);
                //cmd.Parameters.AddWithValue("@superior_id", idSuperior);
                cmd.Parameters.AddWithValue("@created_by", "Default User");
                cmd.Parameters.AddWithValue("@updated_by", "Default User");
                conn.Open();
                result = (string)cmd.ExecuteScalar();
                conn.Close();

                //kirim email aktivasi
                if(result =="Valid")
                {
                    try
                    {
                        Email emailData = GetEmailBody(newID);
                        var sub = emailData.EmailSubject;
                        var body = emailData.EmailBody;
                        //string bodie = body.Replace(System.Environment.NewLine, string.Empty);
                        SmtpClient mailObj = new SmtpClient("mail.kalbe.co.id");
                        var msg = new MailMessage();
                        //mess.From = senderEmail;
                        //msg.From = new MailAddress("it.bintang7@gmail.com", "CAPA B7 Mailing System");
                        msg.From = new MailAddress("notification@bintang7.com", "B7 Connect Mailing System");
                        msg.Body = body;
                        msg.Subject = sub;
                        //mess.Bcc.Add(senderEmail);
                        msg.Priority = MailPriority.High;
                        msg.IsBodyHtml = true;
                        msg.To.Add(userVendor.Email);

                        //mailObj.Send(msg);

                        return Json("success");
                    }
                    catch (Exception ex)
                    {
                        SqlCommand cmdLog = new SqlCommand("sp_create_log", logEmailConn);
                        cmdLog.CommandType = CommandType.StoredProcedure;
                        cmdLog.Parameters.AddWithValue("@modul", "Sending CAPA Activation Email");
                        cmdLog.Parameters.AddWithValue("@user", "CAPA Default User");
                        cmdLog.Parameters.AddWithValue("@message", ex.Message);
                        logEmailConn.Open();
                        cmdLog.ExecuteNonQuery();
                        logEmailConn.Close();

                        return Json("error, check log");
                    }
                }
                else
                {
                    return Json("error");
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult FindUserVendorById(UserVendor userVendor)
        {
            List<UserVendor> allUserVendors = GetAllUserVendors();
            UserVendor userFound = allUserVendors.Find(v => v.ID == userVendor.ID);
            UserVendor notFound = new UserVendor();
            notFound.UserVendorName = "not found";
            if (userFound == null)
            {
                return Json(notFound);
            }
            return Json(userFound);
        }

        public ActionResult UpdateUserVendor(UserVendor userVendor)
        {
            //Session["nik_active"].ToString()
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE USER_VENDORS SET user_vendor_name = @user_vendor_name, username=@username, email=@email, updated_by=@updated_by, last_updated_on=@last_updated_on WHERE RecordID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", userVendor.ID);
                    cmd.Parameters.AddWithValue("user_vendor_name", userVendor.UserVendorName);
                    cmd.Parameters.AddWithValue("username", userVendor.Username);
                    cmd.Parameters.AddWithValue("email", userVendor.Email);
                    //use session
                    cmd.Parameters.AddWithValue("updated_by", "Default User");
                    cmd.Parameters.AddWithValue("last_updated_on", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return Json("success");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult DeleteUserVendor(string userID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE USER_VENDORS SET is_active = 0, updated_by=@updated_by, last_updated_on=@last_updated_on WHERE RecordID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", userID);
                    //use session
                    cmd.Parameters.AddWithValue("updated_by", "Default User");
                    cmd.Parameters.AddWithValue("last_updated_on", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                //}
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}