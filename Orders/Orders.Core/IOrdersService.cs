using Orders.Models;

namespace Orders.Core;

public interface IOrdersService
{
    int GetTotalProductsSold(IEnumerable<Order> orders);

    IEnumerable<string> GetDistinctSoldProductNames(IEnumerable<Order> orders);

    Product GetBestSellingProduct(IEnumerable<Order> orders);

    IDictionary<Product, int> GetProductAverageQuantity(IEnumerable<Order> orders);

    double GetTotalProfit(IEnumerable<Order> orders);

    Order GetMostExpensiveOrder(IEnumerable<Order> orders);

    IEnumerable<Product> GetTopNProductsByRevenue(IEnumerable<Order> orders, int n);

    IEnumerable<(string ProductName, int TotalSold)> GetProductsSoldBetweenDates(IEnumerable<Order> orders, DateTime start, DateTime end);
}
