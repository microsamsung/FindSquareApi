using SquareApi.Core.Business;
using SquareApi.Core.Model;


[TestClass]
public class SquareManagerTests
{
    private SquareManager? _squareManager;

    [TestInitialize]
    public void Setup()
    {
        _squareManager = new SquareManager();
    }

    [TestMethod]
    public void IdentifySquares_ReturnsCorrectNumberOfSquares()
    {
        // Arrange
        var points = new List<Point>
        {
            new Point { X = 1, Y = 1 },
            new Point { X = 1, Y = -1 },
            new Point { X = -1, Y = 1 },
            new Point { X = -1, Y = -1 },
            new Point { X = 2, Y = 2 },
            new Point { X = 2, Y = -2 },
            new Point { X = -2, Y = 2 },
            new Point { X = -2, Y = -2 }
        };

        // Act
        var squares = _squareManager.IdentifySquares(points);

        // Assert
        Assert.AreEqual(2, squares.Count()); // Expecting 2 squares
    }

    [TestMethod]
    public void IdentifySquares_WithInvalidPoints_ReturnsEmptyList()
    {
        // Arrange
        var invalidPoints = new List<Point>
        {
            new Point { X = 21, Y = 1 },
            new Point { X = 1, Y = -1 },
            new Point { X = -1, Y = 13 },
            new Point { X = -1, Y = -1 },
            new Point { X = 2, Y = 2 },
            new Point { X = 22, Y = 2 },
            new Point { X = 2, Y = 42 },
            new Point { X = 2, Y = 32 },
            new Point { X = 33, Y = 3 },
            new Point { X = -3, Y = 8 }
        };

        // Act
        var squares = _squareManager.IdentifySquares(invalidPoints);

        // Assert
        Assert.IsFalse(squares.Any()); // Expecting no squares to be identified with invalid input
    }

    [TestMethod]
    public void IdentifySquares_WithDuplicatePoints_ReturnsCorrectNumberOfSquares()
    {
        // Arrange
        var points = new List<Point>
        {
            new Point { X = 1, Y = 1 },
            new Point { X = 1, Y = 1 }, // Duplicate
            new Point { X = -1, Y = 1 },
            new Point { X = -1, Y = -1 },
            new Point { X = 1, Y = -1 }
        };

        // Act
        var squares = _squareManager.IdentifySquares(points);

        // Assert
        Assert.AreEqual(1, squares.Count()); // Expecting 1 square
    }

    [TestMethod]
    public void IdentifySquares_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var points = new List<Point>(); // Empty list

        // Act
        var squares = _squareManager.IdentifySquares(points);

        // Assert
        Assert.IsFalse(squares.Any()); // Expecting no squares with empty input
    }

   



}