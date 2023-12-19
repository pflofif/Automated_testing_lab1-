using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace AnalizerClassLibrary.Tests;

public class ExpressionData
{
    public string Expression { get; set; }
    public bool ExpectedResult { get; set; }
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
            expressionData.ExpectedResult = reader["ExpectedResult"].ToString() == "1";;
        }
        return expressionData;
    }

    public List<string> GetAllIds()
    {
        List<string> ids = new List<string>(); // Створюємо список для збереження ID

        ExpressionData expressionData = new();

        using SqlConnection connection = new(ConnectionString);
        
        connection.Open();
        string query = "SELECT Id FROM ArithmeticExpressions";
        using SqlCommand command = new(query, connection);
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read()) // Проходимо через кожен рядок результатів
        {
            // Додаємо ID до списку
            ids.Add(reader["Id"].ToString()); // Переводимо значення в строку та додаємо до списку
        }

        return ids;
    }
}