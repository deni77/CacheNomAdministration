using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CacheNomAdministration.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public object Totalizer { get; private set; }

        public ActionResult Index()
        {
           // IEnumerable<SelectListItem> slunit = TotalizerController.CreateDropDownUnits();
           // IEnumerable<SelectListItem> slflowdirection = TotalizerController.CreateDropDownFlowDirection();
          //  IEnumerable<SelectListItem> slcontrolercode = TotalizerController.CreateDropDownFlowDirection();
          //  IEnumerable<SelectListItem> slcontrolPointId = TotalizerController.CreateDropDownControlPointId();
          // TempData["EngineeringUnit"] = slunit;
           // TempData["DropDownValuesFlowDirection"] = slflowdirection;
           // TempData["DropDownValuesControlerCode"] = slcontrolercode;
          //  TempData["DropDownValuesControlPointId"] = slcontrolPointId;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}