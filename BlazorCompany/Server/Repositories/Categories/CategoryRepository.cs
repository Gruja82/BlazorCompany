using BlazorCompany.Data.Database;
using BlazorCompany.Data.Entities;
using BlazorCompany.Server.Extensions;
using BlazorCompany.Server.Utilities;
using BlazorCompany.Shared.Dtos;

namespace BlazorCompany.Server.Repositories.Categories
{
    // Implementation class for ICategoryRepository
    public class CategoryRepository:ICategoryRepository 
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        // Create new Category
        public async Task CreateNewCategoryAsync(CategoryDto categoryDto)
        {
            // Create new Category object
            Category category = new();

            category.Code = categoryDto.Code;
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            // Add category to database
            await context.Categories.AddAsync(category);
        }

        // Delete existing Category
        public async Task DeleteCategoryAsync(int id)
        {
            // Find Category by supplied id
            Category category = (await context.Categories.FindAsync(id))!;

            // Remove category from database
            context.Categories.Remove(category);
        }

        public Pagination<CategoryDto> GetFilteredCategories(string? searchText, int pageIndex, int pageSize)
        {
            var allCategories = context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                allCategories = allCategories.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                                                || e.Name.ToLower().Contains(searchText.ToLower()));
            }

            //Variable that will contain CategoryDto objects
            List<CategoryDto> categoryDtos = new();

            // Iterate through allCategories and populate categoryDtos
            // using Category's extension method ConvertToDto()
            foreach (var category in allCategories)
            {
                categoryDtos.Add(category.ConvertToDto());
            }

            // Using PaginationUtility's GetPaginatedResult() method, 
            // return Pagionation<CategoryDto> object
            return PaginationUtility<CategoryDto>.GetPaginatedResult(in categoryDtos, pageIndex, pageSize);
        }

        //// Return paginated list of CategoryDto objects
        //public Pagination<CategoryDto> GetCategoryList(string searchText, int pageIndex, int pageSize)
        //{
        //    // Fetch all Category records from database
        //    // using IQueryable to save memory
        //    var allCategories = context.Categories.AsQueryable();

        //    // If searchText is not null or empty string,
        //    // filter allCategories by searchText
        //    if(!string.IsNullOrEmpty(searchText))
        //    {
        //        allCategories = allCategories.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
        //                                        || e.Name.ToLower().Contains(searchText.ToLower()));
        //    }

        //    // Variable that will contain CategoryDto objects
        //    List<CategoryDto> categoryDtos = new();

        //    // Iterate through allCategories and populate categoryDtos
        //    // using Category's extension method ConvertToDto()
        //    foreach (var category in allCategories)
        //    {
        //        categoryDtos.Add(category.ConvertToDto());
        //    }

        //    // Using PaginationUtility's GetPaginatedResult() method, 
        //    // return Pagionation<CategoryDto> object
        //    return PaginationUtility<CategoryDto>.GetPaginatedResult(in categoryDtos, pageIndex, pageSize);
        //}

        // Return single CategoryDto object
        public async Task<CategoryDto> GetSingleCategoryAsync(int id)
        {
            // Find Category by supplied id
            Category category = (await context.Categories.FindAsync(id))!;

            // Return CategoryDto object using Category's extension method ConvertToDto()
            return category.ConvertToDto();
        }

        // Update existing Category
        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            // Find Category by supplied id
            Category category = (await context.Categories.FindAsync(categoryDto.Id))!;

            // Set category's properties to the ones contained in categoryDto
            category.Code = categoryDto.Code;
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            // Update category
            context.Categories.Update(category);
        }

        // Custom validation
        public async Task<Dictionary<string, string>> ValidateCategoryAsync(CategoryDto categoryDto)
        {
            // Define variable that will contain possible errors
            Dictionary<string, string> errors = new();

            // Fetch all Category records
            IQueryable<Category> allCategories = context.Categories.AsQueryable();

            // If categoryDto's Id value is grater than 0, then it means that Category is used in Update operation
            if (categoryDto.Id > 0)
            {
                // Find Category record by categoryDto's Id value
                Category category = (await context.Categories.FindAsync(categoryDto.Id))!;

                // If category's Code is modified, check for Code uniqueness
                // among all Category records
                if (category.Code != categoryDto.Code)
                {
                    // If provided categoryDto's Code is already contained in
                    // any of the Category records then add error to errors Dictionary
                    if (allCategories.Select(e => e.Code.ToLower()).Contains(categoryDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Category with this Code in database. Please provide different one!");
                    }
                }

                // If category's Name is modified, check for Name uniqueness
                // among all Category records
                if (category.Name != categoryDto.Name)
                {
                    // If provided categoryDto's Name is already contained in
                    // any of the Category records then add error to errors Dictionary
                    if (allCategories.Select(e => e.Name.ToLower()).Contains(categoryDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Category with this Name in database. Please provide different one!");
                    }
                }
            }

            // Return errors Dictionary
            return errors;
        }
    }
}
