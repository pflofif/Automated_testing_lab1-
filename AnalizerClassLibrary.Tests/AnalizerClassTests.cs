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

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void CreateStack_ShouldReturnCorrectResult_WhenGivenSimpleArithmeticExpression(int id)
    {
        TestAnalaizer(id);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(2)]
    public void CreateStack_ShouldReturnCorrectResult_WhenGivenExpressionWithBrackets(int id)
    {
        TestAnalaizer(id);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    public void CreateStack_ShouldReturnCorrectResult_WhenGivenExpressionWithSingleNumber(int id)
    {
        TestAnalaizer(id);
    }

    private void TestAnalaizer(int id)
    {
        //Arrange
        ExpressionData expressionData = _dbManager.GetExpressionById(id);
        AnalaizerClass.expression = expressionData.Expression;
        ArrayList expectedArray = new(expressionData.ExpectedResult.Split(','));

        //Act
        ArrayList result = AnalaizerClass.CreateStack();

        //Assert
        Assert.Equal(result, expectedArray);
    }
}