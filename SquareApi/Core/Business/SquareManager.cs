using SquareApi.Core.Dto;
using SquareApi.Core.Model;

namespace SquareApi.Core.Business
{
    public class SquareManager
    {

        public IEnumerable<Square> IdentifySquares(List<Point> points)
        {
            var squares = new List<Square>();

            // Iterate through all combinations of four points
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    for (int k = j + 1; k < points.Count; k++)
                    {
                        for (int l = k + 1; l < points.Count; l++)
                        {
                            var candidateSquare = new ShapeDto
                            {
                                Point1 = points[i],
                                Point2 = points[j],
                                Point3 = points[k],
                                Point4 = points[l]
                            };

                            // Check if the candidate points form a square
                            if (IsSquare(candidateSquare))
                            {
                                squares.Add(new Square()
                                {
                                    Point1 = candidateSquare.Point1,
                                    Point2 = candidateSquare.Point2,
                                    Point3 = candidateSquare.Point3,
                                    Point4 = candidateSquare.Point4
                                });
                            }
                        }
                    }
                }
            }

            return squares;
        }



        private bool IsSquare(ShapeDto shape)
        {
            // Calculate distances between all pairs of points
            var distances = new double[6];
            distances[0] = Distance(shape.Point1, shape.Point2);
            distances[1] = Distance(shape.Point1, shape.Point3);
            distances[2] = Distance(shape.Point1, shape.Point4);
            distances[3] = Distance(shape.Point2, shape.Point3);
            distances[4] = Distance(shape.Point2, shape.Point4);
            distances[5] = Distance(shape.Point3, shape.Point4);

            // Sort distances to simplify comparison
            Array.Sort(distances);

            // Check if all sides have equal lengths
            if (distances[0] != distances[1] || distances[1] != distances[2] || distances[2] != distances[3])
            {
                return false;
            }

            // Check if diagonals have equal lengths
            if (distances[4] != distances[5])
            {
                return false;
            }

            // Check if all angles are 90 degrees (optional)
            // You can skip this check if not required

            return true;
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
