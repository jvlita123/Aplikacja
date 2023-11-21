using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class CategoriesService
    {
        private readonly CategoriesRepository _categoriesRepository;
        public CategoriesService(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = _categoriesRepository.GetAll().ToList();

            return categories;
        }

        public Category Add(Category category)
        {
            Category newCategory = _categoriesRepository.AddAndSaveChanges(category);

            return newCategory;
        }
    }
}
