using System.Globalization;
using System.Security.Claims;
using System.Web.Http;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Services.Services;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountInfoController : ControllerBase
    {
        IAccountBusiness _AccountBusiness;
        IBankBusiness _BankBusiness;
        public AccountInfoController(IAccountBusiness AccountBusiness, IBankBusiness BankBusiness)
        {
            _AccountBusiness = AccountBusiness;
            _BankBusiness = BankBusiness;
        }
        [Route("InsertBankUser")]
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> InsertBankUser([FromBody] AccountCreateModel AccountCreate)
        {
            AccountInfoDataModel AccountInfo = new AccountInfoDataModel();
            var BankId = await _BankBusiness.FindBankId(AccountCreate.BankName);
            if (BankId == null)
                return NotFound("Bank not found");
            var CheckIfNameExists =await _AccountBusiness.CheckIfNameExists(AccountCreate.AccountName, AccountCreate.BankName);
            if(CheckIfNameExists)
                return NotFound("User Exists Already");
            AccountInfo.AccountName = AccountCreate.AccountName.Trim().ToUpper();
            var ConcatAccountID = AccountCreate.AccountName.ToString().Substring(0, 3) + DateTime.Now.ToShortDateString().Replace('/', ' ');
            AccountInfo.AccountId = string.Concat(ConcatAccountID.Where(c => !char.IsWhiteSpace(c)));
            AccountInfo.BankId = BankId;
            AccountInfo.AccountRole = "User";
            AccountInfo.AccountPassword = _AccountBusiness.GetRandomPassword(9);
            AccountInfo.AccountEmail = AccountCreate.AccountEmail;
            AccountInfo.IsActive = true;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            AccountInfo.CreatedBy = identity.Name;
            AccountInfo.Balance = 500;
            var message =await _AccountBusiness.InsertAccountDetails(AccountInfo);
            if (!string.IsNullOrEmpty(message))
            {
                return Ok("Account Succesfully created for user");
            }
            else
            {
                return BadRequest();
            }

        }
       
       
        [Route("GetBankBalanceOfUser")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetBankBalanceOfUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var balance = _AccountBusiness.GetUsersBalance(email).ToString();
            decimal parsed = decimal.Parse(balance, CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            string text = string.Format(hindi, "{0:c}", parsed);
            return Ok(text);
        }
        [Route("WithdrawOrDepositMoney")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> WithdrawOrDepositMoney([FromUri]decimal Amount,bool IsWithdraw)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            if (IsWithdraw)
            {
                var balance =await _AccountBusiness.GetUsersBalance(email);
                if (balance > Amount)
                    return BadRequest("The Balance is less than the requested Amount");
               
            }
            var Account = await _AccountBusiness.GetAccountInfoByEmail(email);
            var result = _AccountBusiness.WithdrawOrDepositMoney(Account, Amount, IsWithdraw);
            return Ok(result);
        }
        [Route("Transaction")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Transaction([FromUri] decimal Amount, bool toSameBank,string? BankName,bool IsRTGS,string? AccountName)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            if (!toSameBank)
            {
                if (!IsRTGS)
                {
                    var balance = await _AccountBusiness.GetUsersBalance(email);
                    if (balance < Amount + ((Amount * 6) / 100))
                        return BadRequest("The Balance is less than the requested Amount");
                  
                }

            }
           
            var Account = await _AccountBusiness.GetAccountInfoByEmail(email);
            var result = _AccountBusiness.Transaction(Account, Amount, IsRTGS,toSameBank,BankName,AccountName);
            return Ok(result);
        }

    }
}
