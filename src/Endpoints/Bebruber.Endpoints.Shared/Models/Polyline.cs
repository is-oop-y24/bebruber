using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bebruber.Endpoints.Shared.Models
{
    public class Polyline
    {
        private List<MapPoint> _points;
        private FisSst.BlazorMaps.Polyline _polyline;

        public IReadOnlyList<MapPoint> Points => _points.AsReadOnly();

        public Polyline(ICollection<MapPoint> points, FisSst.BlazorMaps.Polyline polyline)
        {
            _points = points.ToList();
            _polyline = polyline;
        }

        public async Task DeleteAsync()
        {
            await _polyline.Remove();
        }
    }
}