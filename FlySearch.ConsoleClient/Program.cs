// See https://aka.ms/new-console-template for more information

using FlySearch.ConsoleClient;

const string BaseAddress = "http://localhost:5283";

Console.WriteLine("Press Enter to start...");
Console.WriteLine("Press any key to exit...");



while (true) {
	var key = Console.ReadKey(true).Key;// != ConsoleKey.Ente
	if (key == ConsoleKey.Escape) {
		break;
	}

	if (key == ConsoleKey.Enter) {
		Console.WriteLine("Operation started...");
		var demo = new Demo(BaseAddress);
		await demo.RunDemoAsync();
		break;

	}
}
Console.ReadKey();

