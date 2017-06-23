using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CacheNomAdministration.WebApi.Models.Totalizers
{
    public class IndexViewModels
    {
        [Display(Name = "Име контролна точка")]
        public string ControlPointName { get; set; }

        [UIHint("UnitEditor")]
        [Display(Name = "Мерна единица")]
        public string EngineeringUnit { get; set; }

        [Display(Name = "Код продукт")]
        [Required(ErrorMessage = "Полето е задължително")]
        [Remote(action: "VerifyProductCode", controller: "Totalizer")] // , ErrorMessage = "Въведете валиден код на продукта !")
        public string ProductCode { get; set; }

        [Display(Name = "Тотализатор по продукт по контролна точка")]
        public string TotalizerTag { get; set; }

        [Display(Name = "Id на контролна точка")]
        public int ControlPointID { get; set; }

        [Display(Name = "Тип посока")]
        public int FlowDirection { get; set; }

        [Display(Name = "Код контролер")]
        public int ControlerCode { get; set; }

        [Display(Name = "Пореден номер на продукта")]
        public int PositionNumber { get; set; }
     
    }
}