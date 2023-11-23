using HUG.CRUD.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartHRM.Repository;
using SmartHRM.Services.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartHRM.Services
{
    public class AccountService
    {
        private readonly AppSetting _appSettings;
        private readonly PBKDF2 _pbkdf2;
        private readonly AccountRepository _accountRepository;
        private readonly RoleRepository _roleRepository;
        private readonly RefeshTokenRepository _refeshTokenRepository;
        private readonly EmployeeRepository _employeeRepository;
        public AccountService(AccountRepository accountRepository,
                            RoleRepository roleRepository,
                            RefeshTokenRepository refeshTokenRepository,
                            IOptionsMonitor<AppSetting> optionsMonitor,
                            EmployeeRepository employeeRepository)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _refeshTokenRepository = refeshTokenRepository;
            _appSettings = optionsMonitor.CurrentValue;
            _employeeRepository = employeeRepository;
            _pbkdf2 = new PBKDF2();
        }

        public Role GetRoleById(int roleId)
        {
            if (!_roleRepository.IsExists(roleId)) return null;
            return _roleRepository.GetById(roleId);
        }
        public List<Role> GetRoles()
        {
            return _roleRepository.GetAll().ToList();
        }

        public Account GetAccount(int accountId)
        {
            if (!_accountRepository.IsExists(accountId)) return null;
            var account = _accountRepository.GetById(accountId);
            return account;
        }

        public IEnumerable<AccountDto> GetAccounts()
        {
            var accountsQuerry = from a in _accountRepository.GetAll()
                                 join r in _roleRepository.GetAll() on a.RoleId equals r.Id
                                 select new AccountDto
                                 {
                                     Id = a.Id,
                                     Username = a.Username,
                                     FullName = a.FullName,
                                     Avatar = a.Avatar,
                                     RoleId = a.RoleId,
                                     RoleName = r.Name,
                                     Email = a.Email,
                                     PhoneNumber = a.PhoneNumber,
                                     EmployeeId = a.EmployeeId,
                                     IsDeleted = a.IsDeleted
                                 };
            return accountsQuerry;
        }

        public AccountDto GetAccountById2(int accountId)
        {
            return GetAccounts().FirstOrDefault(x=>x.Id == accountId);
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

            accountCreate.Password = _pbkdf2.HashPassword(accountCreate.Password, out var salt);
            accountCreate.Salt = salt;
            accountCreate.IsDeleted = false;
            if (!_accountRepository.Create(accountCreate))
            {
                return new ResponseModel(500, "Something went wrong while saving");
            }

            return new ResponseModel(201, "Successfully created");

        }

        public ResponseModel UpdateAccount(int accountId, Account updatedAccount)
        {
            if (!_accountRepository.IsExists(accountId)) return new ResponseModel(404, "Not found");
            if (!_accountRepository.IsExists(updatedAccount))
            {
                return new ResponseModel(404, "Username is not exist");
            }
            updatedAccount.Password = _pbkdf2.HashPassword(updatedAccount.Password, out var salt);
            updatedAccount.Salt = salt;
            if (!_accountRepository.Update(updatedAccount))
            {
                return new ResponseModel(500, "Something went wrong updating account");
            }
            return new ResponseModel(204, "");

        }

        public ResponseModel UpdateAccountByAdmin(int accountId, Account updatedAccount)
        {
            if (!_accountRepository.IsExists(accountId)) return new ResponseModel(404, "Not found");

            var selectedAccount = _accountRepository.GetById(accountId);
            selectedAccount.RoleId = updatedAccount.RoleId;
            selectedAccount.FullName = updatedAccount.FullName;
            selectedAccount.Email = updatedAccount.Email;
            selectedAccount.PhoneNumber = updatedAccount.PhoneNumber;
            selectedAccount.EmployeeId = updatedAccount.EmployeeId;
            selectedAccount.Avatar = updatedAccount.Avatar;
            if (!_accountRepository.Update(selectedAccount))
            {
                return new ResponseModel(500, "Something went wrong updating account");
            }
            return new ResponseModel(204, "");

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

        public async Task<ResponseModel> ValidateUsernameAndPassword(Account account)
        {

            if (!_accountRepository.IsExists(account))
            {
                return new ResponseModel(404, "Username is not exist");
            }
            if (!this.VerifyPassword(account))
            {
                return new ResponseModel(404, "Password is not correct");
            }
            return new ResponseModel(
                200,
                "Account ok"
            );
        }

        public async Task<ResponseModel> ValidateAccount(Account account)
        {

            if (!_accountRepository.IsExists(account))
            {
                return new ResponseModel(404, "Username is not exist");
            }
            if (!this.VerifyPassword(account))
            {
                return new ResponseModel(404, "Password is not correct");
            }
            var token = await GenerateToken(account);
            return new ResponseModel(
                200,
                "Login successfully",
                token
            );
        }

        private bool VerifyPassword(Account account)
        {
            try
            {
                var selectedAccount = _accountRepository.GetAccount(account);
                if (!_pbkdf2.VerifyPassword(account.Password, selectedAccount.Password, selectedAccount.Salt))
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


        }

        private async Task<TokenModel> GenerateToken(Account account)
        {
            var accountId = _accountRepository.GetAccount(account).Id;
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                    new Claim("Username" , account.Username),
                    new Claim("Id" , accountId.ToString()),

                    //roles
                }),

                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accsessToken = jwtTokenHandler.WriteToken(token);
            var refeshToken = GenerateRefeshToken();

            //Save in db
            var refeshTokenCreate = new RefeshToken
            {
                JwtId = token.Id,
                AccountId = accountId,
                Token = refeshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddHours(1)
            };
            await _refeshTokenRepository.Create_Async(refeshTokenCreate);

            return new TokenModel
            {
                AccessToken = accsessToken,
                RefeshToken = refeshToken
            };
        }

        private string GenerateRefeshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<ResponseModel> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false //ko ktra token het han

            };

            try
            {
                //Check 1: accessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken,
                    tokenValidateParam, out var validatedToken);

                //check 2: check algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg
                                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (!result)
                    {
                        return new ResponseModel(500, "Invalid token");
                    }
                }

                //check 3: check accessToken expire?
                var utcExpireDate =
                    long.Parse(tokenInVerification.Claims.FirstOrDefault(x =>
                        x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new ResponseModel(200, "Access token has not yet expired", model);
                }

                //check 4: Check refeshtoken exists in db
                if (!_refeshTokenRepository.IsExists(model.RefeshToken))
                {
                    return new ResponseModel(500, "Refesh token does not exist");
                }

                //check 5: Check refeshtoken is used/revoked?
                if (!_refeshTokenRepository.IsValid(model.RefeshToken))
                {
                    return new ResponseModel(500, "Refesh token has been used or revoked");
                }

                //check 6: Access Token Id == jwtId in RefeshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x =>
                    x.Type == JwtRegisteredClaimNames.Jti).Value;

                if (_refeshTokenRepository.GetRefeshToken(model.RefeshToken).JwtId != jti)
                {
                    return new ResponseModel(500, "Token does not match");
                }

                //Update token 
                var currentRefeshToken = _refeshTokenRepository.GetRefeshToken(model.RefeshToken);
                currentRefeshToken.IsRevoked = true;
                currentRefeshToken.IsUsed = true;
                _refeshTokenRepository.Update(currentRefeshToken);

                //Create new token
                var account = await _accountRepository.GetById_Async(currentRefeshToken.AccountId);
                var token = await GenerateToken(account);
                return new ResponseModel(
                    200,
                    "Renew Token successfully",
                    token
                );

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseModel(500, "Something went wrong");
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;

        }

        public object GetAccountInfoById(int accountId)
        {
            var selectedAccount = _accountRepository.GetById(accountId);
            var infor = new
            {
                Id = accountId,
                Username = selectedAccount.Username,
                FullName = selectedAccount.FullName,
                Avatar = selectedAccount.Avatar,
                RoleId = selectedAccount.RoleId,
                RoleName = _roleRepository.GetById(selectedAccount.RoleId).Name,
                Email = selectedAccount.Email,
                PhoneNumber = selectedAccount.PhoneNumber,
                EmployeeId = selectedAccount.EmployeeId
            };
            return infor;
        }

        public ResponseModel UpdateDeleteStatus(int AccountId, bool status)
        {
            if (!_accountRepository.IsExists(AccountId)) return new ResponseModel(404, "Not found");
            var updatedAccount = _accountRepository.GetById(AccountId);
            updatedAccount.IsDeleted = status;
            if (!_accountRepository.Update(updatedAccount))
            {
                return new ResponseModel(500, "Something went wrong when change delete status Role");
            }
            return new ResponseModel(204, "");
        }

    }


}
