﻿using DigitalStore.Base.Schema;
using DigitalStore.Data.Domain;

namespace DigitalStore.Schema
{
    public class ProductCategoryRequest : BaseRequest
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

    }


    public class ProductCategoryResponse : BaseResponse
    {
        public long Id { get; set; }
        public ProductResponse Product { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
