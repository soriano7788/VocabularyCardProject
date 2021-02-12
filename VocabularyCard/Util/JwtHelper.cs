using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VocabularyCard.Util
{
    // 這個 helper 可以直接用 singleton
    public class JwtHelper
    {
        private readonly string _issuer = "me";
        private readonly string _secret = "abc";

        public JwtHelper(string issuer, string secret)
        {
            _issuer = issuer;
            _secret = secret;
        }

        // todo: 參數只先設一個 userId 未來是否會較不好擴充? 定義一個 JwtTokenParameter 之類的 class，假如以後要加資料，就去擴充 JwtTokenParameter 的 property
        public string GetToken(string userId,int expireMinutes = 30)
        {
            DateTime utcNow = DateTime.UtcNow;
            byte[] secretBytes = Encoding.UTF8.GetBytes(_secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(secretBytes);


            // 設定要加入到 JWT Token 中的聲明資訊(Claims)
            List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iss, ""));
            claims.Add(new Claim("uid", userId));
            ClaimsIdentity userClaimsIdentity = new ClaimsIdentity(claims);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Subject = userClaimsIdentity,
                Expires = utcNow.AddMinutes(expireMinutes),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string serizlizeToken = tokenHandler.WriteToken(securityToken);


            return serizlizeToken;
        }

        public bool ValidateToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var jwt = tokenHandler.ReadJwtToken(token);

                if (jwt == null)
                {
                    return false;
                }

                //var secretBytes = Convert.FromBase64String(_secret);
                byte[] secretBytes = Encoding.UTF8.GetBytes(_secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),

                    //LifetimeValidator = LifetimeValidator

                    // 這啥?
                    //ClockSkew = TimeSpan.Zero
                };

                SecurityToken securityToken;
                principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                //tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                //JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                //LogUtility.ErrorLog(JsonConvert.SerializeObject(principal, settings));

                return true;
            }
            catch (Exception e)
            {
                // 驗證失敗的原因會有多種 case，像是格式錯誤、token 過期、decode失敗 等等
                LogUtility.ErrorLog(e.ToString());
                //throw;
                return false;
            }
        }

    }
}
