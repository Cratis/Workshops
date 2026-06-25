// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Core.Employment.Employees;

public record Department(string Value) : ConceptAs<string>(Value)
{
    public static readonly Department NotSet = new(string.Empty);

    public static implicit operator Department(string value) => new(value);

    public static implicit operator string(Department department) => department.Value;
}
