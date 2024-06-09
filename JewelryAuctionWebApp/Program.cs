var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

    pattern: "{controller=Customers}/{action=Index}/{id?}");

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
