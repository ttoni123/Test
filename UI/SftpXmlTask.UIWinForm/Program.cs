using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SftpXmlTask.Client;

namespace SftpXmlTask.UIWinForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<IClientDefinition, ClientDefinition>(client =>
                    {
                        client.BaseAddress = new Uri("https://localhost:7166/");
                    });
                    services.AddTransient<Form1>();
                }).Build();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(host.Services.GetRequiredService<IClientDefinition>()));
        }
    }
}