// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentValidation;

namespace Core.Employment.Employees.Hire;

[Command]
public record HireEmployee(EmployeeName Name, Department Department)
{
    public (EmployeeId, EmployeeHired) Handle()
    {
        var employeeId = EmployeeId.New();
        return (employeeId, new(Name, Department));
    }
}

[EventType]
public record EmployeeHired(EmployeeName Name, Department Department);

public class HireEmployeeValidator : CommandValidator<HireEmployee>
{
    public HireEmployeeValidator()
    {
        RuleFor(command => command.Name).NotNull().WithMessage("Employee name must be provided");
        RuleFor(command => command.Name.Value)
            .NotEmpty()
            .When(command => command.Name is not null)
            .WithMessage("Employee name must be provided");

        RuleFor(command => command.Department).NotNull().WithMessage("Department must be provided");
        RuleFor(command => command.Department.Value)
            .NotEmpty()
            .When(command => command.Department is not null)
            .WithMessage("Department must be provided");
    }
}

public class UniqueEmployeeName : IConstraint
{
    public void Define(IConstraintBuilder builder) => builder
        .Unique(_ => _
            .On<EmployeeHired>(e => e.Name)
            .WithMessage("An employee with this name has already been hired"));
}
