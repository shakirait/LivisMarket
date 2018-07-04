
using LivinMarket.Product.Model.Queries.Business;
using Livis.Market.Utilities.Helper;
using Livis.Market.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Livis.Market.Data;
using System;
using LivinMarket.Order.Model.Commands.Business;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Livis.Market.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly string _URL_CHECKOUT_PAGE = "/MyCart";


        public async Task<ActionResult> Index()
        {
            var currency = await _userContext.GetCurrentCurrency();
            var responseQuery = await _processor.ProcessQueryAsync<ProductShoppingQuery, ProductShoppingResponse>(new ProductShoppingQuery()
            {
                SkuOrSkuVariantOrName = string.Empty,
                CustomerGroup = (await _userContext.GetCustomerGroup()),
                Currency = currency
            });
            var model = new ShoppingViewModel()
            {
                Title = "Shopping",
                Products = responseQuery.ProductsShopping.Select(x => new ProductShoppingViewModel()
                {
                    Image = x.MainImageUrl,
                    Name = x.Name,
                    Sku = x.Sku,
                    Price = $"{ComponentsHelper.Currency[currency]}{x.Price.ToString("G29")} {currency}"
                }).ToArray()
            };

            return View(model);
        }

        [HttpGet("Shopping/{sku}")]
        public async Task<ActionResult> View(string sku)
        {
            ShoppingProductViewModel model = null;
            var currency = await _userContext.GetCurrentCurrency();
            var responseQuery = await _processor.ProcessQueryAsync<ProductShoppingQuery, ProductShoppingResponse>(new ProductShoppingQuery()
            {
                SkuOrSkuVariantOrName = sku,
                CustomerGroup = (await _userContext.GetCustomerGroup()),
                Currency = currency
            });
            var product = responseQuery.ProductsShopping.FirstOrDefault();
            if(product != null)
            {
                model = _mapper.Map<ProductShoppingView, ShoppingProductViewModel>(product);
                model.Price = $"{ComponentsHelper.Currency[currency]}{product.Price.ToString("G29")} {currency}";
                model.Variants = !string.IsNullOrEmpty(product.VariantKeys) ? JsonConvert.DeserializeObject<List<Variant>>(product.VariantKeys) : new List<Variant>();
                model.Options = !string.IsNullOrEmpty(product.Options) ? JsonConvert.DeserializeObject<List<OptionViewModel>>(product.Options) : new List<OptionViewModel>();
                model.Title = product.Name;
                model.Photos = (product.PartnerPhotos != null && product.PartnerPhotos.Count > 0) ? product.PartnerPhotos.Select(x => x.BlobUrl).ToArray() : new string[] { }; 
            }
            if(model == null)
            {
                model = new ShoppingProductViewModel()
                {
                    Title = sku
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeQuantity(Guid? customerId, string sku, string variantId, decimal quantity)
        {
            try
            {
                if (customerId == null)
                {
                    customerId = await GetCustomerId();
                }
                var cartView = await _cartService.LoadCart(customerId.Value);
                if (cartView != null)
                {
                    await _cartService.ChangeQuantity(cartView, sku, variantId, quantity);
                    await CreateOrUpdateCartView(cartView);
                    if (_cartService.HasItems(cartView))
                    {
                        var subTotal = _cartService.GetSubTotal(cartView, sku, variantId);
                        var subTotalDisplay = String.Format("{0}{1} {2}", ComponentsHelper.Currency[cartView.Currency], subTotal.ToString("G29"), cartView.Currency);
                        var totalCart = _cartService.GetTotalPriceOfCart(cartView);
                        var totalCartDisplay = String.Format("{0}{1} {2}", ComponentsHelper.Currency[cartView.Currency], totalCart.ToString("G29"), cartView.Currency);

                        return Json(new { @success = true, @message = subTotalDisplay, @quantity = quantity, @total = totalCartDisplay });
                    }
                }
                return SuccessActionJson(string.Empty);
            }
            catch (Exception ex)
            {
                return ReturnFailJson(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLineItem(Guid? customerId, string sku, string variantId)
        {
            try
            {
                if (customerId == null)
                {
                    customerId = await GetCustomerId();
                }
                var cartView = await _cartService.LoadCart(customerId.Value);
                if (cartView != null)
                {
                    _cartService.RemoveToCart(cartView, sku, variantId);
                    await CreateOrUpdateCartView(cartView);
                    if (_cartService.HasItems(cartView))
                    {
                        var totalCart = _cartService.GetTotalPriceOfCart(cartView);
                        var totalCartDisplay = String.Format("{0}{1} {2}", ComponentsHelper.Currency[cartView.Currency], totalCart.ToString("G29"), cartView.Currency);
                        return SuccessActionJson(totalCartDisplay);
                    }
                }
                return SuccessActionJson("reload");
            }
            catch(Exception ex)
            {
                return ReturnFailJson(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(Guid? customerId, string sku, string variantId, int quantity)
        {
            try
            {
                if (customerId == null)
                {
                    customerId = await GetOrCreateDefaultCustomerId();
                }
                var cartView = await _cartService.LoadOrCreateCart(customerId.Value);
                if (string.IsNullOrEmpty(variantId))
                {
                    variantId = sku;
                }
                await _cartService.ChangeQuantity(cartView, sku, variantId, quantity);
                await CreateOrUpdateCartView(cartView);
                var customerGroup = await _userContext.GetCustomerGroup();
                if (customerGroup == CustomerGroup.NonMember)
                    return SuccessActionJson(customerId.Value.ToString());
                else
                    return SuccessActionJson(string.Empty);
            }
            catch(Exception ex)
            {
                return ReturnFailJson(ex.Message);
            }
        }

        public ActionResult Cart()
        {
            var cartViewModel = GenerateCartViewModel("My Cart");
            cartViewModel.CssCartClass = "active";

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Cart(Guid? customerId)
        {
            try
            {
                CartView model = null;
                if (customerId == null)
                {
                    var currentCustomerId = await GetCustomerId();
                    model = await _cartService.LoadCart(currentCustomerId);
                    if (model == null || !_cartService.HasItems(model))
                    {
                        return PartialView(ViewNameConstant.PREVIEW_CART, null);
                    }
                    _cartService.UpdateTotalPrice(model);
                    return PartialView(ViewNameConstant.PREVIEW_CART, model);
                }
                model = await _cartService.LoadCart(customerId.Value);
                if (model != null && _cartService.HasItems(model))
                {
                    _cartService.UpdateTotalPrice(model);
                    return PartialView(ViewNameConstant.PREVIEW_CART, model);
                }
                return PartialView(ViewNameConstant.PREVIEW_CART, null);
            }catch(Exception ex)
            {
                return ReturnFailJson(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateShippingAddress(ShippingViewModel address)
        {
            try
            {
                CartView model = null;
                var customerId = address.CustomerId;
                if (customerId == null)
                {
                    var currentCustomerId = await GetCustomerId();
                    model = await _cartService.LoadCart(currentCustomerId);
                    if (model != null)
                    {
                        _cartService.UpdateShippingCart(model, address);
                        await CreateOrUpdateCartView(model);
                    }
                }
                else
                {
                    model = await _cartService.LoadCart(customerId.Value);
                    if (model != null)
                    {
                        _cartService.UpdateShippingCart(model, address);
                        await CreateOrUpdateCartView(model);
                    }
                }
                
                return RedirectToAction("Payment", "Shopping");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Customer", "Shopping");
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> ViewShippingAddress(Guid? customerId)
        {
            try
            {
                CartView model = null;
                if (customerId == null)
                {
                    var currentCustomerId = await GetCustomerId();
                    model = await _cartService.LoadCart(currentCustomerId);
                    if (model == null || !_cartService.HasItems(model))
                    {
                        return PartialView(ViewNameConstant.SHIPPING_CART, null);
                    }
                    await _cartService.GetOrCreateDefaultShippingAddress(model);
                    PopulateCountryCodes(string.Empty);
                    var shipping = _cartService.GetOrCreateDefaultShipping(model);
                    var addressView = _mapper.Map<AddressView, ShippingViewModel>(shipping.ShippingAddress);
                    addressView.CustomerId = customerId;
                    return PartialView(ViewNameConstant.SHIPPING_CART, addressView);
                }
                model = await _cartService.LoadCart(customerId.Value);
                if (model != null && _cartService.HasItems(model))
                {
                    await _cartService.GetOrCreateDefaultShippingAddress(model);
                    PopulateCountryCodes(string.Empty);
                    var shipping = _cartService.GetOrCreateDefaultShipping(model);
                    var addressView = _mapper.Map<AddressView, ShippingViewModel>(shipping.ShippingAddress);
                    addressView.CustomerId = customerId;
                    return PartialView(ViewNameConstant.SHIPPING_CART, addressView);
                }
                return PartialView(ViewNameConstant.SHIPPING_CART, null);
            }
            catch (Exception ex)
            {
                return ReturnFailJson(ex.Message);
            }
        }

        public ActionResult Customer()
        {
            var cartViewModel = GenerateCartViewModel("My Info");
            cartViewModel.CssCustomerInfoClass = "active";

            return View(cartViewModel);
        }

        public ActionResult Payment()
        {
            var cartViewModel = GenerateCartViewModel("My Payment");
            cartViewModel.CssPaymentMethodClass = "active";

            return View(cartViewModel);
        }

        public ActionResult Confirmation()
        {
            var cartViewModel = GenerateCartViewModel("My Confirmation");
            cartViewModel.CssConfirmationClass = "active";

            return View(cartViewModel);
        }

        public ActionResult Complete()
        {
            var cartViewModel = GenerateCartViewModel("Order Completion");
            cartViewModel.CssOrderCompleteClass = "active";

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> MergeCart(Guid? customerId)
        {
            var viewModel = CreateEmptyMiniCart();
            var currentContact = await _userContext.GetCurrentContact();
            if (customerId == null)
            {
                if(currentContact == null)
                    return PartialView(ViewNameConstant.MINI_CART, viewModel);
                else
                {
                    var cartViewOfCurrentContact = await _cartService.LoadOrCreateCart(currentContact.ContactId);
                    viewModel.ItemCount = _cartService.CountLineItems(cartViewOfCurrentContact);
                    return PartialView(ViewNameConstant.MINI_CART, viewModel);
                }
            }
            var cartViewFrom = await _cartService.LoadOrCreateCart(customerId.Value);
            if (currentContact == null)
            {
                viewModel.ItemCount = _cartService.CountLineItems(cartViewFrom);
                return PartialView(ViewNameConstant.MINI_CART, viewModel);
            }
            else
            {
                if (currentContact.ContactId == customerId.Value)
                {
                    viewModel.ItemCount = _cartService.CountLineItems(cartViewFrom);
                    return PartialView(ViewNameConstant.MINI_CART, viewModel);
                }
                else
                {
                    var cartViewTo = await _cartService.LoadOrCreateCart(currentContact.ContactId);
                    _cartService.MergeCart(cartViewFrom, cartViewTo);
                    await CreateOrUpdateCartView(cartViewTo);
                    await DeleteCartView(cartViewFrom);
                    viewModel.ItemCount = _cartService.CountLineItems(cartViewTo);
                    return PartialView(ViewNameConstant.MINI_CART, viewModel);
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> MiniCart(Guid? customerId)
        {
            var viewModel = CreateEmptyMiniCart();
            if (customerId == null)
            {
                customerId = await GetCustomerId();
                if(customerId.Value  == Guid.Empty)
                {
                    return PartialView(ViewNameConstant.MINI_CART, viewModel);
                }
            }
            var cartView = await _cartService.LoadOrCreateCart(customerId.Value);
            viewModel.ItemCount = _cartService.CountLineItems(cartView);
            return PartialView(ViewNameConstant.MINI_CART, viewModel);
        }

        private void PopulateCountryCodes(string errorMessage)
        {
            ViewBag.CountryList = new SelectList(ComponentsHelper.GetCountryList(), "Key", "Value");
            ViewBag.ErrorMessage = errorMessage;
        }

        private MiniCartViewModel CreateEmptyMiniCart() {
            return new MiniCartViewModel()
            {
                ItemCount = 0,
                UrlCheckout = _URL_CHECKOUT_PAGE
            };
        }

        private async Task DeleteCartView(CartView cartView)
        {
            await _processor.ProcessCommandAsync<DeleteCartCommand>(new DeleteCartCommand()
            {
                CartView = cartView
            });
        }

        private async Task CreateOrUpdateCartView(CartView cartView)
        {
            if (cartView.Id == -1)
            {
                await _processor.ProcessCommandAsync<AddCartCommand>(new AddCartCommand()
                {
                    CartView = cartView
                });
            }
            else
            {
                await _processor.ProcessCommandAsync<UpdateCartCommand>(new UpdateCartCommand()
                {
                    CartView = cartView
                });
            }
        }

        private CartViewModel GenerateCartViewModel(string titlePage)
        {
            return new CartViewModel()
            {
                Title = titlePage,
                CssCartClass = string.Empty,
                CssConfirmationClass = string.Empty,
                CssCustomerInfoClass = string.Empty,
                CssOrderCompleteClass = string.Empty,
                CssPaymentMethodClass = string.Empty,
                LinkCart = "/MyCart",
                LinkCustomerInfo = "/MyInfo",
                LinkConfirmation = "/MyConfirmation",
                LinkPaymentMethod = "/MyPayment",
                LinkOrderComplete = "/OrderCompletion"
            };
        }
    }
}