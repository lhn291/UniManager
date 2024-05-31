using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniManager.Application.Interfaces.Services;
using UniManager.Infrastructure.Data;

namespace UniManager.Infrastructure.Services
{
    public class IdGeneratorService : IIdGeneratorService
    {
        private readonly ApplicationDbContext _context;

        public IdGeneratorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateIdAsync<TEntity>(string prefix, Expression<Func<TEntity, string>> idSelector) where TEntity : class
        {
            string yearSuffix = DateTime.Now.ToString("yy");
            string lastYearInDatabase = "";

            bool hasEntities = await _context.Set<TEntity>().AnyAsync();

            if (hasEntities)
            {
                var lastStudentIdOfYear = await _context.Set<TEntity>()
                    .OrderByDescending(idSelector)
                    .Select(idSelector)
                    .FirstOrDefaultAsync();

                if (lastStudentIdOfYear != null)
                {
                    lastYearInDatabase = lastStudentIdOfYear.Substring(2, 2);
                }
            }

            if (yearSuffix != lastYearInDatabase)
            {
                return $"{prefix}{yearSuffix}001";
            }

            int nextSequenceNumber = 1;
            var lastStudentIdOfYearAndSequenceNumber = await _context.Set<TEntity>()
                .OrderByDescending(idSelector)
                .Select(idSelector)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(lastStudentIdOfYearAndSequenceNumber))
            {
                string sequenceNumberStr = lastStudentIdOfYearAndSequenceNumber[4..];
                if (int.TryParse(sequenceNumberStr, out int sequenceNumber))
                {
                    nextSequenceNumber = sequenceNumber + 1;
                }
            }

            string newStudentId = $"{prefix}{yearSuffix}{nextSequenceNumber:D3}";
            return newStudentId;
        }
    }
}
