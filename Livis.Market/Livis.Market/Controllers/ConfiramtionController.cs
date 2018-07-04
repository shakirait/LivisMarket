using System.Threading.Tasks;
using LivinMarket.Organisation.Model.Commands.Business;
using Microsoft.AspNetCore.Mvc;

namespace Livis.Market.Controllers
{
    public class ConfiramtionController : BaseController
    {
        public async Task<IActionResult> VerifyAgency(string token)
        {
            await _processor.ProcessCommandAsync<VerifyAgencyCommand>(new VerifyAgencyCommand()
            {
                Token = token
            });
            return GetMemberPageToRedirect();
        }
    }
}