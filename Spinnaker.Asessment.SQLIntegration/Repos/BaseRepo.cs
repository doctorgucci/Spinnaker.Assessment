using Spinnaker.Asessment.SQLIntegration.Base;

namespace Spinnaker.Asessment.SQLIntegration.Repos
{
    public class BaseRepo : IDisposable
    {
        protected readonly CustomerDbContext _context;
        public BaseRepo(
            CustomerDbContext context)
        { _context = context; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
