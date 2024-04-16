using Microsoft.AspNetCore.OutputCaching;
using PuppeteerSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOutputCache();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        x.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseOutputCache();

app.MapGet("/photos/{hashtag}", async (string hashtag) =>
    {
        var urls = await GetImageUrls(hashtag).ToListAsync();
        return urls;
    })
    .WithName("GetImageUrls")
    .CacheOutput(x => x.Expire(TimeSpan.FromMinutes(1)))
    .WithOpenApi();

app.Run();
return;

async IAsyncEnumerable<string> GetImageUrls(string hashtag)
{
    await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    await using var page = await browser.NewPageAsync();
    await page.GoToAsync($"https://www.instagram.com/explore/tags/{hashtag}/", WaitUntilNavigation.Networkidle0);
    await page.WaitForTimeoutAsync(5000);

    var images = await page.XPathAsync("//img");
    foreach (var image in images)
    {
        var src = await image.EvaluateFunctionAsync<string>("e => e.src");
        if (!src.Contains("http"))
        {
            continue;
        }
        yield return src;
    }
}