using DbUp;
using System.Reflection;
using PreguntasYRespuestas.Data;
using PreguntasYRespuestas.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod()
.AllowAnyHeader().WithOrigins("http://localhost:3000").AllowCredentials()));

builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IQuestionCache, QuestionCache>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
EnsureDatabase.For.SqlDatabase(connectionString);
var upgrader = DeployChanges.To.SqlDatabase(connectionString, null)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly()).WithTransaction().Build();
if (upgrader.IsUpgradeRequired())
{
    upgrader.PerformUpgrade();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<QuestionsHub>("/questionshub");

app.UseCors("CorsPolicy");

app.Run();
