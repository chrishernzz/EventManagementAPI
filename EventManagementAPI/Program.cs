
using Microsoft.EntityFrameworkCore;
using EventManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//registering the service here
//This way it keeps it running only when it sees it so one at a time: builder.Services.AddScoped<EventManagementAPI.Services.IEventService, EventManagementAPI.Services.EventService>();
//builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EventManagementAPI.Repositories.IEventRepository, EventManagementAPI.Repositories.EventRepository>();
builder.Services.AddScoped<EventManagementAPI.Services.IEventService, EventManagementAPI.Services.EventService>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
