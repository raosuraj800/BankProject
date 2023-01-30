using System.Data;
using System.Security.Claims;
using DBLayer.DataModel;
using DBLayer.Models;
using DBLayer.Repository;
using DBLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BankController : ControllerBase
    {
        IBankBusiness _bankBusiness;
        public BankController(IBankBusiness bankBusiness)
        {
            _bankBusiness = bankBusiness;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("InsertBankDetails")]
        public IActionResult InsertBankDetails([FromBody]string BankName)
        {
            
            var dto = new BankDataModel();
            dto.BankName = BankName.Trim().ToString().ToUpper();
            var ConcatBankID = dto.BankName.ToString().Substring(0,3) + DateTime.Now.ToShortDateString().Replace('/',' ');
            dto.BankId = string.Concat(ConcatBankID.Where(c => !char.IsWhiteSpace(c)));
            dto.IsActive = true;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            dto.CreatedBy = identity.Name;
            var message =  _bankBusiness.InsertBankDetails(dto);
            return Ok(message);
        }
        //[Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("GetAllBank")]
        public IActionResult GetAllBank()
        {
            var Response = _bankBusiness.GetAllBank();
            if(Response.Count<=0)
                return NotFound();

            return Ok(Response);
        }
    }
}
