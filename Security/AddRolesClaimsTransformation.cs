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

            /*
            // Orginal
            var user = (from a in db.Account
                    where a.Email == nameId
                    select a).FirstOrDefault();
            */

            var user = (from a in db.User
                        where a.Username == nameId
                        select a).FirstOrDefault();

            /*
            // Orginal?
            // for (int i = user?.Role ?? 3; i <= 3; i++) // Orginal
            {
                // var role = new Claim(newIdentity.RoleClaimType, ((Roles)i).ToString()); // Orginal
                var role = new Claim(newIdentity.RoleClaimType, (i).ToString());

                newIdentity.AddClaim(role);
            }
            */

            // Does this work? I don't know if the loop needed to continue or not now that everyone must have a role, when the old had role as nullable. Can we have multiple roles in the new schema?
            var role = new Claim(newIdentity.RoleClaimType, user.Role.Name);
            newIdentity.AddClaim(role);

            return clone;
        }
    }
}
