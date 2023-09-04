﻿using AutoMapper;
using GeekShopping.CouponAPI.Data.DTO;
using GeekShopping.CouponAPI.Model;

namespace GeekShopping.CouponAPI.Config
{
	public class MappingConfig
	{
		public static MapperConfiguration RegisterMaps()
		{
			var mappingConfig = new MapperConfiguration(config =>
			{
				config.CreateMap<CouponDTO, Coupon>().ReverseMap();
                // config.CreateMap<Product, ProductDTO>();
            });
			

			return mappingConfig;
		}
	}
}
