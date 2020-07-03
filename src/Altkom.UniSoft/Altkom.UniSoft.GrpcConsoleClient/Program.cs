using Altkom.UniSoft.GrpcService;
using Bogus;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.UniSoft.GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Client!");

            const string url = "https://localhost:5001";

            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Latitude, f => f.Random.Float(-90f, 90f))
                .RuleFor(p => p.Longitude, f => f.Random.Float(-10f, 10f))
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 120))
                .GenerateForever();

            var channel = GrpcChannel.ForAddress(url);

            var client = new Altkom.UniSoft.GrpcService.TrackingService.TrackingServiceClient(channel);

            // AddLocationRequest request = new AddLocationRequest { Latitude = 58.04f, Longitude = 21.05f, Speed = 50, Direction = 90 };

            foreach (var location in locations)
            {
                AddLocationResponse response = await client.AddLocationAsync(location);
                Console.WriteLine(response);

                await Task.Delay(TimeSpan.FromSeconds(0.01));
            }


        }
    }
}
