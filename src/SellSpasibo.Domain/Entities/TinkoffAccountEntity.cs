using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class TinkoffAccountEntity : Entity
    {
        private const int PasswordLength = 255;
        private const int PhoneLength = 20;
        private const int AccountIdLength = 20;
        private TinkoffAccountEntity() { }

        public TinkoffAccountEntity(string password, 
            string accountId, string phone)
        {
            Password = password;
            AccountId = accountId;
            Phone = phone;
        }
        
        /// <summary>
        /// Зашифрованный пароль от аккаунта Tinkoff
        /// </summary>
        [StringLength(PasswordLength)]
        public string Password { get; private set; }
        /// <summary>
        /// Зашифрованный номер телефона
        /// </summary>
        [StringLength(PhoneLength)]
        public string Phone { get; private set; }
        /// <summary>
        /// Номер счёта среди всех
        /// </summary>
        [StringLength(AccountIdLength)]
        public string AccountId { get; private set; }

        public void SetAccountId(string accountId)
        {
            AccountId = accountId;
            SetUpdated();
        }

        public void SetPassword(string password)
        {
            Password = password;
            SetUpdated();
        }

        public void SetInactive()
        {
            IsDeleted = true;
            SetUpdated();
        }
    }
}