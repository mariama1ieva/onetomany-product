namespace slider.Services.Interface
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetALLAsync();
    }
}
