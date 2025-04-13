namespace FlySearch.AirwaysApi.Airways.RoyalAir.Domain;

public sealed class Seat {
	public Seat(string @class, bool extraSpace, string number, decimal price) {
		Class = @class;
		ExtraSpace = extraSpace;
		Number = number;
		Price = price;
	}

	public decimal Price { get; set; }
	public string Class { get; }
	public bool ExtraSpace { get; }
	public string Number { get; }
}
