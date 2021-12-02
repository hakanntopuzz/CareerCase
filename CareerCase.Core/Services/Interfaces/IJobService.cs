using CareerCase.Domain.Requests.Job;
using CareerCase.Domain.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareerCase.Core.Services.Interfaces
{
    public interface IJobService
    {
        Task<ServiceResult> CreateJobAsync(CreateJobRequest createJobRequest);
        Task<ICollection<JobResult>> SearchJob(string endDate);
    }
}