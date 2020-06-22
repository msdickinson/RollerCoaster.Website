using DickinsonBros.Telemetry.Abstractions;
using DickinsonBros.Telemetry.Abstractions.Models;
using System.Threading.Tasks;

namespace RollerCoaster.Website.Infrastructure.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        public async Task FlushAsync()
        {
            await Task.CompletedTask;
        }

        public void Insert(TelemetryData telemetryData)
        {

        }
    }
}
