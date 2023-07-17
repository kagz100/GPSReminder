using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoRemind.Models
{
    public class LocationManager
    {


        public async Task<Location> GetCurrentLocatoin()
        {
            try
            {
                //get current device location with default maxinmum accuracy

                var location = await Geolocation.GetLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

                return new Location(location.Latitude, location.Longitude);
            }

            catch (FeatureNotSupportedException fnsx)
            {
                // Handle not supported on device exception
                Console.WriteLine(fnsx.Message);
                return null;
            }
            catch (FeatureNotEnabledException fnex)
            {
                // Handle not enabled on device exception
                Console.WriteLine(fnex.Message);
                return null;
            }
            catch (PermissionException pex)
            {
                // Handle permission exception
                Console.WriteLine(pex.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
