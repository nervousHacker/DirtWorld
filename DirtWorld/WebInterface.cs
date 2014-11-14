using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using Nancy;
using Nancy.ModelBinding;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace DirtWorld
{
	public class WebInterface : NancyModule
	{
		private static Server server;
		private static string serverDirectory;
		private static string jarDirectory;
		private static string defaultJarUrl = "https://s3.amazonaws.com/Minecraft.Download/versions/1.8/minecraft_server.1.8.jar";

		public WebInterface()
		{
			SetUpDirectories();

			Get["/{name?}"] = parameters => {
				var u = new {name = "magumbo", uuid = "test-uuid", legacy = true};
				return "Welcome to DirtWorld " + parameters.name + 
					"\nUser: " + new Nancy.Json.JavaScriptSerializer().Serialize(u);
			};

			Get["/start"] = parameters => {
				//StartServer ();
				//CreateServerDirectory();
				return "starting up";
			};

			Get["/getjar"] = parameters => {
				DownloadServerJar(defaultJarUrl);
				//CreateServerDirectory();
				return "got it";
			};

			Get["/jars"] = parameters => {
				server = new Server(serverDirectory);
				var wl = server.GetWhiteList();
				dynamic user = new ExpandoObject();
				user.name = "bob";
				user.uuid = "sljflskfjk";
					user.legacy = true;
				wl.Add(user);
				server.SaveWhiteList(wl);

				var jars = GetExistingJars().Select(x => x.Name);

				return Response.AsJson(jars, Nancy.HttpStatusCode.OK);
			};

			Get["/create/{name}"] = parameters => {
				if (GetExistingServers().Count(x => x.Name == parameters.name) > 0) {
					// server with that name already exists.
					// delete existing server to create a new server with the same name.
					// return the message.
				}

				server = new Server(serverDirectory);
				server.Name = parameters.name;
				server.SetEula(true);

				server.DataReceived += (sender, args) => {
					Console.WriteLine(args.Data);
				};
				return "starting up";
			};

			Get["/stop"] = parameters => {
				//StopServer ();
				return "stopping server";
			};

			Get["/test"] = parameters => {
				return Response.AsFile("Content/index.html");
			};
		}

		private DirectoryInfo[] GetExistingServers()
		{
			var di = new DirectoryInfo(serverDirectory);
			return di.GetDirectories();
		}

		private FileInfo[] GetExistingJars()
		{
			var di = new DirectoryInfo(jarDirectory);
			return di.GetFiles();
		}

		private void DownloadServerJar(string jarUrl)
		{
			using (var wc = new WebClient()) {
				wc.DownloadFile(jarUrl, jarDirectory + "/" + jarUrl.Substring(jarUrl.LastIndexOf('/')));
			}
		}

		private void SetUpDirectories()
		{
			string homePath;
			string dataDirectory = "dirtworld";

			if (Environment.OSVersion.Platform == PlatformID.Unix ||
			    Environment.OSVersion.Platform == PlatformID.MacOSX) {
				homePath = Environment.GetEnvironmentVariable("HOME");
			} else {
				homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
			}

			serverDirectory = Directory.CreateDirectory(homePath + "/" + dataDirectory + "/server").FullName;
			jarDirectory = Directory.CreateDirectory(homePath + "/" + dataDirectory + "/server_jars").FullName;
		}

		static void DataReceived(object sender, EventArgs e)
		{
			Console.WriteLine("got me some data");
		}
	}
}

