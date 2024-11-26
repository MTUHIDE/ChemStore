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
        ChemstoreContext db;


        public AddRolesClaimsTransformation()
        {
            db = new ChemstoreContext();
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

            var user = (from u in db.User.Include(u => u.Role)
                        where u.Username == nameId
                        select u).FirstOrDefault();

            var role = new Claim(newIdentity.RoleClaimType, user.Role.Name);
            newIdentity.AddClaim(role);

            return clone;
        }
    }
}
