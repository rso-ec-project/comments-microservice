﻿namespace Comments.Application.Ratings
{
    public class RatingDto
    {
        public int ChargingStationId { get; set; }
        public double Rating { get; set; }
        public int TotalRatingCount { get; set; }
        public int Rating1Count { get; set; }
        public int Rating2Count { get; set; }
        public int Rating3Count { get; set; }
        public int Rating4Count { get; set; }
        public int Rating5Count { get; set; }
    }
}
