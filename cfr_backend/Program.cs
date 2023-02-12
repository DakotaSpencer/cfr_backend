using CFRDal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IManager, ApiManager>();
//builder.Services.AddSingleton<IManager, UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "movie",
    pattern: "movie/{id:int}",
    new { controller = "Api", action = "GetMovie" });
    
app.MapControllerRoute(
    name: "getreviews",
    pattern: "movie/{id:int}/reviews",
    new { controller = "Api", action = "GetReviewsForMovie" });

app.MapControllerRoute(
    name: "createUser",
    pattern: "createuser",
    new { controller = "User", action = "CreateUser" });

app.MapControllerRoute(
    name: "deleteUser",
    pattern: "deleteuser/{id}",
    new { controller = "User", action = "DeleteUser" });

app.MapControllerRoute(
    name: "createReview",
    pattern: "createreview",
    new { controller = "User", action = "CreateReview" });

app.MapControllerRoute(
    name: "login",
    pattern: "login",
    new { controller = "User", action = "Login" });

app.Run();
