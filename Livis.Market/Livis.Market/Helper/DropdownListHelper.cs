using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Helper
{
    public class DropdownListHelper
    {
        public static IEnumerable<SelectListItem> GetTitle()
        {
            return new List<SelectListItem>() {
                new SelectListItem(){
                    Text = "Mr",
                    Value = "Mr",
                },
                new SelectListItem(){
                    Text = "Ms",
                    Value = "Ms",
                }
            };
        }

        public static IEnumerable<SelectListItem> GetCountry()
        {
            return new List<SelectListItem>() {
                new SelectListItem(){
                    Text = "Germany",
                    Value = "Germany",
                },
                new SelectListItem(){
                    Text = "Australia",
                    Value = "Australia",
                },
                new SelectListItem(){
                    Text = "VietNam",
                    Value = "VietNam",
                }
            };
        }
    }
}
