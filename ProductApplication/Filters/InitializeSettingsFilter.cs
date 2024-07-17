using Microsoft.AspNetCore.Mvc.Filters;
using ProductApplication.Service.IService;

namespace ProductApplication.Filters
{
    public class InitializeSettingsFilter : IAsyncActionFilter  
    {
       
        private readonly IAppSettingsService _appSettingsService;
        public InitializeSettingsFilter(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as ISettingsController;
            if (controller != null)
            {
                controller.UseApi = await _appSettingsService.GetUseApiFlagAsync();
            }
            await next();
        }
    }
    public interface ISettingsController
    {
        bool UseApi { get; set; }
    }
}
