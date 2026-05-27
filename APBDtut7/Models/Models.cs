using System;
using System.Collections.Generic;

namespace ApbdTutorial7.Models;

public class ComponentManufacturer
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime FoundationDate { get; set; }
    
    public ICollection<Component> Components { get; set; } = new List<Component>();
}

public class ComponentType
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    public ICollection<Component> Components { get; set; } = new List<Component>();
}

public class Component
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ManufacturerId { get; set; }
    public int TypeId { get; set; }

    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public ComponentType Type { get; set; } = null!;
    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}

public class PC
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public ICollection<PCComponent> PCComponents { get; set; } = new List<PCComponent>();
}

public class PCComponent
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; } = null!;
    public int Amount { get; set; }

    public PC PC { get; set; } = null!;
    public Component Component { get; set; } = null!;
}