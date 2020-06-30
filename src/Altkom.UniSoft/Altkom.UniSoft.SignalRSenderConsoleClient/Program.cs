using Altkom.UniSoft.Models;
using Bogus;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Altkom.UniSoft.SignalRSenderConsoleClient
{

    public class RandomRetryPolicy : IRetryPolicy
    {
        private readonly Random random = new Random();

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            if (retryContext.ElapsedTime < TimeSpan.FromSeconds(60))
            {
                return TimeSpan.FromSeconds(random.NextDouble() * 10);
            }
            else
            {
                return null;
            }
        }
    }

    // dotnet add package Microsoft.AspNetCore.SignalR.Client
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/signalr/messages";

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                //.WithAutomaticReconnect()
                //.WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(3), TimeSpan.FromMinutes(1) })
                .WithAutomaticReconnect(new RandomRetryPolicy())
                .Build();

            connection.Closed += error =>
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    // Powiadomienie uzytkownika
                }

                return Task.CompletedTask;
            };

            connection.Reconnected += connectionId =>
            {
                if (connection.State == HubConnectionState.Connected)
                {
                    // Przesladanych danych z bufora (kolejki)
                }

                return Task.CompletedTask;
            };

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            // dotnet add package Bogus

            Faker<Message> faker = new Faker<Message>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraph());

            // yield
            IEnumerable<Message> messages = faker.GenerateForever();

            foreach (Message message in messages)
            {
                await connection.SendAsync("SendMessage", message);

                Console.WriteLine($"Sent {message.Title} {message.Content}");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }
    }
}
