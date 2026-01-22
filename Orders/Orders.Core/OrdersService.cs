using Orders.Models;

namespace Orders.Core;

public class OrdersService : IOrdersService
{
    public int GetTotalProductsSold(IEnumerable<Order> orders)
    {
        return orders
            .SelectMany(o => o.Items)
            .Sum(i => i.Quantity);
    }

    public IEnumerable<string> GetDistinctSoldProductNames(IEnumerable<Order> orders)
    {
        return orders
            .SelectMany(o => o.Items)
            .Select(i => i.Product.Name)
            .Distinct()
            .OrderBy(n => n);
    }

    public Product GetBestSellingProduct(IEnumerable<Order> orders)
    {
        var best = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product.Name)
            .Select(g => new { Name = g.Key, Total = g.Sum(i => i.Quantity), g.First().Product.Price })
            .OrderByDescending(x => x.Total)
            .ThenByDescending(x => x.Total * x.Price)
            .ThenBy(x => x.Name)
            .First();

        return new Product(best.Name, best.Price);
    }

    public IDictionary<Product, int> GetProductAverageQuantity(IEnumerable<Order> orders)
    {
        var grouped = orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product.Name)
            .ToDictionary(g => g.Key, g => new { Avg = (int)Math.Round(g.Average(i => i.Quantity), MidpointRounding.AwayFromZero), g.First().Product });

        return grouped.ToDictionary(kv => kv.Value.Product, kv => kv.Value.Avg);
    }

    public double GetTotalProfit(IEnumerable<Order> orders)
    {
        return orders
            .SelectMany(o => o.Items)
            .Sum(i => i.Quantity * i.Product.Price);
    }

    public Order GetMostExpensiveOrder(IEnumerable<Order> orders)
    {
        return orders
            .OrderByDescending(o => o.Items.Sum(i => i.Quantity * i.Product.Price))
            .First();
    }

    public IEnumerable<Product> GetTopNProductsByRevenue(IEnumerable<Order> orders, int n)
    {
        return orders
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product.Name)
            .Select(g => new Product(g.Key, g.First().Product.Price))
            .OrderByDescending(p =>
            {
                // compute revenue for this product name
                return orders.SelectMany(o => o.Items).Where(i => i.Product.Name == p.Name).Sum(i => i.Quantity * i.Product.Price);
            })
            .Take(n);
    }

    public IEnumerable<(string ProductName, int TotalSold)> GetProductsSoldBetweenDates(IEnumerable<Order> orders, DateTime start, DateTime end)
    {
        return orders
            .Where(o => o.Date >= start && o.Date <= end)
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Product.Name)
            .Select(g => (ProductName: g.Key, TotalSold: g.Sum(i => i.Quantity)))
            .OrderByDescending(x => x.TotalSold);
    }
}
