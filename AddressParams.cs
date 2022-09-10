using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure_inquiry
{
    public class AddressParams
    {
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int RegionId { get; set; }
        public int NeighbourhoodId { get; set; }
        public int StreetId { get; set; }
        public int BuildingId { get; set; }
        public int ApartmentId { get; set; }
    }
}
