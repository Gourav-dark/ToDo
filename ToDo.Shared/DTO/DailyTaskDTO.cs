﻿using System.ComponentModel.DataAnnotations;

namespace ToDo.Shared.DTO;
public class DailyTaskDTO
{
   
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
    //public int Sequence { get; set; }

    public bool IsCompleted { get; set; } = false;

}
