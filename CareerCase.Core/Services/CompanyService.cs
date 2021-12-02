using CareerCase.Core.Services.Interfaces;
using CareerCase.Domain;
using CareerCase.Domain.Entities;
using CareerCase.Domain.Requests.Company;
using CareerCase.Domain.Results;
using CareerCase.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CareerCase.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly IRepository<Company> _companyRepository;

        public CompanyService(ILogger<CompanyService> logger, IRepository<Company> companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task<ServiceResult> CreateCompanyAsync(CreateCompanyRequest createCompanyRequest)
        {
            try
            {
                var hasCompanyByPhoneNumber = await _companyRepository.ExistsAsync(q => q.Phone == createCompanyRequest.Phone);

                if(hasCompanyByPhoneNumber)
                {
                    return ServiceResult.Error("Bu telefon numarasına kayıtlı firma mevcut.");
                }

                var company = new Company
                {
                    Address = createCompanyRequest.Address,
                    JobLimit = Constants.JobLimit,
                    Name = createCompanyRequest.Name,
                    Phone = createCompanyRequest.Phone
                };

                await _companyRepository.CreateAsync(company);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.Error("Opss! Bir hata oluştu.");
            }
        }
    }
}