﻿namespace BusinessLogic.Models.Sessions
{
    public class SeatModel
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }
    }
}
