using System;
using System.ComponentModel.DataAnnotations.Schema;
using HRM.Entity.Common;

namespace HRM.Entity.Entities
{
    public class RefreshToken : EntityBase
    {
        [ForeignKey("FK_RT_User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpriredAt { get; set; }
        
    }
}