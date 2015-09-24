using System;
using Owin;
using Microsoft.Owin.Hosting;
using Nancy;
using Nancy.Owin;
using System.IO;

namespace DirtWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            var url = "http://127.0.0.1:1234";

			using (WebApp.Start<Startup>(url))
			{
				Console.WriteLine("Server started at " + url);
				Console.WriteLine("Press \'q\' to close the server.");
				while (Console.Read() != 'q') ;
			}
		}

	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseNancy();
			Nancy.StaticConfiguration.DisableErrorTraces = false;
		}
	}
}
