using BlazorCompany.Shared.Dtos;

namespace BlazorCompany.Client.Services.Categories
{
    // Interface that defines methods for communicating with Server
    public interface ICategoryService
    {
        // Return filtered list of CategoryDto objects
        Task<Pagination<CategoryDto>> GetItems(string? searchText, int pageIndex, int pageSize);
        // Return single CategoryDto
        Task<CategoryDto> GetItem(int id);
        // Create new Category
        Task<object> CreateNewItem(CategoryDto categoryDto);
        // Edit selected Category
        Task<object> EditItem(CategoryDto categoryDto);
        // Delete selected Category
        Task<int> DeleteItem(int id);
    }
}
