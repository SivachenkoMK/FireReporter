﻿namespace FireReporter.DTOs;

public class LocationRequest
{
    public RelativeLocation Location { get; set; } = default!;
    
    public Guid FireId { get; set; }
}