using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using DBLayer.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace Services.Services.Class
{
    public class AuthenticateBusiness:IAuthenticateBusiness
    {

        private UnitOfWork<BankDatabaseContext> unitOfWork = new UnitOfWork<BankDatabaseContext>();
        //private GenericRepository<AccountInfo> repository;
        private IBankRepository bankRepo;
        public string _key;
        public AuthenticateBusiness(string key)
        {
            //  If you want to use Generic Repository with Unit of work
            //repository = new GenericRepository<AccountInfo>(unitOfWork);
            //If you want to use Specific Repository with Unit of work
            bankRepo = new BankRepository(unitOfWork);
            this._key = key;
        }

        public UserCredential VerifyUser(string UserName, string Password)
        {
            var context = new BankDatabaseContext();
            var result = context.AccountInfos.FirstOrDefault(r => r.AccountName == UserName && r.AccountPassword == Password);
            UserCredential user = new UserCredential();
            user.UserName = result.AccountName;
            user.Password = result.AccountPassword;
            return user;

        }
        public string Authentication(string username, string password)
        {
            var Credentials = bankRepo.VerifyUser(username, password);

            if (!(username.Equals(Credentials.UserName) || password.Equals(Credentials.Password)))
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, Credentials.UserName),
                        new Claim(ClaimTypes.Role, Credentials.Role),
                        new Claim(ClaimTypes.Email, Credentials.Email)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<AccountInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountInfo GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(AccountInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountInfo obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(AccountInfo obj)
        {
            throw new NotImplementedException();
        }
    }
}
