using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Service.IService;
using System;

namespace ProductApplication.Service.Service
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly ApplicationDbContext _context;

        public AppSettingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<bool> GetUseApiFlagAsync()
        {
            var setting = await _context.AppSettings.FirstOrDefaultAsync();
            return setting.UseApi;
        }
        
    }
}
