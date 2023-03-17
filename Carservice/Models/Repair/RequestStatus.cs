namespace Carservice.Models.Repair
{
    public class RequestStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<RepairRequest>? RepairRequests { get; set; }
    }
}
