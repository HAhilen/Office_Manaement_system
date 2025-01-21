using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employees", async (ApplicationDbContext db) =>
{
    var employees = await db.Employees.ToListAsync();

    var employeeViewModels = employees.Select(e => new EmployeeViewModel
    {
        Id = e.Id,
        Name = e.Name,
        Department = e.Department,
        Email = e.Email,
        OrganizationId= e.OrganizationId
    }).ToList();

    return Results.Ok(employeeViewModels);
});


app.MapGet("/employees/{id}", async (int id, ApplicationDbContext db) =>
{
    var employee = await db.Employees.FindAsync(id);
    if (employee is null) return Results.NotFound();
    var employeeViewModel = new EmployeeViewModel
    {
        Id = employee.Id,
        Name = employee.Name,
        Department = employee.Department,
        Email = employee.Email,
        OrganizationId = employee.OrganizationId
    };

    return Results.Ok(employeeViewModel);
});


app.MapPost("/employees", async (EmployeeViewModel employeeViewModel, ApplicationDbContext db) =>
{
    var employee = new Employee
    {
        Name = employeeViewModel.Name,
        Department = employeeViewModel.Department,
        Email = employeeViewModel.Email,
        OrganizationId = employeeViewModel.OrganizationId
    };

    db.Employees.Add(employee);
    await db.SaveChangesAsync();
    var createdEmployee = new EmployeeViewModel
    {
        Id = employee.Id,
        Name = employee.Name,
        Department = employee.Department,
        Email = employee.Email,
        OrganizationId = employee.OrganizationId
    };

    return Results.Created($"/employees/{createdEmployee.Id}", createdEmployee);
});


app.MapPut("/employees/{id}", async (int id, EmployeeViewModel employeeViewModel, ApplicationDbContext db) =>
{
    var existingEmployee = await db.Employees.FindAsync(id);
    if (existingEmployee is null) return Results.NotFound();

   
    existingEmployee.Name = employeeViewModel.Name;
    existingEmployee.Department = employeeViewModel.Department;
    existingEmployee.Email = employeeViewModel.Email;
    existingEmployee.OrganizationId = employeeViewModel.OrganizationId;

    db.Entry(existingEmployee).State = EntityState.Modified;
    await db.SaveChangesAsync();

    return Results.Ok(existingEmployee);
});


app.MapDelete("/employees/{id}", async (int id, ApplicationDbContext db) =>
{
    var employee = await db.Employees.FindAsync(id);
    if (employee is null) return Results.NotFound();

    db.Employees.Remove(employee);
    await db.SaveChangesAsync();

    return Results.Ok(employee);
});

app.Run();
