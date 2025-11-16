using System;
using Project.Domain.Enums;

namespace Project.Application.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}