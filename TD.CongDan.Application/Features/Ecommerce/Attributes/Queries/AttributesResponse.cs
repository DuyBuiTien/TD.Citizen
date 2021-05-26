﻿using TD.CongDan.Domain.Entities;

namespace TD.CongDan.Application.Features.Attributes.Queries
{
    public class AttributesResponse
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public int? Position { get; set; }
        public bool? IncludeInMenu { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string[] Tags { get; set; }

    }
}