﻿using System;
using System.Threading.Tasks;
using HubServiceInterfaces;
using Microsoft.AspNetCore.SignalR.Client;

namespace Clients.ConsoleOne
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            connection.On<DateTime>(Strings.Events.TimeSent, (dateTime) =>
            {
                Console.WriteLine(dateTime.ToString());
            });

            // loop is here just to keep the worker running
            while (true)
            {
                try
                {
                    await connection.StartAsync();

                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }

            Console.WriteLine("Client One listening. Hit Ctrl-C to quit.");
            Console.ReadLine();
        }
    }
}