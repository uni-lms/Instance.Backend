using FastEndpoints;
using Uni.Backend.Modules.Courses.Endpoints;

namespace Uni.Backend.Modules.Courses.Documentation;

public class GetAllCoursesDocs: Summary<GetAllCourses>
{
    public GetAllCoursesDocs()
    {
        Summary = "Gets all available courses";
        Response(200, "Courses fetched successfully");
    }
}