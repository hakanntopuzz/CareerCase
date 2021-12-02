using CareerCase.Core.Services.Interfaces;
using CareerCase.Domain;
using CareerCase.Domain.Entities;
using CareerCase.Domain.Requests.Job;
using CareerCase.Domain.Results;
using CareerCase.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCase.Core.Services
{
    public class JobService : IJobService
    {
        #region Ctor
        private readonly ILogger<JobService> _logger;
        private readonly IRepository<Job> _jobRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly IUnfavorableWordService _unfavorableWordService;

        public JobService(ILogger<JobService> logger, IRepository<Job> jobRepository,
            IRepository<Company> companyRepository, IUnfavorableWordService unfavorableWordService)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _unfavorableWordService = unfavorableWordService;
        }

        #endregion

        public async Task<ServiceResult> CreateJobAsync(CreateJobRequest createJobRequest)
        {
            try
            {
                var company = await _companyRepository.FirstOrDefaultAsync(q => q.Id == createJobRequest.CompanyId);

                if(company == null)
                {
                    return ServiceResult.Error("Firma bulunamadı.");
                }

                if(company.JobLimit == 0)
                {
                    return ServiceResult.Error("Firmanın ilan hakkı yoktur.");
                }

                var job = new Job
                {
                    CompanyId = createJobRequest.CompanyId,
                    Position = createJobRequest.Position,
                    Benefits = createJobRequest.Benefits,
                    Description = createJobRequest.Description,
                    EndDate = DateTime.Now.AddDays(Constants.JobDayLimit),
                    Pay = createJobRequest.Pay,
                    WorkType = createJobRequest.WorkType,
                    Quality = await CalculateJobQuality(createJobRequest)
                };

                await _jobRepository.CreateAsync(job);
                company.JobLimit = company.JobLimit - 1;
                await _companyRepository.UpdateAsync(company);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.Error("Opss! Bir hata oluştu.");
            }
        }

        public async Task<ICollection<JobResult>> SearchJob(string endDate)
        {
            var date = DateTime.Parse(endDate);
            var jobs = await _jobRepository.ToListAsync(q => q.EndDate >= DateTime.Now && q.EndDate <= date);

            var jobList = jobs.Select(s => new JobResult
            {
                Benefits = s.Benefits,
                Description = s.Description,
                Pay = s.Pay,
                Position = s.Position,
                Quality = s.Quality,
                WorkType = s.WorkType,
                EndDate = s.EndDate
            }).ToList();

            return jobList;
        }

        private async Task<int> CalculateJobQuality(CreateJobRequest request)
        {
            int quality = 0;

            if(!string.IsNullOrEmpty(request.WorkType))
            {
                quality++;
            }

            if (request.Pay > 0)
            {
                quality++;
            }

            if (!string.IsNullOrEmpty(request.Benefits))
            {
                quality++;
            }

            var hasUnfavorableWord = await _unfavorableWordService.CheckStringForUnfavorableWordAsync(request.Description);

            if (!hasUnfavorableWord)
            {
                quality = quality + 2;
            }

            return quality;
        }
    }
}