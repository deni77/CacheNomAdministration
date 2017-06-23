using CacheNomAdministration.SoapServices.SoapProxy;
using CacheNomAdministration.SoapServices.Utility;
using CacheNomAdministration.WebApi.Models;
using CacheNomAdministration.WebApi.Models.Totalizers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CacheNomAdministration.WebApi.Controllers
{
    public class TotalizerController : Controller
    {
        // GET: Totalizer
        public ActionResult Index()
        {
           return View();
        }

       public ActionResult GetAll([DataSourceRequest] DataSourceRequest request)
        {
            ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
            client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));

            IEnumerable<IndexViewModels> allTotalizers = client.GetConfiguration().Select(b => new IndexViewModels
                                                                {
                                                                    TotalizerTag = b.TotalizerTag,
                                                                    ControlPointID = Convert.ToInt32(b.ControlPointID),
                                                                    FlowDirection = Convert.ToInt32(b.FlowDirection),
                                                                    ControlerCode = Convert.ToInt32(b.ControlerCode),
                                                                    PositionNumber = Convert.ToInt32(b.PositionNumber),
                                                                    ControlPointName = b.ControlPointName,
                                                                    EngineeringUnit = b.EngineeringUnit,
                                                                    ProductCode = b.ProductCode
                                                                }).ToList();
            return Json(allTotalizers.ToDataSourceResult(request));
        }

        public ViewResult GetByControlPointID([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId();
            ViewBag.DropDownValuesControlPointId = slcontrolPointId;

            IEnumerable<SelectListItem> slunit = CreateDropDownUnits();
            ViewBag.EngineeringUnit = slunit;
                    
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetByControlPointIdGrid(int ControlPointID, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId();
            ViewBag.DropDownValuesControlPointId = slcontrolPointId;

           // if (ControlPointID != null && ControlPointID!=0)
           // {
                ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
                client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));

                var all = client.GetConfigurationId(Convert.ToInt32(ControlPointID));
                if (all==null)
                {
                   return Json(new List<IndexViewModels>().ToDataSourceResult(request));
                }

                IEnumerable<IndexViewModels> allTotalizers = all.Select(b => new IndexViewModels
                {
                    TotalizerTag = b.TotalizerTag,
                    ControlPointID = Convert.ToInt32(b.ControlPointID),
                    FlowDirection = Convert.ToInt32(b.FlowDirection),
                    ControlerCode = Convert.ToInt32(b.ControlerCode),
                    PositionNumber = Convert.ToInt32(b.PositionNumber),
                    ControlPointName = b.ControlPointName,
                    EngineeringUnit = b.EngineeringUnit,
                    ProductCode = b.ProductCode
                }).ToList();
                return Json(all.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);        
            //}
           // return Json(new List<IndexViewModels>().ToDataSourceResult(request)); 
        }

       // [HttpPost]
        public ActionResult VerifyProductCode (string productCode)
        {
          if (productCode!=null)
            {
                bool result = false;
                try
                {
                    ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
                    client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
                    result = client.IsValidProductCode(productCode);
                }
                catch
                {
                    // TODO : gre6ka
                }

                string suggestedName = String.Format(CultureInfo.InvariantCulture, "Въведете валиден код на продукта !");

                if (!result)
                { return Json(suggestedName, JsonRequestBehavior.AllowGet); } // false - gre6kata se vizualizira
                else
                { return Json(true, JsonRequestBehavior.AllowGet); }
            }

           return Json(true, JsonRequestBehavior.AllowGet);
        }

        public bool VerifyProductCodeBool(string productCode)
        {
            if (productCode != null)
            {
                bool result = false;
                try
                {
                    ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
                    client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
                    result = client.IsValidProductCode(productCode);
                }
                catch
                {
                    // TODO : gre6ka
                }

                string suggestedName = String.Format(CultureInfo.InvariantCulture, "Въведете валиден код на продукта !");

                if (!result)
                { return false; }
                else
                {
                    return true;
                }
            }

            return false;
        }


        [HttpGet]
        public ActionResult VerifyControlPointName(int controlPointId, string controlPointName)
        {
            if (controlPointName != null)
            {
                bool result = false;
                if (controlPointId == 0)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                try
                {
                    ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
                    client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
                    result = client.IsValidControlPointName(Convert.ToInt64(controlPointId), controlPointName);
                }
                catch
                {
                   // TODO : gre6ka
                }

                if (!result)
                { return Json(false, JsonRequestBehavior.AllowGet); }
                else
                { return Json(true, JsonRequestBehavior.AllowGet); }
            }
            return Json(data: null);
        }

        // GET: Totalizer/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> slunit = CreateDropDownUnits();
            IEnumerable<SelectListItem> slflowdirection = CreateDropDownFlowDirection();
            IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId();

            ViewBag.EngineeringUnit = slunit;
            ViewBag.DropDownValuesFlowDirection = slflowdirection;
            ViewBag.DropDownValuesControlPointId = slcontrolPointId;
            ViewBag.DropDownValuesControlerCode = slflowdirection;
            return View();
        }

       public static IEnumerable<SelectListItem> CreateDropDownUnits()
        {
           var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "LTR15/KGM", Value = "LTR15/KGM" });
                list.Add(new SelectListItem { Text = "KGM", Value = "KGM" });
                list.Add(new SelectListItem { Text = "TNE", Value = "TNE" });
                list.Add(new SelectListItem { Text = "LTR15", Value = "LTR15" });
             return list;
        }

        public static IEnumerable<SelectListItem> CreateDropDownFlowDirection()
        {
            var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "1", Value = "1" });list.Add(new SelectListItem { Text = "2", Value = "2" });
             return list;
        }

        public static IEnumerable<SelectListItem> CreateDropDownControlPointId()
        {
            ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
            client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));

            var list = new List<SelectListItem>();
            var arrayOfControlPointId = client.GetIdControlPoint();

            for (int i = 0; i < arrayOfControlPointId.Count; i++)
            {
                list.Add(new SelectListItem
                                    {
                                        Value = arrayOfControlPointId[i].MeasuringPointId.ToString(),
                                        Text = arrayOfControlPointId[i].MeasuringPointId.ToString()
                                    });
            }
            return list;
        }
           
        // POST: Totalizer/Create
        [HttpPost]
        public ActionResult Create(AddTotalizerTagViewModel tag)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    CacheNomAdministration.SoapServices.SoapProxy.ControlPointView addtag = new SoapServices.SoapProxy.ControlPointView() {
                        ControlPointName = tag.ControlPointName,
                        EngineeringUnit = tag.EngineeringUnit,
                        ProductCode = tag.ProductCode,
                        TotalizerTag = tag.TotalizerTag,
                        ControlPointID = tag.ControlPointID,
                        FlowDirection = tag.FlowDirection,
                        ControlerCode = tag.ControlerCode
                   };
                   
                    ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
                    client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
                    client.AddRecord(addtag);

                    //ако има грешки от Caheto, Тук да ги прехвана

                    IEnumerable<SelectListItem> slunit = CreateDropDownUnits(); ViewBag.EngineeringUnit = slunit;
                   IEnumerable<SelectListItem> slflowdirection = CreateDropDownFlowDirection(); ViewBag.DropDownValuesFlowDirection = slflowdirection;
                   IEnumerable<SelectListItem> slcontrolercode = CreateDropDownFlowDirection(); ViewBag.DropDownValuesControlerCode = slcontrolercode;
                    IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId(); ViewBag.DropDownValuesControlPointId = slcontrolPointId;

                    ModelState.Clear();
                    return View();
                }
                else
                {
                   IEnumerable<SelectListItem> slunit = CreateDropDownUnits(); ViewBag.EngineeringUnit = slunit;
                   IEnumerable<SelectListItem> slflowdirection = CreateDropDownFlowDirection(); ViewBag.DropDownValuesFlowDirection = slflowdirection;
                   IEnumerable<SelectListItem> slcontrolercode = CreateDropDownFlowDirection(); ViewBag.DropDownValuesControlerCode = slcontrolercode;
                   IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId(); ViewBag.DropDownValuesControlPointId = slcontrolPointId;

                    TempData["ResultMessage"] = new ResultOfAction() { Type = ResultType.Error, Message = "Hеправилно въведени входни данни във формата !!!!", };
                    return View();
                }
            }
            catch
            {
               IEnumerable<SelectListItem> slunit = CreateDropDownUnits(); ViewBag.EngineeringUnit = slunit;
               IEnumerable<SelectListItem> slflowdirection = CreateDropDownFlowDirection(); ViewBag.DropDownValuesFlowDirection = slflowdirection;
                IEnumerable<SelectListItem> slcontrolercode = CreateDropDownFlowDirection(); ViewBag.DropDownValuesControlerCode = slcontrolercode;
                IEnumerable<SelectListItem> slcontrolPointId = CreateDropDownControlPointId(); ViewBag.DropDownValuesControlPointId = slcontrolPointId;

                ModelState.Clear();
                TempData["ResultMessage"] = new ResultOfAction() {Type = ResultType.Error, Message = "Възникна грешка при добавянето на таталайзер тага !!!", };
                return View();
            }
        }

       [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request,IndexViewModels totalizator)
        {
            if (totalizator != null)
            {
                TotalizatorDestroy(totalizator);
            }

            return Json(new[] { totalizator }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, IndexViewModels totalizator)
        {
            var result = VerifyProductCodeBool(totalizator.ProductCode);
            TempData["ResultMessage"] = new ResultOfAction() { };
            if (result && totalizator!=null)
            {
                  TotalizatorUpdate(totalizator);
                return Json(new[] { totalizator }.ToDataSourceResult(request, ModelState));
            }
            return Json(null);
        }

        private void TotalizatorUpdate(IndexViewModels totalizator)
        {
            ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
            client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
            client.UpdateRecordTag(totalizator.TotalizerTag,Convert.ToInt32(totalizator.ControlPointID), Convert.ToInt32(totalizator.FlowDirection),
                                        Convert.ToInt32(totalizator.ControlerCode), Convert.ToInt32(totalizator.PositionNumber),totalizator.ProductCode,totalizator.EngineeringUnit);
        }

        private void TotalizatorDestroy(IndexViewModels totalizator)
        {
            ManagementNomenclaturesSoapClient client = new ManagementNomenclaturesSoapClient();
            client.Endpoint.EndpointBehaviors.Add(new MessageBehavior("_system", "SYS"));
            client.DeleteRecordTag(totalizator.TotalizerTag,Convert.ToInt32(totalizator.ControlPointID), Convert.ToInt32(totalizator.FlowDirection),
                                            Convert.ToInt32(totalizator.ControlerCode),   Convert.ToInt32(totalizator.PositionNumber));
        }
    }
}
