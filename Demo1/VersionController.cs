using BajajAutoBLETrackerAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace BajajAutoBLETrackerAdmin.Controllers
{
    public class VersionController : Controller
    {
        BajajDBEntities objBajajDBEntities = new BajajDBEntities();

        public ActionResult Create()
        {
            try
            {
                Details();
            }
            catch (Exception ex)
            {
                objBajajDBEntities.spADErrorLogSave("Message = " + ex.Message + "<br>Source=" + ex.Source + "<br>StackTrace = " + ex.StackTrace + "<br>TargetSite = " + ex.TargetSite + "<br>InnerException = " + ex.InnerException + "<br>HelpLink = " + ex.HelpLink + "<br>Data = " + ex.Data, "AbsolutePath=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                using (var objBajajDBEntities = new BajajDBEntities())
                {
                    tblVersion objtblVersion = new tblVersion();
                    objtblVersion.Name = form["Name"];

                    if (form["MustRequired"] != null)
                    {
                        objtblVersion.MustRequired = checkBoxToBool(form["MustRequired"]);
                    }

                    objBajajDBEntities.spADVersionSave(objtblVersion.Name, objtblVersion.MustRequired);
                    Details();
                    return View();
                }
            }
            catch (Exception ex)
            {
                objBajajDBEntities.spADErrorLogSave("Message = " + ex.Message + "<br>Source=" + ex.Source + "<br>StackTrace = " + ex.StackTrace + "<br>TargetSite = " + ex.TargetSite + "<br>InnerException = " + ex.InnerException + "<br>HelpLink = " + ex.HelpLink + "<br>Data = " + ex.Data, "AbsolutePath=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                Details();
                return View();
            }

        }

        bool checkBoxToBool(string cbVal)
        {

            if (string.Compare(cbVal, "false") == 0)
                return false;
            if (string.Compare(cbVal, "true,false") == 0)
                return true;
            else
                throw new ArgumentNullException(cbVal);
        }

        public void Details()
        {
            try
            {
                using (var objBajajDBEntities = new BajajDBEntities())
                {
                    var VersionDetails = objBajajDBEntities.spADVersionGetByAll().ToList();

                    ViewBag.VersionDetails = VersionDetails;

                }
            }
            catch (Exception ex)
            {
                objBajajDBEntities.spADErrorLogSave("Message = " + ex.Message + "<br>Source=" + ex.Source + "<br>StackTrace = " + ex.StackTrace + "<br>TargetSite = " + ex.TargetSite + "<br>InnerException = " + ex.InnerException + "<br>HelpLink = " + ex.HelpLink + "<br>Data = " + ex.Data, "AbsolutePath=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);

            }
        }

    }
}