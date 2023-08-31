﻿using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService ?? throw new ArgumentNullException(nameof(productService));
		}
        [Authorize]
		public async Task<ActionResult> ProductIndex()
		{
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAllProducts(accessToken);
			return View(products);
		}

        public async Task<ActionResult> ProductCreate()
        {
            return View();
        }
		[HttpPost]
        [Authorize]
        public async Task<ActionResult> ProductCreate(ProductModel model)
        {
            if(ModelState.IsValid)
			{
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProduct(model, accessToken);
				if(response != null)
				{
					return RedirectToAction(nameof(ProductIndex));
				}
			}

            return View(model);
        }

        public async Task<ActionResult> ProductUpdate(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var model = await _productService.FindProductById(id, accessToken);
            if(model != null)
            {
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProduct(model, accessToken);
                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }
        [Authorize]
        public async Task<ActionResult> ProductDelete(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var model = await _productService.FindProductById(id, accessToken);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> ProductDelete(ProductModel model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.DeleteProductById(model.Id, accessToken);
                if (response)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            return View(model);
        }
    }
}
