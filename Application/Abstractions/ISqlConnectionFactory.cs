using System.Data;

namespace Application.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}

