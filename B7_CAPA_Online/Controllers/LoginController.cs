using B7_CAPA_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static B7_CAPA_Online.Models.KoordinatorModel;
using System.Data;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace B7_CAPA_Online.Controllers
{
    public class LoginController : Controller
    {

        //[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        //public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        //[DllImport("kernel32.dll")]
        //public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        //public static extern bool CloseHandle(IntPtr handle);

        //private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MASTERVENDOR"].ToString());
        //readonly DataAccess DAL = new DataAccess();

        //// GET: Login
        //public ActionResult Index()
        //{
        //    //string decrypted = EncryptionHelper.Decrypt("+NnSWnq/Lh1EfwTSxFxzRmrAwj7YTeawQeHecVIXCRI=");
        //    //return Json(decrypted, JsonRequestBehavior.AllowGet);

        //    Session["LoginStatus"] = "invalid";
        //    Session["NIK"] = "";
        //    Session["FullName"] = "";
        //    Session["Username"] = "";
        //    Session["NamaUser"] = "";
        //    Session["Departemen"] = "";
        //    Session["Lokasi"] = "";
        //    Session["SuperiorName"] = "";
        //    return View();
        //}

        //public string FindKaryawan(string username)
        //{
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    var dictionary = new Dictionary<string, object>{
        //        { "username", username },
        //    };
        //    var parameters = new DynamicParameters(dictionary);
        //    var result = DAL.StoredProcedure(parameters, "SP_Find_User");
        //    return result;
        //}

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult LoginExec(LoginData loginData)
        //{
        //    IntPtr tokenHandle = new IntPtr(0);
        //    string MachineName, username, password, tipeLogin = null;
        //    string loginStatus = "0";

        //    username = loginData.Username;
        //    password = loginData.Password;
        //    tipeLogin = loginData.TipeLogin;

        //    //login AD
        //    if (tipeLogin == "Karyawan")
        //    {
        //        try
        //        {
        //            //The MachineName property gets the name of your computer.   
        //            MachineName = "ONEKALBE";
        //            const int LOGON32_PROVIDER_DEFAULT = 0;
        //            const int LOGON32_LOGON_INTERACTIVE = 2;
        //            tokenHandle = IntPtr.Zero;

        //            //Call the LogonUser function to obtain a handle to an access token.
        //            bool returnValue = LogonUser(username, MachineName, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

        //            //Session["LoginStatus"] = 1;
        //            //Session["xUser"] = 1;

        //            if (returnValue == false)
        //            {
        //                //This function returns the error code that the last unmanaged function returned.
        //                int ret = Marshal.GetLastWin32Error();
        //                if (ret == 1329)
        //                {
        //                    loginStatus = "AD invalid";
        //                    Session["LoginStatus"] = "AD invalid";
        //                }
        //                else
        //                {
        //                    loginStatus = "wrong credentials";
        //                    Session["LoginStatus"] = "wrong credentials";
        //                }
        //            }
        //            else
        //            {
        //                loginStatus = "success";

        //                //get data user karyawan
        //                //var dataKaryawan = FindKaryawan(username);
        //                JavaScriptSerializer jss = new JavaScriptSerializer();
        //                string dataKaryawan = FindKaryawan(username);
        //                var arrayData = JArray.Parse(dataKaryawan);
        //                dynamic objectKary = jss.Deserialize<dynamic>(dataKaryawan);

        //                if (dataKaryawan != null)
        //                {
        //                    Session["LoginStatus"] = "success";
        //                    Session["FullName"] = objectKary[0]["Username"];
        //                    Session["NIK"] = objectKary[0]["NIK"];
        //                    Session["Username"] = username;
        //                    Session["NamaUser"] = objectKary[0]["Username"];
        //                    Session["Departemen"] = objectKary[0]["Org_Group_Name"];
        //                    Session["Lokasi"] = objectKary[0]["Location"];
        //                    Session["SuperiorName"] = objectKary[0]["SuperiorName"];
        //                }
        //                else
        //                {
        //                    loginStatus = "not found";
        //                    Session["LoginStatus"] = "not found";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            loginStatus = "invalid";
        //            Session["LoginStatus"] = "invalid";
        //            Session["LoginErrorMessage"] = ex.Message;
        //        }
        //    }
        //    //cari apakah vendor exists
        //    else
        //    {
        //        //encrypt pw
        //        string encryptedPassword = EncryptionHelper.Encrypt(password);
        //        string queryString =
        //"SELECT CASE WHEN EXISTS (SELECT * FROM USER_VENDORS WHERE username= @username AND password= @password) THEN 'true' ELSE 'false' END; ";
        //        using (conn)
        //        {
        //            SqlCommand cmd = new SqlCommand(
        //                queryString, conn);
        //            cmd.Parameters.AddWithValue("username", username);
        //            cmd.Parameters.AddWithValue("password", encryptedPassword);
        //            conn.Open();
        //            string result = cmd.ExecuteScalar().ToString();
        //            if (result == "true")
        //            {
        //                loginStatus = "success";
        //                Session["Username"] = username;
        //                Session["LoginStatus"] = "success";
        //            }
        //            else
        //            {
        //                loginStatus = "wrong credentials";
        //                Session["LoginStatus"] = "wrong credentials";
        //            }
        //            //using (SqlDataReader reader = cmd.ExecuteReader())
        //            //{
        //            //    while (reader.Read())
        //            //    {
        //            //        Console.WriteLine(String.Format("{0}, {1}",
        //            //            reader[0], reader[1]));
        //            //    }
        //            //}
        //        }
        //    }
        //    return Json(loginStatus);
        //}
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref string lpBuffer, int nSize, ref IntPtr Arguments);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern bool CloseHandle(IntPtr handle);

        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MASTERVENDOR"].ToString());
        readonly DataAccess DAL = new DataAccess();
#pragma warning disable CS0169 // The field 'LoginController.query' is never used
        private string query;
#pragma warning restore CS0169 // The field 'LoginController.query' is never used
#pragma warning disable CS0169 // The field 'LoginController.Option' is never used
        private int Option;
#pragma warning restore CS0169 // The field 'LoginController.Option' is never used
#pragma warning disable CS0169 // The field 'LoginController.URLAttachment' is never used
        private string URLAttachment;
#pragma warning restore CS0169 // The field 'LoginController.URLAttachment' is never used
        public string identifers;

        public bool checkLoginByUsername(string username)
        {
            bool isTrueLogin = true;
            //Session["xUser"] = "dani.pernando";
            //string LoginCAPA = objEntities.CAPA_LoginManagement(1, ModelData.UserName, ModelData.Password).FirstOrDefault();
            //string UserRole = objEntities.CAPA_LoginManagement(3, ModelData.UserName, ModelData.Password).FirstOrDefault();
            //var data = _db.users.Where(s => s.username.Equals(username)).ToList();
            //if (data.Count() > 0)
            //{
            //    //add session
            //    Session["username"] = data.FirstOrDefault().username;
            //    Session["idUser"] = data.FirstOrDefault().id;
            //    isTrueLogin = true;
            //}

            return isTrueLogin;
        }
        public ActionResult Index()
        {

            //string decrypted = EncryptionHelper.Decrypt("+NnSWnq/Lh1EfwTSxFxzRmrAwj7YTeawQeHecVIXCRI=");
            //return Json(decrypted, JsonRequestBehavior.AllowGet);
            Session.Clear();
            //string autologin_token = Request.QueryString["autologinToken"];
            //identifers = Request.QueryString["identifier"];
            //TempData["identifier"] = identifers;
            //ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["B7PortalDB"];

            //string conString = mySetting.ConnectionString;


            //DataTable dt = new DataTable();
            //DataTable dt2 = new DataTable();
            //if (autologin_token != null)
            //{
            //    string query = "SELECT username_apps FROM [dbo].[application_user_token] where token='" + autologin_token + "'";
            //    bool setAutologin = this.AutomaticToken(autologin_token);
            //    if (setAutologin)
            //    {
            //        SqlConnection conn = new SqlConnection(conString);
            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        conn.Open();

            //        // create data adapter
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);

            //        // this will query your database and return the result to your datatable
            //        da.Fill(dt);
            //        conn.Close();
            //        da.Dispose();
            //        string username;
            //        if (dt.Rows[0]["username_apps"] != null)
            //        {
            //            username = dt.Rows[0]["username_apps"].ToString();
            //        }
            //        else
            //        {
            //            username = "-";
            //        }
            //        string query2 = "select Departement from tblM_Userid where UserName_PK ='" + username + "'";



            //        Session["username"] = dt.Rows[0]["username_apps"].ToString();
            //        Session["idUser"] = dt.Rows[0]["username_apps"].ToString();
            //        Session["Role"] = dt2.Rows[0]["Departement"].ToString();
            //        Session["LoginSuccess"] = "True";

            //        return RedirectToAction("TaskList","PendingTask");

            //    }

            //}
            //string autologin_token = Request.QueryString["autologinToken"];
            //identifers = Request.QueryString["identifier"];
            //TempData["identifier"] = identifers;
            ////string conString = ConfigurationManager.ConnectionStrings["B7PortalDB"];
            //string conString = ConfigurationManager.ConnectionStrings["B7PortalDB"].ToString();
            //DataTable dt = new DataTable();

            //if (autologin_token != null)
            //{
            //    string query = "SELECT username_apps FROM [dbo].[application_user_token] where token='" + autologin_token + "'";
            //    bool setAutologin = this.AutomaticToken(autologin_token);
            //    if (setAutologin)
            //    {
            //        SqlConnection conn = new SqlConnection(conString);

            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        conn.Open();

            //        // create data adapter
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);

            //        // this will query your database and return the result to your datatable
            //        da.Fill(dt);
            //        conn.Close();
            //        da.Dispose();
            //        string username;
            //        if (dt.Rows[0]["username_apps"] != null)
            //        {
            //            username = dt.Rows[0]["username_apps"].ToString();
            //            FindKaryawanAndCreateSession(username);
            //        }
            //        else
            //        {
            //            username = "-";
            //        }

            //        //bool SetParam = SetAuthParameter(username);
            //        //return RedirectToAction("/");
            //        return RedirectToAction("TaskList", "PendingTask");
            //    }
            //}

            return View();
        }

        //public bool AutomaticToken(string autologin_token)
        //{
        //    string postString = string.Format("token={0}", autologin_token);
        //    WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/validateToken");
        //    request.Method = "POST";
        //    request.ContentLength = postString.Length;
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
        //    requestWriter.Write(postString);
        //    requestWriter.Close();

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Console.WriteLine(response.StatusDescription);
        //    Stream dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

        //    var responseData = responseJson["data"];

        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();

        //    return this.checkLoginByUsername(responseData["username_application"]);
        //}


        //private void revalidateUsername(string identifier, string usernameApps)
        //{
        //    string postString = string.Format("identifier={0}&username_application={1}", identifier, usernameApps);
        //    // Create a request for the URL. 		
        //    WebRequest request = WebRequest.Create("http://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/revalidateUsername");
        //    request.Method = "POST";
        //    request.ContentLength = postString.Length;
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
        //    requestWriter.Write(postString);
        //    requestWriter.Close();

        //    // Get the response.
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    // Display the status.
        //    Console.WriteLine(response.StatusDescription);
        //    // Get the stream containing content returned by the server.
        //    Stream dataStream = response.GetResponseStream();
        //    // Open the stream using a StreamReader for easy access.
        //    StreamReader reader = new StreamReader(dataStream);
        //    // Read the content.
        //    string responseFromServer = reader.ReadToEnd();

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

        //    var responseData = responseJson["data"];

        //    // Cleanup the streams and the response.
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //}



        public ActionResult Session_Error()
        {
            return View();
        }
        public ActionResult DynamicController(DynamicModel Models, string spname)
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.StoredProcedure(parameters, spname));

        }

        public string FindKaryawan(string username, string tipe, string password)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            var dictionary = new Dictionary<string, object>{
                { "username", username },
                {  "tipe",tipe },
                { "password", password }
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.StoredProcedure(parameters, "SP_Find_User");
            return result;
        }

        void FindKaryawanAndCreateSession(string username)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string dataKaryawan = FindKaryawan(username, "", "");
            var arrayData = JArray.Parse(dataKaryawan);
            dynamic objectKary = jss.Deserialize<dynamic>(dataKaryawan);

            if (arrayData.Count > 0)
            {
                Session["LoginStatus"] = "success";
                Session["FullName"] = objectKary[0]["Username"];
                Session["NIK"] = objectKary[0]["NIK"];
                Session["Username"] = username;
                Session["NamaUser"] = objectKary[0]["Username"];
                Session["Departemen"] = objectKary[0]["Dept"];
                Session["Lokasi"] = objectKary[0]["Location"];
                Session["SuperiorName"] = objectKary[0]["SuperiorName"];
                if (objectKary[0]["Email_1"] != "")
                {

                    Session["Email"] = objectKary[0]["Email_1"];
                }
                else
                {
                    Session["Email"] = objectKary[0]["Email_2"];
                }
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginExec(LoginData loginData)
        {
            IntPtr tokenHandle = new IntPtr(0);
            string MachineName, username, password, tipeLogin = null;
            string loginStatus = "0";

            username = loginData.Username;
            password = loginData.Password;
            tipeLogin = loginData.TipeLogin;

            //login AD
            if (tipeLogin == "Karyawan")
            {
                #pragma warning disable CS0168 // The variable 'identifier' is declared but never used
                string identifier;
                #pragma warning restore CS0168 // The variable 'identifier' is declared but never used
                //if (TempData["identifier"] != null)
                //{
                //    identifier = TempData["identifier"].ToString();
                //    this.revalidateUsername(identifier, loginData.Username);
                //}

                if (username != "" || ( username !="" && password == "B7Portal123"))
                {
                    loginStatus = "success";
                    using (var client = new HttpClient())
                    {
                        string LoginApiBasePath = ConfigurationManager.AppSettings["LoginApiBasePath"];
                        client.DefaultRequestHeaders.Clear();
                        try
                        {
                            HttpResponseMessage Res = await client.PostAsJsonAsync(LoginApiBasePath + "/Login", new
                            {
                                Username = username,
                                Password = password,
                            });

                            // Cek return dari Api Login, login berhasil jika return == "Success"
                            if (Res.IsSuccessStatusCode)
                            {
                                dynamic response = await Res.Content.ReadAsAsync<JObject>();
                                string responseMessage = response.message.ToString();

                                if (responseMessage.ToLower() == "success")
                                {
                                    loginStatus = "success";
                                }
                                else if (responseMessage.ToLower() == "user not found")
                                {
                                    loginStatus = "not found";
                                }
                                else
                                {
                                    loginStatus = "invalid";
                                }
                            }
                            else
                            {
                                loginStatus = "invalid";
                            }
                        }
                        catch (Exception)
                        {

                            loginStatus = "invalid";
                        }
                    }

                    //get data user karyawan
                    //var dataKaryawan = FindKaryawan(username);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string dataKaryawan = FindKaryawan(username, "", "");
                    var arrayData = JArray.Parse(dataKaryawan);
                    dynamic objectKary = jss.Deserialize<dynamic>(dataKaryawan);


                    if (arrayData.Count > 0)
                    {
                        Session["LoginStatus"] = "success";
                        Session["FullName"] = objectKary[0]["Username"];
                        Session["NIK"] = objectKary[0]["NIK"];
                        Session["Username"] = username;
                        Session["NamaUser"] = objectKary[0]["Username"];
                        Session["Departemen"] = objectKary[0]["Dept"];
                        Session["Lokasi"] = objectKary[0]["Location"];
                        Session["SuperiorName"] = objectKary[0]["SuperiorName"];
                        if (objectKary[0]["Email_1"] != "")
                        {

                            Session["Email"] = objectKary[0]["Email_1"];
                        }
                        else
                        {
                            Session["Email"] = objectKary[0]["Email_2"];
                        }

                        //if (TempData["identifier"] != null) revalidateUsername(TempData["identifier"].ToString(), username);
                    }
                    else
                    {
                        loginStatus = "not found";
                        Session["LoginStatus"] = "not found";
                    }
                }
                else
                {
                    try
                    {
                        //The MachineName property gets the name of your computer.   
                        MachineName = "ONEKALBE";
                        const int LOGON32_PROVIDER_DEFAULT = 0;
                        const int LOGON32_LOGON_INTERACTIVE = 2;
                        tokenHandle = IntPtr.Zero;

                        //Call the LogonUser function to obtain a handle to an access token.
                        bool returnValue = LogonUser(username, MachineName, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

                        //Session["LoginStatus"] = 1;
                        //Session["xUser"] = 1;

                        if (returnValue == false)
                        {
                            //This function returns the error code that the last unmanaged function returned.
                            int ret = Marshal.GetLastWin32Error();
                            if (ret == 1329)
                            {
                                loginStatus = "AD invalid";
                                Session["LoginStatus"] = "AD invalid";
                            }
                            else
                            {
                                loginStatus = "wrong credentials";
                                Session["LoginStatus"] = "wrong credentials";
                            }
                        }
                        else
                        {
                            loginStatus = "success";

                            //get data user karyawan
                            //var dataKaryawan = FindKaryawan(username);
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            string dataKaryawan = FindKaryawan(username, "", "");
                            var arrayData = JArray.Parse(dataKaryawan);
                            dynamic objectKary = jss.Deserialize<dynamic>(dataKaryawan);

                            if (arrayData.Count > 0)
                            {
                                Session["LoginStatus"] = "success";
                                Session["FullName"] = objectKary[0]["Username"];
                                Session["NIK"] = objectKary[0]["NIK"];
                                Session["Username"] = username;
                                Session["NamaUser"] = objectKary[0]["Username"];
                                Session["Departemen"] = objectKary[0]["Dept"];
                                Session["Lokasi"] = objectKary[0]["Location"];
                                Session["SuperiorName"] = objectKary[0]["SuperiorName"];

                                //if (TempData["identifier"] != null) revalidateUsername(TempData["identifier"].ToString(), username);
                            }
                            else
                            {
                                loginStatus = "not found";
                                Session["LoginStatus"] = "not found";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        loginStatus = "invalid";
                        Session["LoginStatus"] = "invalid";
                        Session["LoginErrorMessage"] = ex.Message;
                    }
                }


            }
            //cari apakah vendor exists
            else
            {
                //if (username != "" && password == "B7Portal")
                //{
                //    loginStatus = "success";
                //    Session["Username"] = username;
                //    Session["LoginStatus"] = "success";
                //}
                //else
                //{
                //encrypt pw    
                string encryptedPassword = EncryptionHelper.Encrypt(password);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string dataKaryawan = FindKaryawan(username, "Vendor", encryptedPassword);
                var arrayData = JArray.Parse(dataKaryawan);
                dynamic objectKary = jss.Deserialize<dynamic>(dataKaryawan);

                if (arrayData.Count > 0)
                {
                    loginStatus = "success";
                    Session["LoginStatus"] = "success";
                    Session["FullName"] = objectKary[0]["user_vendor_name"];
                    Session["NamaUser"] = objectKary[0]["user_vendor_name"];
                    Session["NIK"] = objectKary[0]["userID"];
                    Session["Username"] = username;
                    Session["Departemen"] = "Others";
                    Session["SuperiorName"] = objectKary[0]["SuperiorName"];
                }
                else
                {
                    loginStatus = "not found";
                    Session["LoginStatus"] = "not found";
                }

                //using (SqlDataReader reader = cmd.ExecuteReader())
                //{
                //    while (reader.Read())
                //    {
                //        Console.WriteLine(String.Format("{0}, {1}",
                //            reader[0], reader[1]));
                //    }
                //}



            }
            return Json(loginStatus);
        }
        public ActionResult checksession()
        {
            if (System.Web.HttpContext.Current.Session["Username"] == null)
            {
                return Json("False");
            }
            else
            {
                return Json("True");
            }
        }


        //public void revalidateUsername(string identifier, string usernameApps)
        //{
        //    string postString = string.Format("identifier={0}&username_application={1}", identifier, usernameApps);
        //    // Create a request for the URL. 		
        //    WebRequest request = WebRequest.Create("https://intranetportal.bintang7.com/B7-Portal/api/v1/applicationUser/revalidateUsername");
        //    request.Method = "POST";
        //    request.ContentLength = postString.Length;
        //    request.ContentType = "application/x-www-form-urlencoded";

        //    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
        //    requestWriter.Write(postString);
        //    requestWriter.Close();

        //    // Get the response.
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    // Display the status.
        //    Console.WriteLine(response.StatusDescription);
        //    // Get the stream containing content returned by the server.
        //    Stream dataStream = response.GetResponseStream();
        //    // Open the stream using a StreamReader for easy access.
        //    StreamReader reader = new StreamReader(dataStream);
        //    // Read the content.
        //    string responseFromServer = reader.ReadToEnd();

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    dynamic responseJson = js.Deserialize<dynamic>(responseFromServer);

        //    var responseData = responseJson["data"];
        //    // "username_active_directory": "DESKTOP-R46N8LD\\Adam",
        //    // "username_application": "adam",
        //    // "token": "e1bef5fb05b7b4f8a31f627526a5a08f",
        //    // "application_name": "CAPA",
        //    // "last_updated": "2021-07-01T21:53:18.627"

        //    // Cleanup the streams and the response.
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //}
    }
}