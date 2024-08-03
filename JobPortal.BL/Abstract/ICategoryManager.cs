using JobPortal.Entities.Models.Concrete;

namespace JobPortal.BL.Abstract
{
    public interface ICategoryManager : IManager<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
