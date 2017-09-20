using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Core.Repository.Dapper
{
    public class DapperCommand<T> : IDapperCommand
    {
        private readonly IDbConnection _dbConnection;

        public DapperCommand(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

      
    }
}