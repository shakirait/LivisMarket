using LivinMarket.Organisation.Model.Commands.Business;
using Livis.Market.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace LivinMarket.Organisation.CommandHandlers.Business
{
    public class UpdateOrganisationCommandHandler : CommandHandler<UpdateOrganisationCommand>
    {
        protected async override Task HandleCommandAsync(UpdateOrganisationCommand command)
        {
            var organisation = _context.Organisations.First(x => x.OrganisationId == command.OrganisationId);
            organisation.AccountHolder = command.AccountHolder;
            organisation.AccountNumber = command.AccountNumber;
            organisation.BankName = command.BankName;
            organisation.BankNumber = command.BankNumber;
            organisation.BranchName = command.BranchName;
            organisation.BranchNumber = command.BranchNumber;
            organisation.City = command.City;
            organisation.FaxNumber = command.FaxNumber;
            organisation.FirstName = command.FirstName;
            organisation.LastName = command.LastName;
            organisation.Latitude = command.Latitude;
            organisation.Longtitude = command.Longtitude;
            organisation.OpenTime = command.OpenTime;
            organisation.PhoneNumber = command.PhoneNumber;
            organisation.PostCode = command.PostCode;
            organisation.Prefecture = command.Prefecture;
            organisation.RegistrationStatus = command.RegistrationStatus;
            organisation.ShopName = command.ShopName;
            organisation.Street = command.Street;
            organisation.WebsiteUrl = command.WebsiteUrl;

            _context.SaveChanges();
        }
    }
}
