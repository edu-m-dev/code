using Orders.Core;
using Orders.Models;
using Xunit;

namespace Orders.Tests;

public class OrdersServiceTests
{
    private IOrdersService _svc = new OrdersService();

    private static Order[] CreateSampleOrders()
    {
        return new[]
        {
            new Order(DateTime.Now, new [] { new OrderItem(new Product("A", 1.5), 2), new OrderItem(new Product("B", 2.0), 1) }),
            new Order(DateTime.Now, new [] { new OrderItem(new Product("A", 1.5), 3), new OrderItem(new Product("C", 0.5), 4) })
        };
    }

    [Fact]
    public void GetTotalProductsSold_BasicScenario()
    {
        // Arrange
        var orders = CreateSampleOrders();

        // Act
        var total = _svc.GetTotalProductsSold(orders);

        // Assert
        Assert.Equal(10, total);
    }

    [Fact]
    public void GetDistinctSoldProductNames_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var distinct = _svc.GetDistinctSoldProductNames(orders).ToList();

        Assert.Equal(new[] { "A", "B", "C" }, distinct);
    }

    [Fact]
    public void GetBestSellingProduct_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var best = _svc.GetBestSellingProduct(orders);

        Assert.Equal("A", best.Name);
    }

    [Fact]
    public void GetProductAverageQuantity_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var averages = _svc.GetProductAverageQuantity(orders);

        // find average for product A
        var avgForA = averages.Single(kv => kv.Key.Name == "A").Value;
        Assert.Equal(2.5, avgForA);
    }

    [Fact]
    public void GetTotalProfit_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var profit = _svc.GetTotalProfit(orders);

        Assert.Equal(2 * 1.5 + 1 * 2.0 + 3 * 1.5 + 4 * 0.5, profit);
    }

    [Fact]
    public void GetMostExpensiveOrder_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var mostExp = _svc.GetMostExpensiveOrder(orders);

        // second order is more expensive (3*1.5 + 4*0.5 = 4.5 + 2 = 6.5) vs first (2*1.5 + 1*2 = 3 + 2 = 5.0)
        Assert.Same(orders[1], mostExp);
    }

    [Fact]
    public void GetTopNProductsByRevenue_BasicScenario()
    {
        var orders = CreateSampleOrders();

        var top = _svc.GetTopNProductsByRevenue(orders, 2).ToList();

        Assert.Equal(2, top.Count);
        Assert.Equal("A", top[0].Name);
    }

    [Fact]
    public void GetProductsSoldBetweenDates_BasicScenario()
    {
        var orders = CreateSampleOrders();
        var start = DateTime.Today.AddDays(-1);
        var end = DateTime.Today.AddDays(1);

        var between = _svc.GetProductsSoldBetweenDates(orders, start, end).ToList();

        Assert.Contains(between, x => x.ProductName == "A" && x.TotalSold == 5);
    }
}
