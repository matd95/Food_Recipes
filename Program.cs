using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Food_Recipes.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    policy.RequireRole("Admin"));
});
// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Recipes");
    options.Conventions.AllowAnonymousToPage("/Recipes/Index");
    options.Conventions.AllowAnonymousToPage("/Recipes/Details");
    options.Conventions.AuthorizeFolder("/Ingredients", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categories", "AdminPolicy");
});
builder.Services.AddDbContext<Food_RecipesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Food_RecipesContext") ?? throw new InvalidOperationException("Connection string 'Food_RecipesContext' not found.")));

builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Food_RecipesContext") ?? throw new InvalidOperationException("Connection string 'Food_RecipesContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
