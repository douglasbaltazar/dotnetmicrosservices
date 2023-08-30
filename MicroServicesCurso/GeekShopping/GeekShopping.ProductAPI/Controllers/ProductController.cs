﻿using GeekShopping.ProductAPI.Data.DTO;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
				_repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDTO>> FindById(long id)
		{
			var product = await _repository.FindById(id);
			if(product.Id <= 0)
			{
				return NotFound();
			}
			return Ok(product);
		}
		[HttpGet("/")]
		public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAll()
		{
			var products = await _repository.FindAll();
			return Ok(products);
		}
		[HttpPost]
		public async Task<ActionResult<ProductDTO>> Create(ProductDTO dto)
		{
			if (dto == null)
			{
				return BadRequest();
			}
			var product = await _repository.Create(dto);
			return Ok(product);
		}

		[HttpPut]
		public async Task<ActionResult<ProductDTO>> Update(ProductDTO dto)
		{
			if (dto == null)
			{
				return BadRequest();
			}
			var product = await _repository.Update(dto);
			return Ok(product);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			var status = await _repository.DeleteById(id);
			if (!status)
			{
				return BadRequest();
			}
			return Ok(status);
		}

	}
}
