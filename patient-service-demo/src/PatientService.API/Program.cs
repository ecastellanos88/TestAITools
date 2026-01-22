using PatientService.Application.Patients.CreatePatient;
using PatientService.Application.Patients.GetPatient;
using PatientService.Application.Patients.GetAllPatients;
using PatientService.Application.Patients.UpdatePatient;
using PatientService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Register application services
builder.Services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
builder.Services.AddScoped<CreatePatientHandler>();
builder.Services.AddScoped<GetPatientHandler>();
builder.Services.AddScoped<GetAllPatientsHandler>();
builder.Services.AddScoped<UpdatePatientHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
