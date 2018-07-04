using Livis.Market.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Livis.Market.Data;

namespace LivinMarket.Product.Model.Commands.Business
{
    public class AddProductCommand : ICommand
    {
        public Livis.Market.Data.Product NewProduct { get; set; }
    }
}
