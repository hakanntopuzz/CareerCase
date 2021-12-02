using CareerCase.Domain.Requests.UnfavorableWord;
using CareerCase.Domain.Results;
using System.Threading.Tasks;

namespace CareerCase.Core.Services.Interfaces
{
    public interface IUnfavorableWordService
    {
        Task<ServiceResult> CreateWordAsync(CreateWordRequest createWordRequest);
        Task<bool> CheckStringForUnfavorableWordAsync(string text);
    }
}