﻿using Carservice.Models.Users;

namespace Carservice.Models.Repair
{
    public class RepairRequest
    {
        public int Id { get; set; }
        public string AppUserId { get; set; } = "";
        public AppUser? AppUser { get; set; }
        public int RequestStatusId { get; set; }
        public RequestStatus? Status { get; set; }
        public int MadeYear { get; set; }
        public string CarBrand { get; set; } = "";
        public string Mileage { get; set; } = "";
        public string Servise { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string ContactNumber { get; set; } = "";
        public string RequestText { get; set; } = "";
    }
}
