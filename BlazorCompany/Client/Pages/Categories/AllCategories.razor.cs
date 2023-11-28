using BlazorCompany.Client.Services.Categories;
using BlazorCompany.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorCompany.Client.Pages.Categories
{
    public partial class AllCategories
    {
        [Inject]
        private ICategoryService CategoryService { get; set; } = default!;
        [Inject]
        private NavigationManager NavManager { get; set; } = default!;

        private Pagination<CategoryDto> CategoriesList { get; set; } = new();
        private string? SearchText { get; set; }
        private int PageIndex { get; set; }
        private int PageSize { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CategoriesList = await CategoryService.GetItems(SearchText, PageIndex, PageSize);
        }

        private async Task HandleChildSearch(string item)
        {
            SearchText = item;
            PageIndex = default;
            CategoriesList = await CategoryService.GetItems(SearchText, PageIndex, PageSize);
        }

        private async Task HandlePageSizeChanged(int value)
        {
            PageIndex = default;
            value = value > 0 ? value : 1;
            PageSize = value;
            CategoriesList = await CategoryService.GetItems(SearchText, PageIndex, PageSize);
        }

        private async Task HandlePageChanged(int value)
        {
            PageIndex = value;
            CategoriesList = await CategoryService.GetItems(SearchText, PageIndex, PageSize);
        }

        private void NavigateToCreatePage()
        {
            NavManager.NavigateTo("/category");
        }

        private void NavigateToEditPage(int id)
        {
            NavManager.NavigateTo($"/category/{id}");
        }
    }
}
