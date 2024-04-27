using SquareApi.Core.Dto;
using SquareApi.Core.Model;

namespace SquareApi.Core.Business
{
    public class SquareManager
    {
        /// <summary>
        /// Methods to calculate number of Square can be draw from Points stored in DB
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public IEnumerable<Square> IdentifySquares(List<Point> points)
        {
            var squares = new List<Square>();

            // Create a list to store unique points
            var uniquePoints = new List<Point>();

            // Create a dictionary to quickly check if a point exists
            var pointDictionary = new Dictionary<string, Point>();

            // Iterate through each point and add it to uniquePoints if it's not a duplicate
            foreach (var point in points)
            {
                var key = $"{point.X},{point.Y}";
                if (!pointDictionary.ContainsKey(key))
                {
                    uniquePoints.Add(point);
                    pointDictionary[key] = point;
                }
            }

            // Iterate through each combination of four points
            for (int i = 0; i < uniquePoints.Count - 3; i++)
            {
                for (int j = i + 1; j < uniquePoints.Count - 2; j++)
                {
                    for (int k = j + 1; k < uniquePoints.Count - 1; k++)
                    {
                        for (int l = k + 1; l < uniquePoints.Count; l++)
                        {
                            var p1 = points[i];
                            var p2 = points[j];
                            var p3 = points[k];
                            var p4 = points[l];

                            // Check if the points form a square
                            if (IsSquare(p1, p2, p3, p4))
                            {
                                // Ensure all points of the square are unique
                                if (pointDictionary.ContainsKey($"{p1.X},{p1.Y}") &&
                                    pointDictionary.ContainsKey($"{p2.X},{p2.Y}") &&
                                    pointDictionary.ContainsKey($"{p3.X},{p3.Y}") &&
                                    pointDictionary.ContainsKey($"{p4.X},{p4.Y}"))
                                {
                                    squares.Add(new Square
                                    {
                                        Id = squares.Count + 1,
                                        Point1 = p1,
                                        Point2 = p2,
                                        Point3 = p3,
                                        Point4 = p4
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return squares;
        }

        /// <summary>
        /// Helper method to check if four points form a square
        /// </summary>
        /// <param name="p1">p1</param>
        /// <param name="p2">p2</param>
        /// <param name="p3">p3</param>
        /// <param name="p4">p4</param>
        /// <returns>bool</returns>

        private bool IsSquare(Point p1, Point p2, Point p3, Point p4)
        {
            int d2 = DistanceSquare(p1, p2);  // from p1 to p2
            int d3 = DistanceSquare(p1, p3);  // from p1 to p3
            int d4 = DistanceSquare(p1, p4);  // from p1 to p4

            if (d2 == 0 || d3 == 0 || d4 == 0)
                return false;

            // If lengths of (p1, p2) and (p1, p3) are same, then p4 must be same distance away from p2 and p3
            if (d2 == d3 && 2 * d2 == d4 && 2 * DistanceSquare(p2, p4) == DistanceSquare(p2, p3))
            {
                return true;
            }

            // The above condition is not true, so lengths of (p1, p3) and (p1, p4) must be same
            if (d3 == d4 && 2 * d3 == d2 && 2 * DistanceSquare(p3, p2) == DistanceSquare(p3, p4))
            {
                return true;
            }

            // The above condition is not true, so lengths of (p1, p2) and (p1, p4) must be same
            if (d2 == d4 && 2 * d2 == d3 && 2 * DistanceSquare(p2, p3) == DistanceSquare(p2, p4))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Helper method to calculate distance between two points
        /// </summary>
        /// <param name="p1">p1</param>
        /// <param name="p2">p2</param>
        /// <returns></returns>

        private int DistanceSquare(Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }
    }
}
