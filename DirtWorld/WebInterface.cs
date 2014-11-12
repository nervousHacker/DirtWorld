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
		private string ServerDirectory;
		private string JarDirectory;

		public WebInterface ()
		{
			SetUpDirectories ();

			Get ["/{name?}"] = parameters => {
				System.GC.GetTotalMemory (false);
				return "Welcome to DirtWorld " + parameters.name;
			};

			Get ["/start"] = parameters => {
				StartServer ();
				//CreateServerDirectory();
				return "starting up";
			};

			Get ["/getjar"] = parameters => {
				DownloadServerJar("");
				//CreateServerDirectory();
				return "got it";
			};

			Get ["/jars"] = parameters => {
				var jars = GetExistingJars().Select(x=> x.Name);

				return Response.AsJson(jars, Nancy.HttpStatusCode.OK);
			};

			Get ["/create/{name}"] = parameters => {
				var serv = new Server (CreateServerDirectory(parameters.name));
				serv.Name = parameters.name;
				serv.SetEula (true);

				serv.DataReceived += (sender, args) => {
					Console.WriteLine(args.Data);
				};
				return "starting up";
			};

			Get ["/stop"] = parameters => {
				StopServer ();
				return "stopping server";
			};

			Get ["/test"] = parameters => {
				return Response.AsFile ("Content/index.html");
			};
		}

		// jar url
		// https://s3.amazonaws.com/Minecraft.Download/versions/1.8/minecraft_server.1.8.jar

		private DirectoryInfo[] GetExistingServers ()
		{
			var di = new DirectoryInfo (ServerDirectory);
			return di.GetDirectories ();
		}

		private FileInfo[] GetExistingJars ()
		{
			var di = new DirectoryInfo (JarDirectory);
			return di.GetFiles ();
		}

		private void DownloadServerJar(string jarUrl){
			jarUrl = "https://s3.amazonaws.com/Minecraft.Download/versions/1.8/minecraft_server.1.8.jar";
			using (var wc = new WebClient ()) {
				wc.DownloadFile(jarUrl, JarDirectory + "/" + jarUrl.Substring(jarUrl.LastIndexOf('/')));
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
			this.ServerDirectory = Directory.CreateDirectory (GetHomePath () + "/minecraft_servers").FullName;
			this.JarDirectory = Directory.CreateDirectory (this.ServerDirectory + "/server_jars").FullName;
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

		static Process serverProcess;

		public void StartServer ()
		{
			serverProcess = new Process ();
			serverProcess.StartInfo.WorkingDirectory = "/Users/stum/Downloads/";
			serverProcess.StartInfo.FileName = "java";
			serverProcess.StartInfo.Arguments = "-jar -Xmx1024M -Xms1024M minecraft_server.1.8.jar nogui";
			serverProcess.StartInfo.UseShellExecute = false;
			serverProcess.StartInfo.RedirectStandardOutput = true;
			serverProcess.StartInfo.RedirectStandardInput = true;
			serverProcess.OutputDataReceived += (sender, args) => Console.WriteLine ("received output: {0}", args.Data);
			serverProcess.Start ();
			serverProcess.BeginOutputReadLine ();

		}

		public void StopServer ()
		{
			var input = serverProcess.StandardInput;
			input.WriteLine ("stop");
			serverProcess.Close ();
			serverProcess.Dispose ();
		}
	}
}

