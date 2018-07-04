using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandHandlers.Business
{
    public class VerifyAgencyCommandHandler : CommandHandler<VerifyAgencyCommand>
    {
        protected async override Task HandleCommandAsync(VerifyAgencyCommand command)
        {
            var organisation = _context.Organisations.FirstOrDefault(o => o.TokenConfirmationMail.Equals(command.Token));
            if(organisation != null && !organisation.IsActivateAccount)
            {
                organisation.IsActivateAccount = true;
                _context.SaveChanges();
            }
        }
    }
}
