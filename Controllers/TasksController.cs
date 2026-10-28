using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewTask.Controllers;

public class TasksController : Controller
{
    private static readonly IReadOnlyList<string> SuperMarketNames =
        new[] { "WellMart", "Silpo", "ATB", "Furshet", "Metro" };

    private static Dictionary<string, int> CreateShoppingList()
    {
        return new Dictionary<string, int>
        {
            ["Milk"] = 2,
            ["Bread"] = 2,
            ["Cake"] = 1,
            ["Ice Cream"] = 5,
            ["Cola"] = 10
        };
    }

    public IActionResult SprintTasks()
    {
        return View();
    }

    public IActionResult Greetings()
    {
        return View();
    }

    public IActionResult ProductInfo()
    {
        return View();
    }

    public IActionResult SuperMarkets()
    {
        ViewBag.SuperMarkets = SuperMarketNames;
        return View();
    }

    public IActionResult ShoppingList()
    {
        return View(CreateShoppingList());
    }

    [HttpGet]
    public IActionResult ShoppingCart()
    {
        PopulateShoppingCartOptions();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ShoppingCart(
        string fullName,
        string address,
        string supermarket,
        DateTime shippingDate,
        string[] products)
    {
        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address))
        {
            ViewBag.ErrorMessage = "Full name and address are required.";
            PopulateShoppingCartOptions();
            return View();
        }

        return Content(
            $"Your products will be shipped at: {address}. Bon appetit, {fullName}!"
        );
    }

    private void PopulateShoppingCartOptions()
    {
        ViewBag.SuperMarkets = new SelectList(SuperMarketNames);
        ViewBag.ShippingDates = Enumerable.Range(1, 3)
            .Select(days => DateTime.Today.AddDays(days))
            .ToArray();
        ViewBag.Products = new MultiSelectList(CreateShoppingList().Keys);
    }
}
