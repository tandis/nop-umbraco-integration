﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Mvc;
using Nop.Integration.Umbraco.Core.Core;
using Nop.Integration.Umbraco.Nop;
using Umbraco.Web.WebApi;
using Nop.Integration.Umbraco.Products;

namespace Nop.Integration.Umbraco.Core.Controllers
{
    public class NopProductsApiController : UmbracoApiController
    {
        private readonly NopApiService _nopService;
        

        public NopProductsApiController()
        {
            _nopService = new NopApiService();
        }

        [HttpGet]
        public List<Product> GetProducts(FormDataCollection queryStrings)
        {
            var storeId = GlobalSettings.UmbracoSettings.NopStoreId;
            var isGetProductLimitToStore = GlobalSettings.UmbracoSettings.GetProductLimitToStore;
            var products = storeId != 0 && isGetProductLimitToStore ?_nopService.GetAllProducts().Where(product => product.StoreIds.Contains(storeId)).ToList() : _nopService.GetAllProducts();

            return products;
        }

        [HttpPost]
        public string Create([System.Web.Http.FromBody]string name)
        {
            var storeId = GlobalSettings.UmbracoSettings.NopStoreId;
            var isCreateProductLimitToStore = GlobalSettings.UmbracoSettings.CreateProductLimitToStore;

            var product = new PostProductObject()
            {
                Name = name
            };

            if (storeId != 0 && isCreateProductLimitToStore)
                product.StoreIds = new[] {storeId};

            var productId = _nopService.CreateProduct(product);

            return productId;
        }

        [HttpPost]
        public void Update(PostProductObject product)
        {
            _nopService.UpdateProduct(product);
        }
    }
}
