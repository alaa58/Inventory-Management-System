using System.Linq.Expressions;
using InventoryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagementSystemAPI.Data
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(int id);
        T GetByID(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task SaveChangesAsync();
    }
}
