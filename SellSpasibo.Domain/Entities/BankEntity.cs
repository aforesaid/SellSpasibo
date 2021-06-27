using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class BankEntity : Entity
    {
        private const int MemberIdLength = 20;
        private const int NameLength = 100;
        
        private BankEntity() {}
        public BankEntity(string memberId, string name)
        {
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        [StringLength(MemberIdLength)]
        public string MemberId { get; protected set; }
        [StringLength(NameLength)]
        public string Name { get; protected set; }

        public void SetMemberId(string memberId)
        {
            MemberId = memberId;
            SetUpdated();
        }
    }
}
