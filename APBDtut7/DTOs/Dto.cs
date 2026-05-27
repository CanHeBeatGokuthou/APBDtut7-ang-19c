using System;
using System.Collections.Generic;

namespace ApbdTutorial7.DTOs;

public class PcDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class CreatePcDto
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class PcWithComponentsDto : PcDto
{
    public List<PcComponentDetailDto> Components { get; set; } = new();
}

public class PcComponentDetailDto
{
    public int Amount { get; set; }
    public ComponentDetailDto Component { get; set; } = null!;
}

public class ComponentDetailDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ManufacturerDto Manufacturer { get; set; } = null!;
    public TypeDto Type { get; set; } = null!;
}

public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string FoundationDate { get; set; } = null!;
}

public class TypeDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
}