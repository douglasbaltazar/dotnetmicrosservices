using AutoMapper;
using GeekShopping.ProductAPI.Data.DTO;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GeekShopping.ProductAPI.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly MySQLContext _context;
		private IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
        }

		public async Task<IEnumerable<ProductDTO>> FindAll()
		{
			List<Product> products = await _context.Products.ToListAsync();
			return _mapper.Map<List<ProductDTO>>(products);
		}

		public async Task<ProductDTO> FindById(long id)
		{
			Product product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
			return _mapper.Map<ProductDTO>(product);
		}
		public Task<ProductDTO> Create(ProductDTO dto)
		{
			throw new NotImplementedException();
		}
		public Task<ProductDTO> Update(ProductDTO dto)
		{
			throw new NotImplementedException();
		}
		public Task<bool> DeleteById(long id)
		{
			throw new NotImplementedException();
		}




	}
}
