using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Infrastructure
{
    public class SessionConstant
    {
        public static readonly string OneTimePurchase = "OneTimePurchase";
    }

    public class Connection
    {
        public const string ConnectionStringName = "LivisConnection";
        public const string AssemblyMigrationName = "Livis.Market.Data";
    }
}
