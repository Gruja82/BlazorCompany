using BlazorCompany.Server.Repositories.Categories;

namespace BlazorCompany.Server.Repositories.UnitOfWork
{
    // Interface that wraps up specific entity repositories
    // and declares method for saving changes to database
    public interface IUnitOfWork:IDisposable
    {
        public ICategoryRepository CategoryRepository { get; }
        Task ConfirmChangesAsync();
    }
}
