namespace FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

public interface IRoyalAirBooking {
	bool IsBooked(string flightNumber, string seatNumber);
	bool Book(string flightNumber, string seatNumber, string userName);
	IEnumerable<Seat> GetAvailableSeats(string flightNumber);
}
