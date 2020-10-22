using System;
using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Repositories;
using Limit.OfficialSite.Domain.Result;

namespace Limit.OfficialSite.Production.Category
{
    public class CategoryManager
    {
        private readonly IRepository<UgcCategory, Guid> _categoryRepository;

        public CategoryManager(IRepository<UgcCategory, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DomainResult> AddToCategoryAsync(Guid categoryId, UgcBase ugc)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(categoryId);

            if (category == null)
            {
                return DomainResult.Failed(new ArgumentNullException($"找不Id为{categoryId}的category"));
            }

            return DomainResult.Success;
        }
    }
}