using Flurl.Util;
using YahooFinanceApi;

namespace markets.webapi;
internal class TickerService : ITickerService
{

    public TickerService()
    {
        
    }
    public async Task<IEnumerable<Ticker>> GetTickers() {
        var securities = await Yahoo.Symbols("AAPL", "GOOG")
            .Fields(Field.Symbol, Field.RegularMarketPrice, Field.FiftyTwoWeekHigh)
            .QueryAsync();
        return securities.Select(x=>new Ticker(x.Key,x.Value.RegularMarketPrice));
    }
}
