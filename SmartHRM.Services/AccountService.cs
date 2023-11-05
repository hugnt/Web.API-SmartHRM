using HUG.CRUD.Services;
using SmartHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class AccountService 
    {
        private readonly AccountRepository _accountRepository;
        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public ResponseModel CreateAccount(Account accountCreate)
        {
            var accounts = _accountRepository.GetAll()
                            .Where(l => l.Username.Trim().ToLower() == accountCreate.Username.Trim().ToLower())
                            .FirstOrDefault();
            if (accounts != null)
            {
                return new ResponseModel(422, "Account already exists");
            }

            if (!_accountRepository.Create(accountCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");
        }

        public ResponseModel DeleteAccount(int accountId)
        {
            if (!_accountRepository.IsExists(accountId)) return new ResponseModel(404, "Not found");
            var accountToDelete = _accountRepository.GetById(accountId);
            if (!_accountRepository.Delete(accountToDelete))
            {
                return new ResponseModel(500, "Something went wrong when deleting account");
            }
            return new ResponseModel(204, "");
        }

        public Account? GetAccount(int accountId)
        {
            if (!_accountRepository.IsExists(accountId)) return null;
            var account = _accountRepository.GetById(accountId);
            return account;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAll();
        }

        public ResponseModel UpdateAccount(int accountId, Account updatedAccount)
        {
            if (!_accountRepository.IsExists(accountId)) return new ResponseModel(404, "Not found");
            if (!_accountRepository.IsExists(updatedAccount))
            {
                return new ResponseModel(404, "Username is not exist");
            }
            if (!_accountRepository.Update(updatedAccount))
            {
                return new ResponseModel(500, "Something went wrong updating account");
            }
            return new ResponseModel(204, "");
        }

        public Role GetRoleById(int roleId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseModel> ValidateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> ValidateUsernameAndPassword(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
