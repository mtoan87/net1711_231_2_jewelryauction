var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Login}/{action=Login}/{id?}");

app.MapControllerRoute(
     name: "GetCustomerById",
     pattern: "{controller=Customers}/{action=GetCustomerById}/{customerId?}");

app.MapControllerRoute(
     name: "GetPaymentById",
     pattern: "{controller=Payments}/{action=GetPaymentById}/{paymentId?}");

app.MapControllerRoute(
     name: "GetJewelryById",
     pattern: "{controller=Jewelry}/{action=GetJewelryById}/{jewelryId?}");

app.MapControllerRoute(
     name: "GetAuctionResultById",
     pattern: "{controller=AuctionResult}/{action=GetAuctionResultById}/{auctionResultId?}");

app.Run();
