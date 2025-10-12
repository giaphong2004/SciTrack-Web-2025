﻿using Microsoft.EntityFrameworkCore;
using SciTrack.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình kết nối Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<KHCN_DBContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Cấu hình Controller và NewtonsoftJson để xử lý vòng lặp
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS dev: cho FE gọi khác port
builder.Services.AddCors(o => o.AddPolicy("allow-fe",
    p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("allow-fe");
app.MapControllers();

app.Run();