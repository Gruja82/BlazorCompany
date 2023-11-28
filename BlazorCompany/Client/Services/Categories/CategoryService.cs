using BlazorCompany.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace BlazorCompany.Client.Services.Categories
{
    // Implementation class for ICategoryService
    public class CategoryService:ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // Create new Category
        public async Task<object> CreateNewItem(CategoryDto categoryDto)
        {
            // Call API method for creating new Category
            var response = await httpClient.PostAsJsonAsync<CategoryDto>("api/Category/CreateNewCategory", categoryDto);

            // If response is not null
            if(response != null)
            {
                // If returned status code is 200 (OK), then return bool value 'true'
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                // Otherwise return Dictionary containg errors
                else
                {
                    var errors = await response.Content.ReadFromJsonAsync<IDictionary<string, string>>();
                    return errors ?? new Dictionary<string, string>();
                }
            }
            // Otherwise throw exception
            else
            {
                throw new Exception("Unexpected error!");
            }
        }

        // Delete Category
        public async Task<int> DeleteItem(int id)
        {
            // Navigate to URL for deleting selected Category
            var response = await httpClient.PostAsJsonAsync("api/Category/DeleteCategory", id);

            if (response.IsSuccessStatusCode)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        // Edit selected Category
        public async Task<object> EditItem(CategoryDto categoryDto)
        {
            // Navigate to URL for editing selected Category
            var response = await httpClient.PostAsJsonAsync<CategoryDto>("api/Category/EditCategory", categoryDto);

            // If response is not null
            if (response != null)
            {
                // If returned status code is 200 (OK), then return bool value 'true'
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                // Otherwise return Dictionary containg errors
                else
                {
                    var errors = await response.Content.ReadFromJsonAsync<IDictionary<string, string>>();
                    return errors ?? new Dictionary<string, string>();
                }
            }
            // Otherwise throw exception
            else
            {
                throw new Exception("Unexpected error!");
            }
        }

        // Return single Category record
        public async Task<CategoryDto> GetItem(int id)
        {
            // Navigate to URL to fetch specific CategoryDto record
            var response = await httpClient.GetAsync($"api/Category/GetSingleCategory/{id}");

            // If returned result is not null
            if (response != null)
            {
                // If returned status code is Success status code
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the result
                    CategoryDto? categoryDto = await response.Content.ReadFromJsonAsync<CategoryDto>();

                    // If categoryDto is not null, return categoryDto.
                    // Otherwise, return new CategoryDto object
                    return categoryDto ?? new CategoryDto();
                }
                // Otherwise return new CategoryDto object
                else
                {
                    return new CategoryDto();
                }
            }
            // Otherwise return new CategoryDto object
            else
            {
                return new CategoryDto();
            }
        }

        // Return list of CategoryDto objects
        public async Task<Pagination<CategoryDto>> GetItems(string? searchText, int pageIndex, int pageSize)
        {
            // Dictionary that will be used to store query string values
            Dictionary<string, string> queryParams = new();

            queryParams["searchText"] = searchText ?? string.Empty;
            queryParams["pageIndex"] = pageIndex == 0 ? GlobalParameters.PAGE_INDEX.ToString() : pageIndex.ToString();
            queryParams["pageSize"] = pageSize == 0 ? GlobalParameters.PAGE_SIZE.ToString() : pageSize.ToString();

            // API Url
            string url = QueryHelpers.AddQueryString("api/Category/GetCategories", queryParams);

            // Navigate to url
            var response = await httpClient.GetAsync(url);

            // If response is not null
            if (response != null)
            {
                // If returned status code is Success status code
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response
                    var categories = await response.Content.ReadFromJsonAsync<Pagination<CategoryDto>>();

                    // If categories is null then return new Pagination<CategoryDto>
                    // Otherwise return categories
                    return categories ?? new Pagination<CategoryDto>();
                }
                // Otherwise return new Pagination<CategoryDto>
                else
                {
                    return new Pagination<CategoryDto>();
                }
            }
            // Otherwise return new Pagination<CategoryDto>
            else
            {
                return new Pagination<CategoryDto>();
            }
        }
    }
}
