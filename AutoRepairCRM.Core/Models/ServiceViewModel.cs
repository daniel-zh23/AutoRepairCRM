﻿namespace AutoRepairCRM.Core.Models;

public class ServiceViewModel
{
    public bool ServiceState { get; set; }

    public string ServiceType { get; set; } = null!;

    public string StartDate { get; set; } = null!;

    public string? EndDate { get; set; }

    public decimal? Price { get; set; }
}