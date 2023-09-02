using AutoMapper;
using GeekShopping.CartAPI.Model;

namespace GeekShopping.CartAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				// config.CreateMap<ProductDTO, Product>();
				// config.CreateMap<Product, ProductDTO>();
			});
			

			return mappingConfig;
		}
	}
}
