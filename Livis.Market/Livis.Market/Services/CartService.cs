using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Livis.Market.Data;
using Livis.Market.Utilities.ApplicationSettings;
using Livis.Market.Utilities.Services.UserContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Newtonsoft.Json;
using Livis.Market.Utilities.Helper;
using System.Threading.Tasks;
using Livis.Market.Infrastructure;
using LivinMarket.Order.Model.Queries.Business;
using LivinMarket.Product.Model.Queries.Business;
using Livis.Market.Models.ViewModel;

namespace Livis.Market.Services
{
    public class CartService : ICartService
    {
        protected readonly IMapper _mapper;
        protected readonly IAppSettings _appSettings;
        protected readonly ILivisUserContext _userContext;
        protected readonly IProductService _productService;
        protected readonly IRequestProcessor _processor;

        public CartService(IAppSettings appSettings, ILivisUserContext userContext, IMapper mapper, IRequestProcessor processor)
        {
            _mapper = mapper;
            _appSettings = appSettings;
            _userContext = userContext;
            _processor = processor;
            _productService = new ProductService(_userContext, _processor);
        }

        private string GetImageUrl(Data.Product product)
        {
            if(product.PartnerPhotos != null && product.PartnerPhotos.Count > 0)
            {
                return product.PartnerPhotos.First().BlobUrl;
            }
            if (product.Photos != null && product.Photos.Count > 0)
            {
                return product.Photos.First().BlobUrl;
            }
            return string.Empty;

        }
        private CartView CreateDefaultCartView(Guid customerId)
        {
            return new CartView()
            {
                Created = DateTime.Now,
                Currency = _appSettings.DefaultCurrency,
                CustomerId = customerId,
                Forms = new List<OrderFormView>(),
                Market = _appSettings.DefaultMartketName,
                Name = _appSettings.DefaultShopName,
                Modified = DateTime.Now,
                Id = -1,
                OrderLink = new OrderGroupView() {
                    Name = _appSettings.DefaultShopName,
                    CustomerId = customerId,
                    OrderGroupId = 0,
                    Status = OrderStatus.OnHold
                }
            };
        }
        private OrderFormView GetOrCreateDefaultOrderForm(CartView cart)
        {
            if(cart.Forms.Count == 0)
            {
                cart.Forms.Add(new OrderFormView() {
                  AuthorizedPaymentTotal = 0,
                  CapturedPaymentTotal = 0,
                  HandlingTotal = 0,
                  Name = _appSettings.DefaultShopName,
                  OrderFormId = 0,
                  Payments = new List<PaymentView>(),
                  Shipment = new List<ShippingView>()
                });
            }

            return cart.Forms.First();
        }
        private PaymentView GetOrCreateDefaultPayment(CartView cart)
        {
            var orderForm = cart.Forms.First();
            if(orderForm.Payments.Count == 0)
            {
                orderForm.Payments.Add(new PaymentView() {
                    Amount= 0,
                    BillingAddress = null,
                    CustomerName = string.Empty,
                    ExtendedData = string.Empty,
                    PaymentMethodName = string.Empty,
                    Status = PaymentStatus.Pending
                });
            }

            return orderForm.Payments.First();
        }
        public void UpdateShippingCart(CartView cart, ShippingViewModel model)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            shipping.ShippingAddress = _mapper.Map<ShippingViewModel, AddressView>(model);
        }

        public ShippingView GetOrCreateDefaultShipping(CartView cart)
        {
            var orderForm = GetOrCreateDefaultOrderForm(cart);
            if(orderForm.Shipment.Count == 0)
            {
                orderForm.Shipment.Add(new ShippingView()
                {
                    ShippingAddress = null,
                    LineItems = new List<LineItemView>()
                });
            }
            return orderForm.Shipment.First();
        }
        private async Task<SerializableCart> GetCartObject(Guid customerId)
        {
            var response = await _processor.ProcessQueryAsync<GetCartQuery, GetCartResponse>(new GetCartQuery()
            {
                CustomerId = customerId,
                MartketName = _appSettings.DefaultMartketName,
                ShopName = _appSettings.DefaultShopName
            });
            return response.Cart;
        }

