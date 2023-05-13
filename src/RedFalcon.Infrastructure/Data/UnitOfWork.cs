using RedFalcon.Application.Interfaces.Data;

namespace RedFalcon.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseSession _dbSession;
        private readonly IContactRepository _contactRepository;

        public UnitOfWork(DatabaseSession dbSession, IContactRepository contactRepository)
        {
            _dbSession = dbSession;
            _contactRepository = contactRepository;
        }

        public IContactRepository Contacts => _contactRepository;

        public void Commit()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Commit();
            Dispose();
        }

        public void CreateTransaction()
        {
            if (_dbSession.Connection != null)
            {
                _dbSession.Transaction = _dbSession.Connection.BeginTransaction();
            }
            else
            {
                throw new Exception("Database Session is null");
            }
        }

        public void Dispose()
        {
            _dbSession.Transaction?.Dispose();
        }

        public void Rollback()
        {
            if (_dbSession.Transaction != null)
                _dbSession.Transaction.Rollback();
            Dispose();
        }
    }
}
