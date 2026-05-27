using System;
using ApbdTutorial7.Models;
using Microsoft.EntityFrameworkCore;

namespace ApbdTutorial7.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PCComponent>()
            .HasKey(pc => new { pc.PCId, pc.ComponentCode });

        modelBuilder.Entity<Component>()
            .HasKey(c => c.Code);
        modelBuilder.Entity<Component>()
            .Property(c => c.Code).HasColumnType("char(10)").IsRequired();
        modelBuilder.Entity<Component>()
            .Property(c => c.Name).HasMaxLength(300).IsRequired();

        modelBuilder.Entity<PC>().Property(p => p.Name).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<ComponentManufacturer>().Property(cm => cm.Abbreviation).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<ComponentManufacturer>().Property(cm => cm.FullName).HasMaxLength(300).IsRequired();
        modelBuilder.Entity<ComponentType>().Property(ct => ct.Abbreviation).HasMaxLength(30).IsRequired();
        modelBuilder.Entity<ComponentType>().Property(ct => ct.Name).HasMaxLength(150).IsRequired();

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
            new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ManufacturerId = 1, TypeId = 1 },
            new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ManufacturerId = 2, TypeId = 2 },
            new Component { Code = "RAM0000001", Name = "Corsair Vengeance 16GB", Description = "DDR5 RAM", ManufacturerId = 3, TypeId = 3 }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = DateTime.Parse("2026-05-08T09:00:00"), Stock = 5 },
            new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = DateTime.Parse("2026-04-15T13:30:00"), Stock = 12 },
            new PC { Id = 3, Name = "Home Media Center", Weight = 6.0, Warranty = 24, CreatedAt = DateTime.Parse("2026-05-27T12:00:00"), Stock = 8 }        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            new PCComponent { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 }
        );
    }
}