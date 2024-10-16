﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace VCardOnAbp.HttpApi.Client.ConsoleTestApp;

class Program
{
    static async Task Main(string[] args)
    {
        using IAbpApplicationWithInternalServiceProvider application = await AbpApplicationFactory.CreateAsync<VCardOnAbpConsoleApiClientModule>(options =>
        {
            ConfigurationBuilder builder = new();
            builder.AddJsonFile("appsettings.json", false);
            builder.AddJsonFile("appsettings.secrets.json", true);
            options.Services.ReplaceConfiguration(builder.Build());
            options.UseAutofac();
        });
        await application.InitializeAsync();

        ClientDemoService demo = application.ServiceProvider.GetRequiredService<ClientDemoService>();
        await demo.RunAsync();

        Console.WriteLine("Press ENTER to stop application...");
        Console.ReadLine();

        await application.ShutdownAsync();
    }
}
