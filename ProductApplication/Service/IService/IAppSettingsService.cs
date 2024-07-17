namespace ProductApplication.Service.IService
{
    public interface IAppSettingsService
    {
        Task<bool> GetUseApiFlagAsync();

    }
}
