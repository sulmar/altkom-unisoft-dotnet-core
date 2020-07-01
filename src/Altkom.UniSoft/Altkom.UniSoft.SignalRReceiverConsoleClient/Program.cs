using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.UniSoft.SignalRReceiverConsoleClient
{
    // dotnet add package Microsoft.AspNetCore.SignalR.Client
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/signalr/messages";

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Receiver!");

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            connection.On<Message>(nameof(IMessageClient.YouHaveGotMessage), 
                message => Console.WriteLine($"{message.Title} - {message.Content}"));

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }
    }
}
