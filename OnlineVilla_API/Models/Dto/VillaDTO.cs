﻿using System.ComponentModel.DataAnnotations;

namespace OnlineVilla_API.Models.Dto;

public class VillaDTO
{
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}
