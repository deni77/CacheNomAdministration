using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CacheNomAdministration.WebApi.Models.Totalizers
{
    public class AddTotalizerTagViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        [RegularExpression(@"^[TK]{2}[0-9]{6}$", ErrorMessage = "Името на контролната точка започва с 'ТК' и е последвано от шест цифри !")]
        [Display(Name = "Име контролна точка")]
        [Remote(action: "VerifyControlPointName", controller: "Totalizer",AdditionalFields = "ControlPointID", ErrorMessage = "Несъответствие между Id и име на контролна точка !", HttpMethod = "Get")]
        public string ControlPointName { get; set; }

        [Display(Name = "Мерна единица")]
        [Required(ErrorMessage = "Полето е задължително")]
        public string EngineeringUnit { get; set; }

        [Display(Name = "Код продукт")]
        [Required(ErrorMessage = "Полето е задължително")]
        [Remote(action: "VerifyProductCode", controller: "Totalizer", HttpMethod = "Get",ErrorMessage = "Въведете валиден код на продукта !")]
        public string ProductCode { get; set; }

        [Display(Name = "Тотализатор по продукт по контролна точка")]
        [Required(ErrorMessage = "Полето е задължително")]
        public string TotalizerTag { get; set; }

        //[RegularExpression(@"^[0-9]*$", ErrorMessage = "Може да въвеждате само положителни числа !")]
        [Display(Name = "Id на контролна точка")]
        [Required(ErrorMessage = "Полето е задължително")]
        public int ControlPointID { get; set; }

        [Display(Name = "Тип посока")]
        [Required(ErrorMessage = "Полето е задължително")]
        public int FlowDirection { get; set; }

        [Display(Name = "Код контролер")]
        [Required(ErrorMessage = "Полето е задължително")]
        public int ControlerCode { get; set; }

      // [Display(Name = "Пореден номер на продукта")]
       // [Required(ErrorMessage = "Полето е задължително")]
       // public int PositionNumber { get; set; }
    }
}