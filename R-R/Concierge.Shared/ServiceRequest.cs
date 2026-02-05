namespace Concierge.Shared
{
    public class ServiceRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RoomNumber { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;
    }
}
