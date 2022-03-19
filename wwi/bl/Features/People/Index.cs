using AutoMapper.QueryableExtensions;
using wwi.bl.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LazyCache;

namespace wwi.bl.Features.People
{
    public class Index
    {
        public class Query : IRequest<Result>
        {
            public string? SearchString { get; init; }
        }

        public class Result
        {
            public string? SearchString { get; init; }
            public IReadOnlyCollection<Person> People { get; init; } = new List<Person>();
        }

        public class Person
        {
            public int PersonID { get; init; }
            public string? FullName { get; init; }
            public string? PreferredName { get; init; }
        }

        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly IConfigurationProvider _configurationProvider;
            private readonly IDbContextFactory<WwiDbContext> _dbContextFactory;
            private readonly IAppCache _appCache;

            public QueryHandler(IConfigurationProvider configurationProvider, IDbContextFactory<WwiDbContext> dbContextFactory, IAppCache appCache)
            {
                _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
                _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
                this._appCache = appCache ?? throw new ArgumentNullException(nameof(appCache));
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                Task<Result> getter() => _Handle(query, cancellationToken);
                var key = $"People.Index.{query.SearchString}";
                return await _appCache.GetOrAddAsync(key, getter);
            }

            private async Task<Result> _Handle(Query query, CancellationToken cancellationToken)
            {
                var result = new Result
                {
                    SearchString = query.SearchString
                };

                using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

                IQueryable<bl.EF.Person> people = dbContext.People;
                if (!string.IsNullOrWhiteSpace(query.SearchString))
                {
                    people = people.Where(x => x.FullName.Contains(query.SearchString));
                }

                return new Result
                {
                    SearchString = query.SearchString,
                    People = await people
                        .ProjectTo<Person>(_configurationProvider)
                        .ToListAsync(cancellationToken)
                };
            }
        }
    }
}
