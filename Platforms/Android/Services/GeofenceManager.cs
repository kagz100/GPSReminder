using Android.Gms.Location;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms;

namespace GeoRemind.Platforms.Android.Services
{
    public class GeofenceManager
    {
    
    private readonly FusedLocationProviderClient _fusedLocationClient;
        private PendingIntent _geofencePendingIntent;

        public GeofenceManager()
        {
           _geofencingClient = LocationServices.GetGeofencingClient(Application.Context);
        }

        public void AddGeoFence()
        {
            var geoFencingRequest = new GeofencingRequest.Builder().AddGeofence(geofence).Build();


            var intent = new Intent(Android.App.Application.Context, typeof(GeofenceBroadcastReceiver));

            var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);

            LocationServices.GeofencingApi.AddGeofences(_googleApiClient, geoFencingRequest, pendingIntent).SetResultCallback(this);

        }


        public void CreateGeofence(double latitude, double longitude, float radius, string requestId)
        {
            var geofence = new GeofenceBuilder()
                .SetRequestId(requestId)
                .SetCircularRegion(latitude, longitude, radius)
                .SetExpirationDuration(Geofence.NeverExpire)
                .SetTransitionTypes(Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionExit)
                .Build();

            var geofencingRequest = new GeofencingRequest.Builder()
                .SetInitialTrigger(GeofencingRequest.InitialTriggerEnter)
                .AddGeofence(geofence)
                .Build();


            var intent = new Intent(Android.App.Application.Context, typeof(GeofenceBroadcastReceiver));


            _geofencePendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.UpdateCurrent);

            _geofencingClient.addGeofences(geofencingRequest, _geofencePendingIntent);

        }

        public void RemoveGeofence(string requestId)
        {
            var requestIds = new List<string> { requestId };
            _geofencingClient.RemoveGeofences(requestIds);
        }
    }

}
