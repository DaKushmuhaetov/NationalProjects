using Chuvashia.NationalProjects.Binding;
using Chuvashia.NationalProjects.Context;
using Chuvashia.NationalProjects.Model.News;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chuvashia.NationalProjects.View
{
    public sealed class NewsQueryHandler
    {
        private NationalProjectsDbContext _context;
        public NewsQueryHandler(NationalProjectsDbContext context)
        {
            _context = context;
        }

        public async Task<Page<NewsPost>> GetPage(GetNewsListBinding binding)
        {
            var newsQuery = _context.NewsPosts
                .AsNoTracking()
                .Where(o => o.IsArchived != true)
                .Where(o => o.Title == ) 

            #region Date filters

            if (binding.StartDate != null && binding.EndDate != null)
            {
                newsQuery = newsQuery.Where(o => o.CreateDate >= binding.StartDate && o.CreateDate <= binding.EndDate);
            }

            #endregion

            if (binding.Type.ToString() == NewsTypeBinding.All.ToString())
            {
                newsQuery = newsQuery.OrderByDescending(o => o.CreateDate);

                var items = await newsQuery
                    .Skip(binding.Offset)
                    .Take(binding.Limit)
                    .ToListAsync();

                return new Page<NewsPost>
                {
                    Limit = binding.Limit,
                    Offset = binding.Offset,
                    Total = await newsQuery.CountAsync(),
                    Items = items
                };
            }
            else
            {
                var type = Enum.Parse<NationalProjectType>(binding.Type.ToString());

                newsQuery = newsQuery
                    .Where(o => o.Type == type)
                    .OrderByDescending(o => o.CreateDate);

                var items = await newsQuery
                    .Skip(binding.Offset)
                    .Take(binding.Limit)
                    .ToListAsync();

                return new Page<NewsPost>
                {
                    Limit = binding.Limit,
                    Offset = binding.Offset,
                    Total = await newsQuery.CountAsync(),
                    Items = items
                };
            }
        }
    }
}
