using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemStoreWebApp.Security
{
    public class RoleRequirementHandler : IAuthorizationHandler
    {
        chemstoreContext db;

        public RoleRequirementHandler()
        {
            db = new chemstoreContext();
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();
            var acceptedRoles = new List<Roles>();
            foreach (var requirement in pendingRequirements)
            {
                if (requirement is AdminRequirement)
                {
                    acceptedRoles.Add(Roles.Admin);
                }
                else if (requirement is EmployeeRequirement)
                {
                    acceptedRoles.Add(Roles.Admin);
                    acceptedRoles.Add(Roles.Employee);
                }
                else if (requirement is AssociateRequirement)
                {
                    acceptedRoles.Add(Roles.Admin);
                    acceptedRoles.Add(Roles.Employee);
                    acceptedRoles.Add(Roles.Associate);
                } 
                else
                {
                    continue;
                }

                Account? account = db.Account.Where(a => a.Email == context.User.Identity.Name).FirstOrDefault();
                if (acceptedRoles.Contains((Roles)(account.Role)))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }

    }

    public class AdminRequirement : IAuthorizationRequirement
    {

    }

    public class EmployeeRequirement : IAuthorizationRequirement
    {

    }

    public class AssociateRequirement : IAuthorizationRequirement
    {

    }
}
