using Microsoft.Extensions.Options;
using serverproxy.Configurations;

namespace serverproxy.Services;
public class ProxyService
{
    private readonly string _origin;

    public ProxyService(IOptions<ProxySettings> settings)
    {
        _origin = settings.Value.Origin;
    }

    public string GetOrigin()
    {
        return _origin;
    }
}
