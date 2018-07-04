using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Livis.Market.Data;
using System.ComponentModel.DataAnnotations;

namespace LivinMarket.Organisation.Model.Commands.Business
{
    public class VerifyAgencyCommand : ICommand
    {
        public string Token { get; set; }
    }
}
