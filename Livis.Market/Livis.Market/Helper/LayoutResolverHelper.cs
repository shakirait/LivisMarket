using Livis.Market.Models.ViewModel;
using Livis.Market.Models.ViewModel.ComponentViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Livis.Market.Helper
{
    public static class LayoutResolverHelper
    {
        internal static void InitLayout(LayoutViewModel model, UrlHelper context)
        {

            //Loading header and footer on config


            ///Header
            ///
            model.Header = new HeaderViewModel();
            model.Header.NavigationLinksAreaHeader = new LinkItemCollection(new List<LinkItem>()
            {
                new LinkItem(){ Target = context.Action("Index", "Shopping", null), Title = "Product" },
                new LinkItem(){ Target = context.Action("Cart", "Shopping", null), Title = "Cart" },
                new LinkItem(){ Target = context.Action("Index", "Company", null), Title = "Company Profile" },
                new LinkItem(){ Target = context.Action("Map", "Company", null), Title = "Map" },
                new LinkItem(){ Target = context.Action("ContactUs", "Company", null), Title = "Contact Us" },
            });
            model.Header.LogoImageHeader = @"../img/LIVIN_MARKET-03.jpg";

            model.Header.MembershipHeaderViewModel = new MembershipHeaderViewModel()
            {
                IsLevisUser = false,
                LoginLink =  context.Action("Index", "Login", null),
                WholesalersLink = new Uri("https://www.youtube.com/watch?v=eiBinM-f-Pk"),
                MenuItemMembershipHeader = new LinkItemCollection(new List<LinkItem>()
                {
                    new LinkItem(){ Target = context.Action("Member", "Account", null), Title = "Member Profile" },
                    new LinkItem(){ Target = context.Action("Addresses", "Account", null), Title = "Addresses Management" },
                    new LinkItem(){ Target = context.Action("Inquiry", "Account", null), Title = "Contact Inquiry" }
                })
            };

            ///Footer
            ///
            model.Footer = new FooterViewModel();
            model.Footer.SocialFooter = new LinkItemCollection(new List<LinkItem>
            {
                  new LinkItem(){ Target = "https://www.youtube.com/watch?v=eiBinM-f-Pk", Description = " <p> <span> Join the Livin community </span> <br> <span> Please tell us your voice </span> </p>" ,
                  Title = "fb-icon social-icon"},
                  new LinkItem(){ Target = "https://www.youtube.com/watch?v=eiBinM-f-Pk", Description = " <p> <span> Join the Livin community </span> <br> <span> Please tell us your voice </span> </p>",
                  Title = "yt-icon social-icon"}
            });
            model.Footer.NavigationLinksAreaFooter = new LinkItemCollection(new List<LinkItem>()
                {
                    new LinkItem(){ Target = context.Action("Index", "Shopping", null), Title = "Product", Group = "Shopping" },
                    new LinkItem(){ Target = context.Action("Cart", "Shopping", null), Title = "Cart", Group = "Shopping" },
                    new LinkItem(){ Target = context.Action("Map", "Company", null), Title = "Map", Group = "Find Livin Shop near you" },
                    new LinkItem(){ Target = context.Action("ContactUs", "Company", null), Title = "Company Profile", Group = "About Livin Market" } 
                });
            model.Footer.CustomerServiceFooter = new FooterInfo() {
                Alias = "Customer Service",
                 OpenTime = "Opening hours: Monday - Friday 08:00-19:00",
                 Tel = "0049-162-2792803"
            };
            model.Footer.CopyrightTextFooter = "Copyrighted 2018 - Livin Market";
        }
    }
}
