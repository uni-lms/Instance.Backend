﻿using Uni.Backend.Data;
using Uni.Backend.Modules.CourseBlocks.Contracts;
using Uni.Backend.Modules.Groups.Contracts;
using Uni.Backend.Modules.Users.Contracts;

namespace Uni.Backend.Modules.Courses.Contracts;

public class Course: BaseModel
{
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public required List<Group> AssignedGroups { get; set; }
    public int Semester { get; set; }
    public required List<User> Owners { get; set; }
    public required List<CourseBlock> Blocks { get; set; }
}