using BuildingBlocks.BaseEntities;

namespace Accounts.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }

        private Account() : base() { }

        public static Account Create(string email, string passwordHash, string passwordSalt)
        {
            var account = new Account
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return account;
        }

        public void ChangePassword(string newPasswordHash, string passwordSalt)
        {
            PasswordHash = newPasswordHash;
            PasswordSalt = passwordSalt;
            UpdateModificationDate();
        }

        public bool VerifyPassword(string passwordHash)
        {
            return PasswordHash == passwordHash;
        }
    }
}
