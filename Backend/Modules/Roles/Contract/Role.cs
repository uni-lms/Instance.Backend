﻿using Backend.Data;

namespace Backend.Modules.Roles.Contract;

public class Role: BaseModel
{
    public required string Name { get; set; }
}