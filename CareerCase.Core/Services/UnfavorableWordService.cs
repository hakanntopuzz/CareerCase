using CareerCase.Core.Services.Interfaces;
using CareerCase.Domain;
using CareerCase.Domain.Entities;
using CareerCase.Domain.Requests.UnfavorableWord;
using CareerCase.Domain.Results;
using CareerCase.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCase.Core.Services
{
    public class UnfavorableWordService : IUnfavorableWordService
    {
        private readonly ILogger<UnfavorableWordService> _logger;
        private readonly IRepository<UnfavorableWord> _wordRepository;
        private readonly ICacheService _cacheService;

        public UnfavorableWordService(ILogger<UnfavorableWordService> logger, IRepository<UnfavorableWord> wordRepository, ICacheService cacheService)
        {
            _logger = logger;
            _wordRepository = wordRepository;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateWordAsync(CreateWordRequest createWordRequest)
        {
            try
            {
                var word = new UnfavorableWord
                {
                    Word = createWordRequest.Word
                };

                await _wordRepository.CreateAsync(word);
                _cacheService.Remove(Constants.UnfavorableWordsCacheKey);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.Error("Opss! Bir hata oluştu.");
            }
        }

        public async Task<bool> CheckStringForUnfavorableWordAsync(string text)
        {
            var words = await GetUnfavorableWords();

            return words.Any(q => text.Contains(q));
        }

        private async Task<ICollection<string>> GetUnfavorableWords()
        {
            var unfavorableWords = _cacheService.Get<ICollection<string>>(Constants.UnfavorableWordsCacheKey);

            if (unfavorableWords == null)
            {
                var unfavorableWordList = await _wordRepository.ToListAsync();
                _cacheService.Set(Constants.UnfavorableWordsCacheKey, unfavorableWordList.Select(s=> s.Word), TimeSpan.FromDays(Constants.WordsCacheDayCount));
            }

            return unfavorableWords;
        }
    }
}