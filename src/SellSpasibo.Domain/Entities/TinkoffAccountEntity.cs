using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Domain.Entities
{
    public class TinkoffAccountEntity : Entity
    {
        private const int LoginLength = 100;
        private const int PasswordLength = 255;
        private const int PhoneLength = 12;
        
        private TinkoffAccountEntity(string phone)
        {
            Phone = phone;
        }

        public TinkoffAccountEntity(string login,
            string password, 
            int accountId, string phone)
        {
            Login = login;
            Password = password;
            AccountId = accountId;
            Phone = phone;
        }

        /// <summary>
        /// Зашифрованный логин от аккаунта Tinkoff
        /// </summary>
        [StringLength(LoginLength)]
        public string Login { get; private set; }
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
        public int AccountId { get; private set; }

        public void SetAccountId(int accountId)
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