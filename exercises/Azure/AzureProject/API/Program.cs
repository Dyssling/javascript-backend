using API.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Education\\javascript-backend\\exercises\\Azure\\AzureProject\\API\\Data\\email_db.mdf;Integrated Security=True;Connect Timeout=30"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
