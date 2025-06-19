namespace markets.webapi;
internal interface ITickerService
{
    Task<IEnumerable<Ticker>> GetTickers();
}
