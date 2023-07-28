using FastEndpoints;
using Uni.Backend.Modules.Courses.Endpoints;

namespace Uni.Backend.Modules.Courses.Documentation;

public class GetCoursesOwnedByMeDocs: Summary<GetCoursesOwnedByMe>
{
    public GetCoursesOwnedByMeDocs()
    {
        Summary = "Gets all courses, which owned by current user";
        Response(200, "Courses fetched successfully");
    }
}