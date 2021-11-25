namespace WebAPI.QueryFilters
{
    public class TicketQueryFilter
    {
        public int? Id { get; set; }    
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Owner { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTime? EnteredDate { get; set; }


    }
}
