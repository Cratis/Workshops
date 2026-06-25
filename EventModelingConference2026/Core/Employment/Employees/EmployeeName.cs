// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Core.Employment.Employees;

public record EmployeeName(string Value) : ConceptAs<string>(Value)
{
    public static readonly EmployeeName NotSet = new(string.Empty);

    public static implicit operator EmployeeName(string value) => new(value);

    public static implicit operator string(EmployeeName name) => name.Value;
}
