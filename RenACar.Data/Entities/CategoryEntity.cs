﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace RentACar.Data.Entities
{
    public class CategoryEntity : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductEntity> Products { get; set; }
    }


    public class CategoryEntityConfiguration : BaseConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.Description)
                .IsRequired(false);

            base.Configure(builder); 
        }
    }

}
