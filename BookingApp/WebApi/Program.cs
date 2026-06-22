using Application.Services;
using Domain.Repositories;
using Infrastructure.Foundation.Database;
using Infrastructure.Foundation.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<BookingDbContext>( options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString( "DefaultConnection" ),
        b => b.MigrationsAssembly( "Infrastructure.Migrations" ) );
} );

builder.Services.AddScoped<IPropertyRepository, EfPropertyRepository>();
builder.Services.AddScoped<IRoomTypeRepository, EfRoomTypeRepository>();
builder.Services.AddScoped<IReservationRepository, EfReservationRepository>();

builder.Services.AddScoped<PropertyService>();
builder.Services.AddScoped<RoomTypeService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<SearchService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
