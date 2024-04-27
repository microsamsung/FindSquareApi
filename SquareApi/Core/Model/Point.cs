using System.ComponentModel.DataAnnotations;

namespace SquareApi.Core.Model
{
    /// <summary>
    /// Class to hold the ID and Cordinates
    /// </summary>
    public class Point
    {
        public int Id { get; set; }

        [Range(-100, 100, ErrorMessage = "X coordinate must be between -100 and 100.")]
        public int X { get; set; }

        [Range(-100, 100, ErrorMessage = "Y coordinate must be between -100 and 100.")]
        public int Y { get; set; }
    }
}
