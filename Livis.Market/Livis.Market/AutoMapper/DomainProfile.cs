using AutoMapper;
using LivinMarket.Contact.Model.Commands.Business;
using LivinMarket.Organisation.Model.Commands.Business;
using LivinMarket.Product.Model.Queries.Business;
using Livis.Market.Data;
using Livis.Market.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<OrganisationViewModel, AddOrganisationCommand>();
            CreateMap<AddOrganisationCommand, Organisation>();
            CreateMap<AgencyViewModel, Organisation>()
                .ForSourceMember(x => x.ConfirmationEmail, opt => opt.Ignore())
                .ForSourceMember(x => x.Password, opt => opt.Ignore())
                .ForSourceMember(x => x.PasswordConfirmation, opt => opt.Ignore())
                .ForSourceMember(x => x.Footer, opt => opt.Ignore())
                .ForSourceMember(x => x.Header, opt => opt.Ignore())
                .ForSourceMember(x => x.Title, opt => opt.Ignore());
            CreateMap<Organisation, AgencyViewModel>()
                .ForMember(x => x.ConfirmationEmail, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.PasswordConfirmation, opt => opt.Ignore())
                .ForMember(x => x.Footer, opt => opt.Ignore())
                .ForMember(x => x.Header, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.Ignore());
            CreateMap <ProductShoppingView, ShoppingProductViewModel>()
                .ForMember(x => x.Photos, opt => opt.Ignore())
                .ForMember(x => x.Price, opt => opt.Ignore())
                .ForMember(x => x.Variants, opt => opt.Ignore())
                .ForMember(x => x.Footer, opt => opt.Ignore())
                .ForMember(x => x.Header, opt => opt.Ignore())
                .ForMember(x => x.Options, opt => opt.Ignore())
                .ForMember(x => x.Title, opt => opt.Ignore());
            CreateMap<AgencyViewModel, AgencyConfirmationCommand>()
                .ForMember(x => x.UserConfirmation, opt => opt.Ignore());
            CreateMap<AgencyViewModel, CustomerConfirmationCommand>()
                .ForMember(x => x.UserConfirmation, opt => opt.Ignore());
            CreateMap<Organisation, OrganisationView>()
                .ForMember(x => x.ViewUrl, opt => opt.Ignore())
                .ForMember(x => x.EditUrl, opt => opt.Ignore());
            CreateMap<AddressView, ShippingViewModel>()
                .ForMember(x => x.CustomerId, opt => opt.Ignore());
        }
    }
}
