using System;
using Nancy;
using Nancy.Hosting.Self;
using System.IO;

namespace DirtWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
   
            var uri = new Uri ("http://localhost:1234");
            var hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;



            using (var host = new NancyHost(hostConfig, uri))
            {
                StaticConfiguration.DisableErrorTraces = false;
                host.Start();
                Console.WriteLine("Server started at " + uri.ToString());
                Console.WriteLine("Press \'q\' to close the server.");
                while (Console.Read() != 'q') ;
            }
		}

	}
}
