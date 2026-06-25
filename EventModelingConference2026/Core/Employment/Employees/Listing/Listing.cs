// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Core.Employment.Employees.Hire;
using Cratis.Arc.MongoDB;
using MongoDB.Driver;

namespace Core.Employment.Employees.Listing;

[ReadModel]
public record Employee(EmployeeId Id, EmployeeName Name, Department Department)
{
    public static ISubject<IEnumerable<Employee>> ObserveAll(IMongoCollection<Employee> collection) =>
        collection.Observe();
}

public class EmployeeProjection : IProjectionFor<Employee>
{
    public void Define(IProjectionBuilderFor<Employee> builder) => builder
        .AutoMap()
        .From<EmployeeHired>();
}
