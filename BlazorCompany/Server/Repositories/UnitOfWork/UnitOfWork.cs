using BlazorCompany.Data.Database;
using BlazorCompany.Server.Repositories.Categories;

namespace BlazorCompany.Server.Repositories.UnitOfWork
{
    // Implementation class for IUnitOfWork
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;

        // Initialize AppDbContext and all specific repositories
        public UnitOfWork(AppDbContext context, ICategoryRepository categoryRepository)
        {
            this.context = context;
            CategoryRepository = categoryRepository;
        }

        public ICategoryRepository CategoryRepository { get; }

        // Method for saving changes to database
        public async Task ConfirmChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        // Method for manual disposing DbContext object
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
