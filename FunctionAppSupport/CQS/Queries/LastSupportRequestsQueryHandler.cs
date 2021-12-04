using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FunctionAppSupport.Data;

namespace FunctionAppSupport.CQS.Queries
{
    public class LastSupportRequestsQueryHandler :
        IRequestHandler<LastSupportRequestsQuery, IEnumerable<LastSupportRequestsQueryResult>>
    {
        private readonly SupportContext _context;

        public LastSupportRequestsQueryHandler(SupportContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<LastSupportRequestsQueryResult>> Handle(LastSupportRequestsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.SupportRequests.OrderByDescending(r => r.Id)
                .Take(request.NumberLastSupportRequests)
                .Select(s => new LastSupportRequestsQueryResult()
                {
                    Id = s.Id,
                    RequestDate = s.RequestDate,
                    Email = s.Email,
                    Problem = s.Problem
                }).AsEnumerable());
        }
    }
}