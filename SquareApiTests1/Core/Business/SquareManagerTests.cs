using SquareApi.Core.Model;

namespace SquareApi.Core.Business.Tests
{
    [TestClass()]
    public class SquareManagerTests
    {
        [TestMethod]
        public void IdentifySquares_ReturnsCorrectNumberOfSquares()
        {
            // Arrange
            var squareManager = new SquareManager();
            var points = new List<Point>();
            points.Add(new Point { X = 1, Y = 1 });
            points.Add(new Point { X = 1, Y = -1 });
            points.Add(new Point { X = -1, Y = 1 });
            points.Add(new Point { X = -1, Y = -1 });
            points.Add(new Point { X = 2, Y = 2 });
            points.Add(new Point { X = 2, Y = -2 });
            points.Add(new Point { X = -2, Y = 2 });
            points.Add(new Point { X = -2, Y = -2 });
            points.Add(new Point { X = 3, Y = 3 });
            points.Add(new Point { X = -3, Y = 8 });
            // Act
            var squares = squareManager.IdentifySquares(points);

            // Assert
            Assert.AreEqual(2, squares.Count()); 
        }

        [TestMethod]
        public void IdentifySquares_WithInvalidPoints_ReturnsEmptyList()
        {
            // Arrange
            var squareManager = new SquareManager();
            var invalidPoints = new List<Point>();
            invalidPoints.Add(new Point { X = 21, Y = 1 });
            invalidPoints.Add(new Point { X = 1, Y = -1 });
            invalidPoints.Add(new Point { X = -1, Y = 13 });
            invalidPoints.Add(new Point { X = -1, Y = -1 });
            invalidPoints.Add(new Point { X = 2, Y = 2 });
            invalidPoints.Add(new Point { X = 22, Y = 2 });
            invalidPoints.Add(new Point { X = 2, Y = 42 });
            invalidPoints.Add(new Point { X = 2, Y = 32 });
            invalidPoints.Add(new Point { X = 33, Y = 3 });
            invalidPoints.Add(new Point { X = -3, Y = 8 });
            
            // Act
            var squares = squareManager.IdentifySquares(invalidPoints);

            // Assert
            Assert.IsFalse(squares.Any()); // Expecting no squares to be identified with invalid input
        }

    }
}