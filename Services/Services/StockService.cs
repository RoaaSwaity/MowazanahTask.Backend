using Domain.Entities;
using Service.Interfaces;

namespace Service.Services
{
    public class StockService : IStockService
    {
        private readonly IRepository<Stock> _repository;

        public StockService(IRepository<Stock> repository)
        {
            _repository = repository;
        }

        public async Task<List<Stock>> Get()
        {
            var results = await _repository.GetAllAsync();
            
            return results.Take(100).ToList();
        }
    }
}
