﻿using Aip.Instance.Backend.Api.Content.Assignment.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Data.Common;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Content.Assignment.Endpoints.GetById;

public class GetAssignmentByIdEndpoint(AssignmentService service) : Endpoint<SearchByIdModel> {
  public override void Configure() {
    Version(2);
    Post("/content/assignments/{id}");
    Options(x => x.WithTags(ApiTags.Assignment.Tag));
    DontThrowIfValidationFails();
  }

  public override async Task HandleAsync(SearchByIdModel req, CancellationToken ct) {
    var result = await service.GetAssignment(req, ct);
    await this.SendResponseAsync(result, ct);
  }
}