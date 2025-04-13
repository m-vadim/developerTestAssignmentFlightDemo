using System.Collections.Concurrent;

namespace FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

public sealed class RoyalAirBooking : IRoyalAirBooking {
	private static readonly IReadOnlyDictionary<string, Seat[]> InitialSeats = GetInitialSeats();
	private readonly ConcurrentDictionary<string, string> _bookings = new();

	public bool IsBooked(string flightNumber, string seatNumber) {
		if (!InitialSeats.TryGetValue(flightNumber, out Seat[] seats) && seats != null && seats.Any(a => a.Number == seatNumber)) {
			throw new InvalidOperationException("Seat not found.");
		}

		return GetAvailableSeats(flightNumber).All(seat => seat.Number != seatNumber);
	}

	public bool Book(string flightNumber, string seatNumber, string userId) {
		if (!InitialSeats.TryGetValue(flightNumber, out Seat[] seats) && seats != null && seats.Any(a => a.Number == seatNumber)) {
			throw new InvalidOperationException("Seat not found.");
		}

		if (_bookings.ContainsKey(Key(flightNumber, seatNumber))) {
			throw new InvalidOperationException("Seat already booked.");
		}

		return _bookings.TryAdd(Key(flightNumber, seatNumber), userId);
	}

	public IEnumerable<Seat> GetAvailableSeats(string flightNumber) {
		if (!InitialSeats.TryGetValue(flightNumber, out Seat[]? seats)) {
			throw new ArgumentException($"Flight {flightNumber} not found.");
		}

		Seat[] initialSeats = InitialSeats[flightNumber];
		var available = new List<Seat>(initialSeats.Length);
		available.AddRange(initialSeats.Where(seat => !_bookings.ContainsKey(Key(flightNumber, seat.Number))));
		return available;
	}

	private static string Key(string flightNumber, string seatNumber) {
		return $"{flightNumber}:{seatNumber}";
	}

	private static Dictionary<string, Seat[]> GetInitialSeats() {
		return new Dictionary<string, Seat[]> {
			{
				"BA1234", [
					new Seat("Economy", false, "14A", 100),
					new Seat("Economy", false, "14B", 100),
					new Seat("Economy", true, "25C", 100),
					new Seat("Business", true, "2A", 300)
				]
			}, {
				"AA4567",
				[
					new Seat("Economy", false, "18D", 150),
					new Seat("Economy", true, "23F", 150),
					new Seat("First", true, "1A", 900)
				]
			}, {
				"DL7890",
				[
					new Seat("Economy", false, "32B", 300),
					new Seat("Economy", false, "32C", 300),
					new Seat("Comfort+", true, "12D", 400)
				]
			}, {
				"UA2468",
				[
					new Seat("Economy", false, "19A", 100),
					new Seat("Economy", false, "19B", 100),
					new Seat("Business", true, "5C", 300)
				]
			}, {
				"SQ2187",
				[
					new Seat("Economy", false, "38A", 200),
					new Seat("Economy", false, "38B", 200),
					new Seat("Premium Economy", true, "22D", 300),
					new Seat("Business", true, "15K", 500)
				]
			}, {
				"QF8523",
				[
					new Seat("Economy", false, "47C", 200),
					new Seat("Premium Economy", true, "31A", 300),
					new Seat("Business", true, "9J", 700),
					new Seat("First", true, "3F", 1000)
				]
			}
		};
	}
}
