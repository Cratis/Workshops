// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if DEBUG
using Cratis.Arc.Tenancy;

namespace Core.Identity.Development;

public class Tenants : ICanProvideTenants
{
    public Task<IEnumerable<Tenant>> Provide() => Task.FromResult<IEnumerable<Tenant>>(
    [
        new Tenant("acme", "Acme"),
        new Tenant("contoso", "Contoso"),
    ]);
}
#endif
