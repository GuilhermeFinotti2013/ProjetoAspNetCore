﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoAspNetCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoAspNetCore.Data.Mapping
{
    class CidMap : IEntityTypeConfiguration<Cid>
    {

        public void Configure(EntityTypeBuilder<Cid> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(p => p.Codigo).IsRequired().HasColumnType("varchar(6)")
                .HasColumnName("Codigo");

            builder.Property(e => e.CidInternalId).HasColumnName("CidInternalId");

            builder.Property(c => c.Diagnostico)
                .HasColumnName("Diagnostico").HasColumnType("nvarchar(4000)");


            builder.ToTable("Cid");
        }
    }
}
