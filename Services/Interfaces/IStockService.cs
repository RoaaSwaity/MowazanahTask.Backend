using Domain.Entities;

namespace Service.Interfaces
{
    public interface IStockService
    {
        Task<List<Stock>> Get();
    }
}