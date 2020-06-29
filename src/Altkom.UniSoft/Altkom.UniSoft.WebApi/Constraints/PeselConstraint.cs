using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.WebApi.Constraints
{
    // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-3.1#route-constraint-reference
    // https://github.com/sulmar/dotnet-core-routecontraint-polish-validators/blob/master/webapi/ConstraintExtensions.cs
    public class PeselConstraint : IActionConstraint
    {
        public int Order => throw new NotImplementedException();

        public bool Accept(ActionConstraintContext context)
        {
            return true;
        }
    }
}
