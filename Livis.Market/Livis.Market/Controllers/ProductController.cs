using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livis.Market.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using LivinMarket.Product.Model.Queries.Business;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Livis.Market.Data;
using LivinMarket.Product.Model.Commands.Business;
using Livis.Common.Tools;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Livis.Market.Utilities.Helper;

namespace Livis.Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : BaseController
    {
        private readonly IHostingEnvironment _appEnvironment;

        public ProductController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public async Task<ActionResult> View(string sku)
        {
            await PopulateCategories();
            var responseQuery = await _processor.ProcessQueryAsync<ProductQuery, ProductResponse>(new ProductQuery()
            {
                NameOrId = sku
            });
            var model = new ProductViewModel();
            if (responseQuery != null)
            {
                var updatedProduct = responseQuery.Products.First();
                model = new ProductViewModel()
                {
                    Sku = updatedProduct.Sku,
                    Name = updatedProduct.Name,
                    CategoryId = updatedProduct.CategoryId.Value,
                    Cost = updatedProduct.Cost.ToString("G29"),
                    Description = updatedProduct.Description,
                    Height = updatedProduct.Height.Value.ToString("G29"),
                    Length = updatedProduct.Length.Value.ToString("G29"),
                    OriginalLinks = updatedProduct.OriginalLinks,
                    ProductId = updatedProduct.ProductId,
                    Resources = updatedProduct.Resources,
                    Videos = updatedProduct.Videos,
                    Weight = updatedProduct.Weight.Value.ToString("G29"),
                    Width = updatedProduct.Width.Value.ToString("G29"),
                    SuggestedPrice = updatedProduct.SuggestedPrice.ToString("G29"),
                    Photos = updatedProduct.Photos?.Select(x => new ProductPhotoModel()
                    {
                        BlobUrl = x.BlobUrl,
                        PhotoId = x.PhotoId
                    }).ToArray(),
                    PartnerPhotos = updatedProduct.PartnerPhotos?.Select(x => new ProductPhotoForPartnerModel()
                    {
                        BlobUrl = x.BlobUrl,
                        PhotoId = x.PhotoId
                    }).ToArray(),
                    Options = !string.IsNullOrEmpty(updatedProduct.VariantOptions) ? JsonConvert.DeserializeObject<List<OptionViewModel>>(updatedProduct.VariantOptions) :
                        new List<OptionViewModel>(),
                    Variants = !string.IsNullOrEmpty(updatedProduct.VariantKeys) ? JsonConvert.DeserializeObject<List<Variant>>(updatedProduct.VariantKeys) : new List<Variant>()
                };
            }
            return View(model);
        }

        public async Task<ActionResult> Index()
        {
            var responseQuery = await _processor.ProcessQueryAsync<ProductQuery, ProductResponse>(new ProductQuery()
            {
                NameOrId = string.Empty
            });
            var model = new ProductView[] { };
            if (responseQuery.Products.Any())
            {
                model = responseQuery.Products.Select(p => new ProductView()
                {
                    Cost = p.Cost.ToString("G29"),
                    SuggestedPrice = p.SuggestedPrice.ToString("G29"),
                    Height = p.Height?.ToString("G29"),
                    Length = p.Length?.ToString("G29"),
                    Name = p.Name,
                    Sku = p.Sku,
                    Weight = p.Weight?.ToString("G29"),
                    Width = p.Width?.ToString("G29"),
                    ProductId = p.ProductId,
                    ImageUrl = p.Photos?.Count > 0 ? p.Photos.First().BlobUrl : "/images/upload.png",
                    EditUrl = "/Product/Update?sku=" + p.Sku,
                    ViewUrl = "/Product/View?sku=" + p.Sku,
                    DeleteUrl = "/Product/Delete?sku=" + p.Sku
                }).ToArray();
            }
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategories();
            return View();
        }

        public async Task<IActionResult> Update(string sku)
        {
            await PopulateCategories();
            var responseQuery = await _processor.ProcessQueryAsync<ProductQuery, ProductResponse>(new ProductQuery()
            {
                NameOrId = sku
            });
            var model = new ProductViewModel();
            if (responseQuery != null)
            {
                var updatedProduct = responseQuery.Products.First();
                model = new ProductViewModel()
                {
                    Sku = updatedProduct.Sku,
                    Name = updatedProduct.Name,
                    CategoryId = updatedProduct.CategoryId.Value,
                    Cost = updatedProduct.Cost.ToString("G29"),
                    Description = updatedProduct.Description,
                    Height = updatedProduct.Height.Value.ToString("G29"),
                    Length = updatedProduct.Length.Value.ToString("G29"),
                    OriginalLinks = updatedProduct.OriginalLinks,
                    ProductId = updatedProduct.ProductId,
                    Resources = updatedProduct.Resources,
                    Videos = updatedProduct.Videos,
                    Weight = updatedProduct.Weight.Value.ToString("G29"),
                    Width = updatedProduct.Width.Value.ToString("G29"),
                    SuggestedPrice = updatedProduct.SuggestedPrice.ToString("G29"),
                    Photos = updatedProduct.Photos?.Select(x => new ProductPhotoModel() {
                        BlobUrl = x.BlobUrl,
                        PhotoId = x.PhotoId
                    }).ToArray(),
                    PartnerPhotos = updatedProduct.PartnerPhotos?.Select(x => new ProductPhotoForPartnerModel() {
                        BlobUrl = x.BlobUrl,
                        PhotoId = x.PhotoId
                    }).ToArray(),
                    Options = !string.IsNullOrEmpty(updatedProduct.VariantOptions) ? JsonConvert.DeserializeObject<List<OptionViewModel>>(updatedProduct.VariantOptions) :
                        new List<OptionViewModel>(),
                    Variants = !string.IsNullOrEmpty(updatedProduct.VariantKeys) ? JsonConvert.DeserializeObject<List<Variant>>(updatedProduct.VariantKeys) : new List<Variant>(),
                    Prices = !string.IsNullOrEmpty(updatedProduct.Prices) ? JsonConvert.DeserializeObject<List<ProductPrice>>(updatedProduct.Prices).Select(x => new ProductPriceView() {
                        CustomerGroup = x.CustomerGroup,
                        Price = x.Price.ToString("G29"),
                        CustomerGroupName = x.CustomerGroup.GetDisplayName()
                    }).ToList() : new List<ProductPriceView>()
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string sku)
        {
            try
            {
                await _processor.ProcessCommandAsync<DeleteProductCommand>(new DeleteProductCommand()
                {
                    Sku = sku
                });
                return Json(new { @success = true, @message = string.Empty });
            }
            catch (Exception ex)
            {
                return Json(new { @success = false, @message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userContext.GetCurrentUser();
                var updateProductCommand = new UpdateProductCommand()
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Cost = string.IsNullOrEmpty(model.Cost) ? 0 : Decimal.Parse(model.Cost),
                    Description = model.Description,
                    Height = string.IsNullOrEmpty(model.Height) ? 0 : Decimal.Parse(model.Height),
                    Length = string.IsNullOrEmpty(model.Length) ? 0 : Decimal.Parse(model.Length),
                    OriginalLinks = model.OriginalLinks,
                    Width = string.IsNullOrEmpty(model.Width) ? 0 : Decimal.Parse(model.Width),
                    Weight = string.IsNullOrEmpty(model.Weight) ? 0 : Decimal.Parse(model.Weight),
                    Videos = model.Videos,
                    SuggestedPrice = string.IsNullOrEmpty(model.SuggestedPrice) ? 0 : Decimal.Parse(model.SuggestedPrice),
                    Resources = model.Resources,
                    Photos = model.Photos?.Select(x => new ProductPhoto()
                    {
                        BlobUrl = x.BlobUrl
                    }).ToArray(),
                    PartnerPhotos = model.PartnerPhotos?.Select(x => new ProductPhotoForPartner()
                    {
                        BlobUrl = x.BlobUrl
                    }).ToArray(),
                    ProductId = model.ProductId,
                    Sku = model.Sku,
                    VariantOptions = (model.Options != null && model.Options.Count > 0) ?
                        JsonConvert.SerializeObject(model.Options) : string.Empty,
                    VariantKeys = (model.Variants != null && model.Variants.Count > 0) ?
                        JsonConvert.SerializeObject(model.Variants) : string.Empty,
                    Prices = (model.Prices != null && model.Prices.Count > 0) ?
                        JsonConvert.SerializeObject(model.Prices.Select(x => new ProductPrice() {
                            CustomerGroup = x.CustomerGroup,
                            Price = string.IsNullOrEmpty(x.Price) ? 0 : Decimal.Parse(x.Price)
                        })) : string.Empty,
                    UserConfirmation = currentUser
                };
                await _processor.ProcessCommandAsync<UpdateProductCommand>(updateProductCommand);
                return RedirectToAction(nameof(Update), new { sku = updateProductCommand.Sku });
            }

            await PopulateCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userContext.GetCurrentUser();
                var addProductCommand = new AddProductCommand() {
                    NewProduct = new Product()
                    {
                        Name = model.Name,
                        CategoryId = model.CategoryId,
                        Cost = string.IsNullOrEmpty(model.Cost) ? 0 : Decimal.Parse(model.Cost),
                        Description = model.Description,
                        Height = string.IsNullOrEmpty(model.Height) ? 0 : Decimal.Parse(model.Height),
                        Length = string.IsNullOrEmpty(model.Length) ? 0 : Decimal.Parse(model.Length),
                        OriginalLinks = model.OriginalLinks,
                        Width = string.IsNullOrEmpty(model.Width) ? 0 : Decimal.Parse(model.Width),
                        Weight = string.IsNullOrEmpty(model.Weight) ? 0 : Decimal.Parse(model.Weight),
                        Videos = model.Videos,
                        SuggestedPrice = string.IsNullOrEmpty(model.SuggestedPrice) ? 0 : Decimal.Parse(model.SuggestedPrice),
                        Resources = model.Resources,
                        Photos = model.Photos?.Select(x => new ProductPhoto()
                        {
                            BlobUrl = x.BlobUrl
                        }).ToArray(),
                        PartnerPhotos = model.PartnerPhotos?.Select(x => new ProductPhotoForPartner()
                        {
                            BlobUrl = x.BlobUrl
                        }).ToArray(),
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatedBy = currentUser,
                        UserId = currentUser.Id,
                        ModifiedBy = currentUser.Id
                    }
                };
                await _processor.ProcessCommandAsync<AddProductCommand>(addProductCommand);
                return RedirectToAction(nameof(Update), new { sku = addProductCommand.NewProduct.Sku });
            }

            await PopulateCategories();
            return View();
        }

        [HttpPost]
        public IActionResult GeneratingVariants([FromBody]List<OptionViewModel> options)
        {
            var optionValues = new List<string[]>();
            foreach(var opt in options)
            {
                optionValues.Add(opt.Values.Split(','));
            }
            var variants = VariantTool.GenerateVariantOptions(optionValues);
            return Json(new { variants = variants });
        }

        [HttpPost]
        public IActionResult UploadFiles()
        {
            List<string> items = new List<string>();
            var files = Request.Form.Files;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);
                    var path = Path.Combine(_appEnvironment.WebRootPath, "UploadFiles\\img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                        items.Add(Path.Combine("\\UploadFiles\\img", fileName));
                    }
                }
            }

            return Json(new { items = items });
        }
        private async Task PopulateCategories()
        {
            var query = await _processor.ProcessQueryAsync<CategoryQuery, ListCategoryResponse>(new  CategoryQuery()
            {
                NameOrId = string.Empty
            });
            ViewBag.Categories = query.Categories.Select(x => new CategoryModel() {
                CategoryId = x.CategoryId,
                Name = x.CategoryName
            }).ToArray();
        }
    }
}