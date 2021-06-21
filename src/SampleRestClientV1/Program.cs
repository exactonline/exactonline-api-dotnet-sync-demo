using System;

namespace SampleRestClientV1
{
	/// <summary>
	/// Starting point of this sample
	/// </summary>
	class Program
	{
		static int Main(string[] args)
		{
			Console.WriteLine("========================");
			Console.WriteLine("SampleRestClientV1");
			Console.WriteLine("========================");
			Console.WriteLine("Retrieves CRM Accounts using sync apis");
			Console.WriteLine("and puts the data in database.");
			Console.WriteLine(string.Empty);

			var job = new Job();
			job.Execute();

			Console.ReadKey();

			return 0;
		}
	}
}
