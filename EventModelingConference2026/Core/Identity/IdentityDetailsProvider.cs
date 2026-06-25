// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Security.Claims;
using Cratis.Arc.Identity;

namespace Core.Identity;

public class IdentityDetailsProvider : IProvideIdentityDetails<IdentityDetails>
{
    public Task<Cratis.Arc.Identity.IdentityDetails> Provide(IdentityProviderContext context)
    {
        var name = Claim(context, "name") ?? Claim(context, ClaimTypes.Name) ?? context.Name.Value;
        var email = Claim(context, "preferred_username") ?? Claim(context, ClaimTypes.Email) ?? string.Empty;

        var details = new IdentityDetails(context.Id.Value, name, email);
        return Task.FromResult(new Cratis.Arc.Identity.IdentityDetails(true, details));
    }

    static string? Claim(IdentityProviderContext context, string type)
    {
        var value = context.Claims.FirstOrDefault(claim => claim.Key == type).Value;
        return string.IsNullOrEmpty(value) ? null : value;
    }
}
