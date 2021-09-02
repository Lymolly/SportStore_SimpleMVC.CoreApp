﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Web.Context;

namespace SportStore.Web.Models
{
    public class EFProductRepository : IProductRepository
    {
        private SportStoreContext context;

        public EFProductRepository(SportStoreContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> GetProducts => context.Products;
    }
}
