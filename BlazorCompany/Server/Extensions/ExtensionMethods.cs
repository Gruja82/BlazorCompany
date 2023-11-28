using BlazorCompany.Data.Database;
using BlazorCompany.Data.Entities;
using BlazorCompany.Server.Repositories.Categories;
using BlazorCompany.Server.Repositories.UnitOfWork;
using BlazorCompany.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace BlazorCompany.Server.Extensions
{
    // This static class contains extension methods for facilitating Program.cs
    // file and to avoid writing similar code over and over
    public static class ExtensionMethods
    {
        // This extension method converts string value to double
        public static double ConvertStringToDouble(this string strValue)
        {
            NumberFormatInfo provider = new();
            provider.NumberDecimalSeparator = ".";
            return Convert.ToDouble(strValue, provider);
        }

        // This extension metod extends WebApplicationBuilder by adding functionallity
        // for registering AppDbContext and all services to DI container
        public static void AddServicesToContainer(this WebApplicationBuilder builder, string connString)
        {
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connString));
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This extension method extends BaseDto object.
        // It's purpose is to store image file in specified folder
        // and to return image file name
        public static string StoreImage(this BaseDto baseDto, string imagesFolder)
        {
            // Variable that represents image file name
            string? imageFileName = string.Empty;

            // If BaseDto's Image is not null, then generate unique file name
            // and store image file in specified folder
            if (baseDto.Image != null)
            {
                StringBuilder sb = new();

                sb.Append(Guid.NewGuid().ToString().Substring(0, 10));
                sb.Append("_");
                sb.Append(baseDto.Image.FileName);
                imageFileName = sb.ToString();

                string filePath = Path.Combine(imagesFolder, imageFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                baseDto.Image.CopyTo(fileStream);
            }

            // Return image file name
            return imageFileName;
        }

        // This extension method converts Category object to CategoryDto object
        public static CategoryDto ConvertToDto(this Category category)
        {
            CategoryDto categoryDto = new();

            categoryDto.Id = category.Id;
            categoryDto.Code = category.Code;
            categoryDto.Name = category.Name;
            categoryDto.Description = category.Description;

            return categoryDto;
        }
    }
}
