﻿using Aip.Instance.Backend.Api.Sections.Data;
using Aip.Instance.Backend.Api.Sections.Services;
using Aip.Instance.Backend.Configuration.Swagger;
using Aip.Instance.Backend.Extensions;

using FastEndpoints;


namespace Aip.Instance.Backend.Api.Sections.Endpoints.GetAll;

public class GetAllCourseSectionsEndpoint(CourseSectionsService service)
  : Endpoint<EmptyRequest, GetAllCourseSectionsResponse> {
  public override void Configure() {
    Version(2);
    Get("/course-sections");
    Options(x => x.WithTags(ApiTags.Sections.Tag));
  }

  public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
    var result = await service.GetAll();

    await this.SendResponseAsync(result, ct);
  }
}