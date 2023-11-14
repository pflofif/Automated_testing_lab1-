using System.Data.SqlClient;
using System.Linq.Expressions;

namespace AnalizerClassLibrary.Tests;

public class ExpressionData
{
    public string Expression { get; set; }
    public string ExpectedResult { get; set; }
}

public class DbManager
{
    private const string ConnectionString =
        @"server=(localdb)\MSSQLLocalDB;database=ArithmeticExpressionsDB;trusted_connection=true;Integrated Security=True";

    public ExpressionData GetExpressionById(int id)
    {
        ExpressionData expressionData = new();

        using SqlConnection connection = new(ConnectionString);
        
        connection.Open();
        string query = "SELECT Expression, ExpectedResult FROM ArithmeticExpressions WHERE Id = @Id";
        
        using SqlCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        
        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            expressionData.Expression = reader["Expression"].ToString();
            expressionData.ExpectedResult = reader["ExpectedResult"].ToString();
        }
        return expressionData;
    }
}