namespace RedFalcon.Application.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository Contacts { get; }
        IOrganizationRepository Organizations { get; }

        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
