using CareerCase.Domain.Requests.Company;
using CareerCase.Domain.Results;
using System.Threading.Tasks;

namespace CareerCase.Core.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ServiceResult> CreateCompanyAsync(CreateCompanyRequest createCompanyRequest);
    }
}