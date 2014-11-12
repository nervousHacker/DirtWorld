using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using Nancy;
using Nancy.ModelBinding;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace DirtWorld
{
	public class WebInterface : NancyModule
	{
		private static Server server;
		private static string serverDirectory;
		private static string jarDirectory;
		private static string defaultJarUrl = "https://s3.amazonaws.com/Minecraft.Download/versions/1.8/minecraft_server.1.8.jar";

		public WebInterface ()
		{
			SetUpDirectories ();

			Get ["/{name?}"] = parameters => {
				System.GC.GetTotalMemory (false);
				return "Welcome to DirtWorld " + parameters.name;
			};

			Get ["/start"] = parameters => {
				//StartServer ();
				//CreateServerDirectory();
				return "starting up";
			};

			Get ["/getjar"] = parameters => {
				DownloadServerJar(defaultJarUrl);
				//CreateServerDirectory();
				return "got it";
			};

			Get ["/jars"] = parameters => {
				var jars = GetExistingJars().Select(x=> x.Name);

				return Response.AsJson(jars, Nancy.HttpStatusCode.OK);
			};

			Get ["/create/{name}"] = parameters => {
				server = new Server (CreateServerDirectory(parameters.name));
				server.Name = parameters.name;
				server.SetEula (true);

				server.DataReceived += (sender, args) => {
					Console.WriteLine(args.Data);
				};
				return "starting up";
			};

			Get ["/stop"] = parameters => {
				//StopServer ();
				return "stopping server";
			};

			Get ["/test"] = parameters => {
				return Response.AsFile ("Content/index.html");
			};
		}


		private DirectoryInfo[] GetExistingServers ()
		{
			var di = new DirectoryInfo (serverDirectory);
			return di.GetDirectories ();
		}

		private FileInfo[] GetExistingJars ()
		{
			var di = new DirectoryInfo (jarDirectory);
			return di.GetFiles ();
		}

		private void DownloadServerJar(string jarUrl){
			using (var wc = new WebClient ()) {
				wc.DownloadFile(jarUrl, jarDirectory + "/" + jarUrl.Substring(jarUrl.LastIndexOf('/')));
			}
		}

		public string GetHomePath ()
		{
			string homePath;

			if (Environment.OSVersion.Platform == PlatformID.Unix ||
			    Environment.OSVersion.Platform == PlatformID.MacOSX) {
				homePath = Environment.GetEnvironmentVariable ("HOME");
			} else {
				homePath = Environment.ExpandEnvironmentVariables ("%HOMEDRIVE%%HOMEPATH%");
			}

			return homePath;
		}

		private void SetUpDirectories ()
		{
			serverDirectory = Directory.CreateDirectory (GetHomePath () + "/minecraft_servers").FullName;
			jarDirectory = Directory.CreateDirectory (serverDirectory + "/server_jars").FullName;
		}

		public string CreateServerDirectory (string serverName)
		{
			var serverPath = GetHomePath () + "/minecraft_servers/" + serverName.Replace (" ", "_");
			return Directory.CreateDirectory (serverPath).FullName;
		}

		static void DataReceived(object sender, EventArgs e)
		{
			Console.WriteLine ("got me some data");
		}
	}
}