        public async Task AddToCart(CartView cart, string sku, string variantId, decimal quantity)
        {
            var response = await _processor.ProcessQueryAsync<ProductQuery, ProductResponse>(new ProductQuery()
            {
                NameOrId = sku
            });
            var products = response.Products;
            if (products != null && products.Length > 0)
            {
                var product = products.FirstOrDefault(p => p.Sku == sku);
                if (product != null)
                {
                    var shipping = GetOrCreateDefaultShipping(cart);
                    var price = await _productService.GetProductPrice(sku);
                    var variants = !string.IsNullOrEmpty(product.VariantKeys) ? JsonConvert.DeserializeObject<List<Variant>>(product.VariantKeys) : new List<Variant>();
                    var variant = variants.FirstOrDefault(v => v.Id.Equals(variantId));
                    if (sku == variantId || variant == null)
                    {
                        shipping.LineItems.Add(new LineItemView()
                        {
                            DisplayName = product.Name,
                            Sku = sku,
                            Quantity = quantity,
                            LineItemId = 0,
                            VariantId = sku,
                            PlacedPrice = price,
                            ExtendedPrice = price * quantity,
                            ImageUrl = GetImageUrl(product)
                        });
                    }
                    else
                    {
                        if (variant != null)
                        {
                            shipping.LineItems.Add(new LineItemView()
                            {
                                DisplayName = product.Name,
                                Sku = sku,
                                Quantity = quantity,
                                LineItemId = 0,
                                VariantId = variant.Id,
                                PlacedPrice = price,
                                ExtendedPrice = price * quantity,
                                ImageUrl = variant.BlockUrl
                            });
                        }
                    }
                }
            }
        }

