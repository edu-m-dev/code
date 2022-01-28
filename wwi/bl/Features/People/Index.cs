using AutoMapper.QueryableExtensions;
using wwi.bl.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace wwi.Features.People
{
    public class Index
    {
        public class Query : IRequest<Result>
        {
            public string SearchString { get; init; }
        }

        public class Result
        {
            public string SearchString { get; init; }
            public IReadOnlyCollection<Person> People { get; init; }
        }

        public class Person
        {
            public int PersonID { get; init; }
            public string FullName { get; init; }
            public string PreferredName { get; init; }
        }

        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly WwiDbContext _dbContext;
            private readonly IConfigurationProvider _configurationProvider;

            public QueryHandler(WwiDbContext dbContext, IConfigurationProvider configurationProvider)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
            }

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = new Result
                {
                    SearchString = query.SearchString
                };

                IQueryable<bl.EF.Person> people = _dbContext.People;
                if (!string.IsNullOrWhiteSpace(query.SearchString))
                {
                    people = people.Where(x => x.FullName.Contains(query.SearchString));
                }

                return new Result
                {
                    SearchString = query.SearchString,
                    People = await people
                        .ProjectTo<Person>(_configurationProvider)
                        .ToListAsync()
                };
            }
        }
    }
}