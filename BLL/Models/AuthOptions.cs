using Microsoft.IdentityModel.Tokens;
using System.Text;
 
namespace BLL.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "InvLunchBackEnd";
        public const string AUDIENCE = "InvLunchFrontEnd";
        const string KEY = "nuichtosudapisatznakvoprosa"; 
        public const int LIFETIME = 60; //Не знаю, какое время жизни ставить
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}