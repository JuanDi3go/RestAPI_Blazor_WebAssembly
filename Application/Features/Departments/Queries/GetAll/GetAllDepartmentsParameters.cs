using Application.Parameters;

namespace Application.Features.Departments.Queries.GetAll
{
    public class GetAllDepartmentsParameters: RequestParameters
    {
        public string? Name { get; set; }
    }
}
