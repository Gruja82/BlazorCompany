using BlazorCompany.Shared.Dtos;

namespace BlazorCompany.Server.Repositories.Categories
{
    // Interface that declares operations that can be applied
    // on CategoryDto objects
    public interface ICategoryRepository
    {
        // Return list of CategoryDto objects
        Pagination<CategoryDto> GetFilteredCategories(string searchText, int pageIndex, int pageSize);
        // Return single CategoryDto object
        Task<CategoryDto> GetSingleCategoryAsync(int id);
        // Create new Category
        Task CreateNewCategoryAsync(CategoryDto categoryDto);
        // Update existing Category
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        // Delete existing Category
        Task DeleteCategoryAsync(int id);
        // Custom validation
        Task<Dictionary<string, string>> ValidateCategoryAsync(CategoryDto categoryDto);

    }
}
