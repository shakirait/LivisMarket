using LivinMarket.Organisation.Exceptions.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Market.Data;
using Livis.Market.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandValidators.Business.Rules
{
    public class NotFoundOrganisationValidationRule : IValidationRule<UpdateOrganisationCommand>
    {
        private const string _USER_Not_EXIST = "Not found organisation.";
        private readonly LivisMarketContext _context;

        public NotFoundOrganisationValidationRule(LivisMarketContext context)
        {
            _context = context;
        }
        public async Task TestAsync(UpdateOrganisationCommand command)
        {
            var isExisted = _context.Organisations.Any(o => o.OrganisationId == command.OrganisationId);
            if (!isExisted)
                throw new OrgNotFoundException(_USER_Not_EXIST);
        }
    }
}