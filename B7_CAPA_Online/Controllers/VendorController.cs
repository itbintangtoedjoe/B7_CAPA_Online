using B7_CAPA_Online.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B7_CAPA_Online.Controllers
{
    public class VendorController : Controller
    {
        private readonly SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MASTERVENDOR"].ToString());
        public List<Vendor> GetAllVendors()
        {
            DataSet ds = new DataSet();
            List<Vendor> allVendors = new List<Vendor>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_select_all_vendors", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
                    vendor.SuperiorGroupName = ds.Tables[0].Rows[i]["superior_group_name"].ToString();
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

        public ActionResult Index()
        {
            List<Vendor> allVendors = GetAllVendors();
            ViewBag.AllVendors = allVendors;
            return View();
        }

        public List<VendorType> GetAllVendorTypes()
        {
            DataSet ds = new DataSet();
            List<VendorType> allVendorTypes = new List<VendorType>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_select_all_vendor_types", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;

                dataAdapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    VendorType vendorType = new VendorType();
                    vendorType.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["RecordID"].ToString());
                    vendorType.Type = ds.Tables[0].Rows[i]["type"].ToString();
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

        public ActionResult CreateVendor()
        {
            List<VendorType> allVendorTypes = GetAllVendorTypes();
            ViewBag.AllVendorTypes = allVendorTypes;
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
                SqlCommand cmd = new SqlCommand("sp_create_vendor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vendor_name", vendor.VendorName);
                cmd.Parameters.AddWithValue("@type_id", vendor.TypeID);
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
    }
}