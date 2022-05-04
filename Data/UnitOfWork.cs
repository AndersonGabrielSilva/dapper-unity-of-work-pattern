using System.Data;

namespace DapperUowTests.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        //Deve ser chamado antes de realizar qualquer ação no banco de dados de preferencia no proprio Controller
        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        //Criando Transação
        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        //Realizando Commit
        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        //Realizando RollBack
        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();
    }
}
