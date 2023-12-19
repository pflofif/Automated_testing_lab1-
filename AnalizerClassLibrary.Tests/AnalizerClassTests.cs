using System.Collections;
using AnalaizerClassLibrary;
using Xunit;

namespace AnalizerClassLibrary.Tests;

public class AnalizerClassTests
{
    private readonly DbManager _dbManager;

    public AnalizerClassTests()
    {
        _dbManager = new DbManager();
    }

    [Fact]
    public void CheckCurerncy_ShouldReturnExpectedResult()
    {
        var ids = _dbManager.GetAllIds();
        foreach (string id in ids)
        {
            TestAnalaizer(int.Parse(id));
        }
    }

    private void TestAnalaizer(int id)
    {
        //Arrange
        ExpressionData expressionData = _dbManager.GetExpressionById(id);
        AnalaizerClass.expression = expressionData.Expression;

        //Act
        bool result = AnalaizerClass.CheckCurrency();

        //Assert
        Assert.Equal(result, expressionData.ExpectedResult);
    }
}