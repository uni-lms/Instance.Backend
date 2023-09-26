using FastEndpoints;

using Riok.Mapperly.Abstractions;


namespace Uni.Backend.Modules.Assignments.Contracts;

[Mapper]
public partial class AssignmentsMapper : ResponseMapper<AssignmentDto, Assignment> {
  public partial AssignmentDto FromEntity(Assignment e);
}