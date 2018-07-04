using Livis.Market.Data;
using Livis.Market.Models.ViewModel;
using Livis.Market.Utilities.Services.UserContext;
using System;
using System.Threading.Tasks;

namespace Livis.Market.Services
{
    public interface ICartService
    {

        Task AddToCart(CartView cart, string sku, string variantId, decimal quantity);

        Task ChangeQuantity(CartView cart, string sku, string variantId, decimal quantity);

        bool HasItems(CartView cart);

        bool HasPaymentMethodSeleced(CartView cart);

        bool HasShippingAddressSelected(CartView cart);

        Task<CartView> LoadCart(Guid customerId);

        Task<CartView> LoadOrCreateCart(Guid customerId);

        void MergeCart(CartView fromCartView, CartView toCartView);

        void RemoveToCart(CartView cart, string sku, string variantId);

        void SetCartCurrency(CartView cart, string currency);

        decimal CountLineItems(CartView cart);

        void UpdateTotalPrice(CartView cart);

        decimal GetSubTotal(CartView cart, string sku, string variantId);

        decimal GetQuantity(CartView cart, string sku, string variantId);

        decimal GetTotalPriceOfCart(CartView cart);

        Task GetOrCreateDefaultShippingAddress(CartView cart);

        ShippingView GetOrCreateDefaultShipping(CartView cart);

        void UpdateShippingCart(CartView cart, ShippingViewModel model);
    }
}
