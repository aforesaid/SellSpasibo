using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class UserInfoEntity : Entity
    {
        private const int PhoneLength = 12;
        private const int PhoneLinkIdLength = 20;
        private const int NameLength = 100;
        
        private UserInfoEntity() { }
        public UserInfoEntity(string phone, string phoneLinkId, string name, bool phoneIsValid = true)
        {
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            PhoneLinkId = phoneLinkId ?? throw new ArgumentNullException(nameof(phoneLinkId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneIsValid = phoneIsValid;
        }
        [StringLength(PhoneLength)]
        public string Phone { get; protected set; }
        public bool PhoneIsValid { get; protected set; }
        [StringLength(PhoneLinkIdLength)]
        public string PhoneLinkId { get; protected set; }
        [StringLength(NameLength)]
        public string Name { get; protected set; }

        public void SetInactive()
        {
            IsDeleted = false;
            SetUpdated();
        }

        public void Merge(UserInfoEntity userInfo)
        {
            PhoneLinkId = userInfo.PhoneLinkId;
            Name = userInfo.Name;
            SetUpdated();
        }
    }
}