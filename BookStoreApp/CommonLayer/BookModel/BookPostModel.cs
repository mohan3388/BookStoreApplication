using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookModel
{
    public class BookPostModel
    {
        [Key]
        public int BookId { get; set; }
      
        public string BookName { get; set; }
     
        public string Description { get; set; }
        
        public string AuthorName { get; set; }
        
        public double ActualPrice { get; set; }
      
        public double DiscountedPrice { get; set; }
        public int TotalRating { get; set; }
        public int RatingCount { get; set; }
     
        public int Quantity { get; set; }

        public string BookImage { get; set; }
    }
}
