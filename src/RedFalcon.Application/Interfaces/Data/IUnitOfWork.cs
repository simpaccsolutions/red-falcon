using System;
using System.Collections.Generic;
using System.Text;

namespace RedFalcon.Application.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IContactRepository Contacts { get; }

        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