        public async Task ChangeQuantity(CartView cart, string sku, string variantId, decimal quantity)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            var lineItem = shipping.LineItems.FirstOrDefault(x => x.Sku.Equals(sku) && x.VariantId.Equals(variantId));
            if(lineItem != null)
            {
                lineItem.Quantity = quantity;
                lineItem.ExtendedPrice = lineItem.PlacedPrice * lineItem.Quantity;
            }
            else
            {
                await AddToCart(cart, sku, variantId, quantity);
            }
        }

        public bool HasItems(CartView cart)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            return shipping.LineItems.Count > 0;
        }

        public decimal CountLineItems(CartView cart)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            decimal count = 0;
            foreach(var lineItem in shipping.LineItems)
            {
                count += lineItem.Quantity;
            }
            return count;
        }

        public bool HasPaymentMethodSeleced(CartView cart)
        {
            var payment = GetOrCreateDefaultPayment(cart);
            return !string.IsNullOrEmpty(payment.PaymentMethodName);
        }

        public bool HasShippingAddressSelected(CartView cart)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            return shipping.ShippingAddress != null;
        }

        public async Task<CartView> LoadCart(Guid  customerId)
        {
            var cart = await GetCartObject(customerId);
            if(cart != null)
            {
                return JsonConvert.DeserializeObject<CartView>(cart.Data);
            }
            return null;
        }

        public async Task<CartView> LoadOrCreateCart(Guid customerId)
        {
            var cartView = await LoadCart(customerId);
            if(cartView == null)
            {
                cartView = CreateDefaultCartView(customerId);
            }
            return cartView;
        }

        public void MergeCart(CartView fromCartView, CartView toCartView)
        {
            var fromShipping = GetOrCreateDefaultShipping(fromCartView);
            if (fromShipping != null && fromShipping.LineItems.Count > 0)
            {
                var toShipping = GetOrCreateDefaultShipping(toCartView);
                foreach (var lineItem in fromShipping.LineItems)
                {
                    var toLineItem = toShipping.LineItems.FirstOrDefault(x => x.Sku.Equals(lineItem.Sku) && x.VariantId.Equals(lineItem.VariantId));
                    if (toLineItem == null)
                    {
                        toShipping.LineItems.Add(new LineItemView()
                        {
                            DisplayName = lineItem.DisplayName,
                            ExtendedPrice = lineItem.ExtendedPrice,
                            LineItemId = lineItem.LineItemId,
                            PlacedPrice = lineItem.PlacedPrice,
                            Quantity = lineItem.Quantity,
                            Sku = lineItem.Sku,
                            VariantId = lineItem.VariantId,
                            ImageUrl = lineItem.ImageUrl
                        });
                    }
                    else
                    {
                        toLineItem.Quantity = toLineItem.Quantity + lineItem.Quantity;
                        toLineItem.ExtendedPrice = toLineItem.Quantity * toLineItem.PlacedPrice;
                    }
                }
            }
        }

        public void RemoveToCart(CartView cart, string sku, string variantId)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            var lineItem = shipping.LineItems.FirstOrDefault(x => x.Sku.Equals(sku) && x.VariantId.Equals(variantId));
            if(lineItem != null)
            {
                shipping.LineItems.Remove(lineItem);
            }
        }

        public void SetCartCurrency(CartView cart, string currency)
        {
           if(ComponentsHelper.Currency.ContainsKey(currency))
            cart.Currency = currency;
        }

        public void UpdateTotalPrice(CartView cart)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            foreach(var lineItem in shipping.LineItems)
            {
                lineItem.ExtendedPrice = lineItem.Quantity * lineItem.PlacedPrice;
            }
        }

        public decimal GetSubTotal(CartView cart, string sku, string variantId)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            var lineItem = shipping.LineItems.FirstOrDefault(x => x.Sku.Equals(sku) && x.VariantId.Equals(variantId));
            if (lineItem != null)
            {
                return lineItem.PlacedPrice * lineItem.Quantity;
            }
            return 0;
        }

        public decimal GetQuantity(CartView cart, string sku, string variantId)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            var lineItem = shipping.LineItems.FirstOrDefault(x => x.Sku.Equals(sku) && x.VariantId.Equals(variantId));
            if (lineItem != null)
            {
                return lineItem.Quantity;
            }
            return 0;
        }

        public decimal GetTotalPriceOfCart(CartView cart)
        {
            var shipping = GetOrCreateDefaultShipping(cart);
            decimal total = 0;
            foreach(var lineItem in shipping.LineItems)
            {
                total += lineItem.Quantity * lineItem.PlacedPrice;
            }
            return total;
        }
        public async Task GetOrCreateDefaultShippingAddress(CartView cart)
        {
            if(!HasShippingAddressSelected(cart))
            {
                var shipping = GetOrCreateDefaultShipping(cart);
                shipping.ShippingAddress = new AddressView();
                var currentContact = await _userContext.GetCurrentContact();
                if(currentContact != null)
                {
                    var selectedContact = currentContact.ShippingAddress != null ? currentContact.ShippingAddress : currentContact.BillingAddress;
                    if(selectedContact != null)
                    {
                        shipping.ShippingAddress = new AddressView()
                        {
                            CityOrTownOrVillage = selectedContact.CityOrTownOrVillage,
                            CountryCode = string.Empty,
                            CountryName = string.Empty,
                            Email = selectedContact.Email,
                            FirstName = selectedContact.FirstName,
                            LastName = selectedContact.LastName,
                            PhoneNumber = selectedContact.PhoneNumber,
                            PostCode = selectedContact.PostCode,
                            Prefecture = selectedContact.Prefecture,
                            StreetAndHouseNumber = selectedContact.StreetAndHouseNumber
                        };
                    }
                }
            }
        }
    }
}
