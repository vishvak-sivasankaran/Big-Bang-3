using Kanini_Tourism.Data;
using Kanini_Tourism.Repository.Interface;
using Kanini_Tourism.Repository.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IGallery, ImageService>();
builder.Services.AddScoped<IHotel, HotelService>();
builder.Services.AddScoped<ITour, TourService>();
builder.Services.AddScoped<IBook,BookingService>();
builder.Services.AddScoped<IRestaurent,RestaurentService>();
builder.Services.AddScoped<ISpot, SpotService>();
builder.Services.AddScoped<IFeedback, FeedbackService>();


builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
builder.Services.AddDbContext<TourDBContext>(optionsAction: options => options.UseSqlServer(builder.Configuration.GetConnectionString(name: "SQLConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
