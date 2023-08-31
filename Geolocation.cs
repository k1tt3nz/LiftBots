namespace ConsoleApp1
{
	public class Geolocation
	{
		public double Latitude { get; set;}
		public double Longitude { get; set;}
		
		public Geolocation()
		{
			Latitude = default;
			Longitude = default;
		}
		public Geolocation(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}
