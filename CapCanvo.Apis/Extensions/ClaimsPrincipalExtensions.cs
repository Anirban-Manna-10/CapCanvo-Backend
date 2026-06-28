using CapCanvo.Core.Common;
using CapCanvo.Core.Entities;
using System.Security.Claims;

namespace CapCanvo.Apis.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user) => user.FindFirst("sub")?.Value;

    //public static IdNameModel GetUser(this ClaimsPrincipal user)
    //{
    //    return new IdNameModel
    //    {
    //        Id = GetUserId(),
    //    };

    //}
}