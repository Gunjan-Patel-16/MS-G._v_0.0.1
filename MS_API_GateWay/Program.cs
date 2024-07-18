using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

# region Configure Ocelot.Json
// Configure Ocelot
builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
# endregion

var app = builder.Build();

//app.UseHttpsRedirection();
//app.UseAuthorization();
//app.MapControllers();
app.UseOcelot();

app.Run();
