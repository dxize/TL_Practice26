using Domain.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<BookingDbContext>( options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString( "DefaultConnection" ),
        b => b.MigrationsAssembly( "Infrastructure.Migrations" ) );
} );

builder.Services.AddScoped<IPropertyRepository, EFPropertyRepository>();
builder.Services.AddScoped<IRoomTypeRepository, EFRoomTypeRepository>();
builder.Services.AddScoped<IReservationRepository, EFReservationRepository>();

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
