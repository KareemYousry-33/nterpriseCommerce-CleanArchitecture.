using EnterpriseCommerce.Application;
using EnterpriseCommerce.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. تسجيل الـ Controllers والـ Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. تسجيل طبقتي الـ Application والـ Infrastructure (ده اللي هيحل إيرور الـ DbContext!)
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

// 3. تشغيل واجهة Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();