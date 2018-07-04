using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LivinMarket.Organisation.Exceptions.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using LivinMarket.Organisation.Model.Queries.Business;
using Livis.Market.Data;
using Livis.Market.Models.ViewModel;
using Livis.Market.Utilities.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Livis.Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrganisationController : BaseLivisServerController
    { 
        public async Task<IActionResult> Index()
        {
            var response = await _processor.ProcessQueryAsync<OrganisationQuery, OrganisationResponse>(new OrganisationQuery()
            {
                NameOrId = string.Empty
            });
            var organisations = response.Organisations;
            var models = Mapper.Map<List<Organisation>, List<OrganisationView>>(organisations.ToList());
            var listModel = models.ToList();
            listModel.ForEach(x => 
                x.ViewUrl = $"/Organisation/Update?orgId={x.OrganisationId}"
            );
            listModel.ForEach(x =>
                x.EditUrl = $"/Organisation/Update?orgId={x.OrganisationId}"
            );
            return View(listModel);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateRegistrationStatus(string.Empty);
            return View();
        }

        public async Task<IActionResult> Update(Guid orgId)
        {
            var response = await _processor.ProcessQueryAsync<OrganisationQuery, OrganisationResponse>(new OrganisationQuery() {
                NameOrId = orgId.ToString()
            });
            var organisation = response.Organisations.FirstOrDefault();
            if(organisation == null)
            {
               await PopulateRegistrationStatus("Not found any organsaition match your condition.");
            }
            else
            {
               await PopulateRegistrationStatus(string.Empty);
            }
            var model = _mapper.Map<Livis.Market.Data.Organisation, OrganisationViewModel>(organisation);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrganisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<OrganisationViewModel, UpdateOrganisationCommand>(model);
                try
                {
                    await _processor.ProcessCommandAsync<UpdateOrganisationCommand>(command);
                }
                catch (OrgNotFoundException ex)
                {
                    await PopulateRegistrationStatus(ex.Message);
                    return View();
                }
            }
            await PopulateRegistrationStatus(string.Empty);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<OrganisationViewModel, AddOrganisationCommand>(model);
                try
                {
                    string scheme = Url.ActionContext.HttpContext.Request.Scheme;
                    command.ConfirmationLink = Url.Action("VerifyAgency", "Confiramtion", null, scheme);
                    await _processor.ProcessCommandAsync<AddOrganisationCommand>(command);
                    return RedirectToAction(nameof(Update), new { orgId = command.OrganisationId });
                }
                catch(UserExistedException ex)
                {
                    await PopulateRegistrationStatus(ex.Message);
                    return View();
                }
            }
            await PopulateRegistrationStatus(string.Empty);
            return View(model);
        }

        private async Task PopulateRegistrationStatus(string errorMessage)
        {
            ViewBag.RegistrationStatus = new SelectList(ComponentsHelper.GetRegistrationStatusList(), "Key", "Value");
            ViewBag.ErrorMessage = errorMessage;
        }
    }
}