using CareerCase.Core.Services.Interfaces;
using CareerCase.Domain.Requests.Company;
using CareerCase.Domain.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CareerCase.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        #region Ctor
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        #endregion

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        public async Task<ActionResult<ServiceResult>> CreateCompany(CreateCompanyRequest request)
        {
            var response = await _companyService.CreateCompanyAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}