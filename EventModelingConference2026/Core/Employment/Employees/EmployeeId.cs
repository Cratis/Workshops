// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Core.Employment.Employees;

public record EmployeeId(Guid Value) : ConceptAs<Guid>(Value)
{
    public static readonly EmployeeId NotSet = new(Guid.Empty);

    public static implicit operator EmployeeId(Guid value) => new(value);

    public static implicit operator Guid(EmployeeId id) => id.Value;

    public static implicit operator EventSourceId(EmployeeId id) => new(id.Value.ToString());

    public static EmployeeId New() => new(Guid.NewGuid());
}
