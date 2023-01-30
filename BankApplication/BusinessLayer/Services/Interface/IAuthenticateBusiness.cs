using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer.Models;
using DBLayer.Repository;

namespace Services.Services
{
    public interface IAuthenticateBusiness
    { 
        string Authentication(string username, string password);
    
    }
}
