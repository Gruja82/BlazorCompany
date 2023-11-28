using BlazorCompany.Client.Services.Categories;
using BlazorCompany.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace BlazorCompany.Client.Pages.Categories
{
    public partial class SingleCategory
    {
        [Inject]
        private ICategoryService CategoryService { get; set; } = default!;
        [Inject]
        private NavigationManager NavManager { get; set; } = default!;
        [Inject]
        private IJSRuntime JsRuntime { get; set; } = default!;

        private bool hidden;
        private EditContext? Context { get; set; }
        private ValidationMessageStore? messageStore;
        private Dictionary<string, string>? errors;

        [Parameter]
        public int Id { get; set; }

        private CategoryDto? CategoryModel { get; set; }

        protected override void OnInitialized()
        {
            CategoryModel = new();
            Context = new(new object());
            errors = new();
            hidden = true;
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id > 0)
            {
                CategoryModel = await CategoryService.GetItem(Id);
                hidden = false;
            }

            Context = new(CategoryModel!);
            messageStore = new(Context);
        }

        private async Task SubmitAsync()
        {
            if (Id == 0)
            {
                var response = await CategoryService.CreateNewItem(CategoryModel!);

                if (response.GetType() == typeof(Dictionary<string, string>))
                {
                    errors = (Dictionary<string, string>)response;

                    AddModelError();
                }
                else
                {
                    NavManager.NavigateTo("/categories");
                }
            }
            else
            {
                var response = await CategoryService.EditItem(CategoryModel!);

                if (response.GetType() == typeof(Dictionary<string, string>))
                {
                    errors = (Dictionary<string, string>)response;

                    AddModelError();
                }
                else
                {
                    NavManager.NavigateTo("/categories");
                }
            }
        }

        private void AddModelError()
        {
            foreach (var error in errors!)
            {
                FieldIdentifier modelField = new(CategoryModel!, error.Key);

                messageStore!.Add(modelField, error.Value);
            }
        }

        private async Task Delete()
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this Category?");

            if (confirmed)
            {
                await CategoryService.DeleteItem(CategoryModel!.Id);

                NavManager.NavigateTo("/categories");
            }
        }
    }
}
