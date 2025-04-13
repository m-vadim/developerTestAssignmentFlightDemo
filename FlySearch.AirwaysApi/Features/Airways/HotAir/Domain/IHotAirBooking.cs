namespace FlySearch.AirwaysApi.Airways.HotAir.Domain;

public interface IHotAirBooking {
	bool IsBooked(string flightNumber, string seatNumber);
	bool Book(string flightNumber, string seatNumber, string userName);
	IEnumerable<Seat> GetAvailableSeats(string flightNumber);
}
