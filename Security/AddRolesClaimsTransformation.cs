using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Security
{
    public class AddRolesClaimsTransformation : IClaimsTransformation
    {
        chemstoreContext db;


        public AddRolesClaimsTransformation()
        {
            db = new chemstoreContext();
        }


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            // Clone current identity
            var clone = principal.Clone();
            var newIdentity = (ClaimsIdentity)clone.Identity;

            var nameId = principal.Identity.Name;
            if (nameId == null)
            {
                return principal;
            }

            var user = (from a in db.Account
                        where a.Email == nameId
                        select a).FirstOrDefault();

            for (int i = user.Role ?? 3; i <= 3; i++)
            {
                var role = new Claim(newIdentity.RoleClaimType, ((Roles)i).ToString());
                newIdentity.AddClaim(role);
            }


            return clone;
        }
    }
}
