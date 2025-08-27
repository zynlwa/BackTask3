using BackendProject.App.Data;

namespace BackendProject.App.Services
{
    public class LayoutService
        (PustokDbContext pustokDbContext)
    {
        private readonly PustokDbContext pustokDbContext = pustokDbContext;
        public Dictionary<string, string> GetSettings()
        {
            return pustokDbContext.Settings.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
