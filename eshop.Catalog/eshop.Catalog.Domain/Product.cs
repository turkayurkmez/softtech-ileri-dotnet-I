﻿namespace eshop.Catalog.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int? Stock { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
