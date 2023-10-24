using System;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:2700/chatHub")
            .Build();

        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        connection.On<int>("ChatDeleted", (chatId) =>
        {
            Console.WriteLine($"Chat with ID {chatId} has been deleted.");
        });

        connection.Closed += async (error) =>
        {
            Console.WriteLine($"Connection closed due to error: {error}");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected. Press any key to exit...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }

        Console.ReadKey();
    }
}
