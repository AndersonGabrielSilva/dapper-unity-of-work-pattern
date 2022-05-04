using System;
using System.Data;
using System.Data.SqlClient;

namespace DapperUowTests.Data
{
    public sealed class DbSession : IDisposable
    {
        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        //Como a injeção de dependencia está como Scope isto garante que está instancia de coneção seja a mesma durante toda a requisição
        public DbSession()
        {
            _id = Guid.NewGuid();
            Connection = new SqlConnection(Settings.ConnectionString); //Criando conexão com o Banco de Dados
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
