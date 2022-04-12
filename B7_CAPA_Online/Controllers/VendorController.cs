using B7_CAPA_Online.Models;
using B7_CAPA_Online.Scripts.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static B7_CAPA_Online.Models.KoordinatorModel;
namespace B7_CAPA_Online.Controllers
{
    public class VendorController : Controller
    {
        readonly DataAccess DAL = new DataAccess();
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MASTERVENDOR"].ToString());
        public List<Vendor> GetAllVendors()
        {
            DataSet ds = new DataSet();
            List<Vendor> allVendors = new List<Vendor>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_vendor_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Option", "GET ALL VENDORS");
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Vendor vendor = new Vendor();
                    vendor.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                    vendor.VendorName = ds.Tables[0].Rows[i]["vendor_name"].ToString();
                    vendor.TypeID = Convert.ToInt32(ds.Tables[0].Rows[i]["type_id"].ToString());
                    vendor.VendorType = ds.Tables[0].Rows[i]["vendor_type"].ToString();
                    vendor.SuperiorName = ds.Tables[0].Rows[i]["superior_name"].ToString();
                    //vendor.SuperiorGroupName = ds.Tables[0].Rows[i]["superior_group_name"].ToString();
                    vendor.CreatedBy = ds.Tables[0].Rows[i]["created_by"].ToString();
                    vendor.UpdatedBy = ds.Tables[0].Rows[i]["updated_by"].ToString();
                    vendor.CreationDate = DateTime.Parse(ds.Tables[0].Rows[i]["creation_date"].ToString());
                    vendor.LastUpdatedOn = DateTime.Parse(ds.Tables[0].Rows[i]["last_updated_on"].ToString());

                    vendor.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["is_active"].ToString());

                    allVendors.Add(vendor);
                }
                return allVendors;
            }
            catch
            {
                return allVendors;
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult FindVendorById(Vendor vendor)
        {
            List<Vendor> allVendors = GetAllVendors();
            Vendor vendorFound = allVendors.Find(v => v.ID == vendor.ID);
            Vendor notFound = new Vendor();
            notFound.VendorName = "not found";
            if (vendorFound == null)
            {
                return Json(notFound);
            }
            return Json(vendorFound);
        }

        public ActionResult Index()
        {
            List<Vendor> allVendors = GetAllVendors();
            List<Vendor> activeVendors = allVendors.Where(v => v.IsActive == 1).ToList();
            ViewBag.AllVendors = activeVendors;
            return View();
        }

        public ActionResult TipeVendorIndex()
        {
            List<VendorType> allVendorTypes = GetAllVendorTypes();
            ViewBag.AllVendorTypes = allVendorTypes;
            return View();
        }

        public ActionResult EditTipeVendor(int tipeVendorID)
        {
            List<VendorType> allVendorTypes = GetAllVendorTypes();
            VendorType type = allVendorTypes.Find(v => v.ID == tipeVendorID);
            ViewBag.ActiveVendorType = type;
            return View();
        }

        public ActionResult UpdateTipeVendor(VendorType vendorType)
        {
            try
            {
                var dictionary = new Dictionary<string, object>{
                    { "@Option", "UPDATE VENDOR TYPE" },
                    { "@type_id", vendorType.ID },
                    { "@superior_id", vendorType.SuperiorID },
                    { "@updated_by", vendorType.UpdatedBy },
                };
                var parameters = new DynamicParameters(dictionary);
                var result = DAL.VendorStoredProcedure(parameters, "sp_vendor_data");
                return Json("success");
            }
            catch(Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult GetListManagerUp()
        {
            var dictionary = new Dictionary<string, object>{
                { "Option", "LIST MANAGER UP" },
            };
            var parameters = new DynamicParameters(dictionary);
            var result = DAL.VendorStoredProcedure(parameters, "sp_vendor_data");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VendorDynamicController(DynamicModel Models , string spname )
        {
            var parameters = new DynamicParameters(Models.Model);
            return Json(DAL.VendorStoredProcedure(parameters, spname));
        }

        public List<VendorType> GetAllVendorTypes()
        {
            DataSet ds = new DataSet();
            List<VendorType> allVendorTypes = new List<VendorType>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_vendor_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Option", "GET VENDOR TYPES");
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    VendorType vendorType = new VendorType();
                    vendorType.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                    vendorType.Type = ds.Tables[0].Rows[i]["type"].ToString();
                    vendorType.SuperiorID = ds.Tables[0].Rows[i]["superior_id"].ToString();
                    vendorType.SuperiorName = ds.Tables[0].Rows[i]["superior_name"].ToString();
                    vendorType.CreatedBy = ds.Tables[0].Rows[i]["created_by"].ToString();
                    vendorType.UpdatedBy = ds.Tables[0].Rows[i]["updated_by"].ToString();
                    vendorType.CreationDate = DateTime.Parse(ds.Tables[0].Rows[i]["creation_date"].ToString());
                    vendorType.LastUpdatedOn = DateTime.Parse(ds.Tables[0].Rows[i]["last_updated_on"].ToString());

                    vendorType.IsActive = Convert.ToInt32(ds.Tables[0].Rows[i]["is_active"].ToString());

                    allVendorTypes.Add(vendorType);
                }
                return allVendorTypes;
            }
            catch
            {
                return allVendorTypes;
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult GetOracleVendors()
        {
            try
            {
                var dictionary = new Dictionary<string, object>{
                    { "@Option", "GET ALL ORACLE VENDORS" },
                };
                var parameters = new DynamicParameters(dictionary);
                var result = DAL.VendorStoredProcedure(parameters, "sp_vendor_data");
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public ActionResult CreateVendor()
        {
            List<VendorType> allVendorTypes = GetAllVendorTypes();
            ViewBag.AllVendorTypes = allVendorTypes;

            //Vendor vendorFound = FindVendorById(vendorID);
            //ViewBag.VendorInfo = vendorFound;
            return View();
            //if (Session["nama_user"] != null)
            //{

            //}
            //else
            //{
            //    return RedirectToAction("Login", "Authentication");
            //}
        }
        public ActionResult SaveVendor(Vendor vendor)
        {
            //Session["nik_active"].ToString()
            try
            {
                SqlCommand cmd = new SqlCommand("sp_vendor_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Option", "CREATE VENDOR");
                cmd.Parameters.AddWithValue("@vendor_name", vendor.VendorName);
                cmd.Parameters.AddWithValue("@type_id", vendor.TypeID);
                //use session
                cmd.Parameters.AddWithValue("@created_by", "Default User");
                cmd.Parameters.AddWithValue("@updated_by", "Default User");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                //}
                var result = 1;
                return Json(result);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult UpdateVendor(Vendor vendor)
        {
            //Session["nik_active"].ToString()
            try
            {
                SqlCommand cmd = new SqlCommand("sp_vendor_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Option", "UPDATE VENDOR");
                cmd.Parameters.AddWithValue("@id", vendor.ID);
                cmd.Parameters.AddWithValue("@vendor_name", vendor.VendorName);
                cmd.Parameters.AddWithValue("@type_id", vendor.TypeID);
                //use session
                cmd.Parameters.AddWithValue("@updated_by", "Default User");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Json("success");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        public ActionResult DeleteVendor(string vendorID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE VENDORS SET is_active = 0, updated_by=@updated_by, last_updated_on=@last_updated_on WHERE RecordID = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", vendorID);
                    //use session
                    cmd.Parameters.AddWithValue("@updated_by", vendorID);
                    cmd.Parameters.AddWithValue("@last_updated_on", DateTime.Now);
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