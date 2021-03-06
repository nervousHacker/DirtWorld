﻿using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;

namespace DirtWorld
{
	public class McServer
	{
		#region Event Handlers

		public event DataReceivedEventHandler DataReceived;
		public event DataReceivedEventHandler ErrorReceived;

		protected virtual void OnDataReceived(DataReceivedEventArgs e)
		{
			if (DataReceived != null) {
				DataReceived(this, e);
			}
		}

		protected virtual void OnErrorReceived(DataReceivedEventArgs e)
		{
			if (ErrorReceived != null) {
				ErrorReceived(this, e);
			}
		}

		#endregion

		#region Properties

		public Process Process { get; set; }

		public string Name { get; set; }

		public int MaxMemory { get; set; }

		public int InitialMemory { get; set; }

		public string Gui { get; set; }

		public string Jar { get; set; }

		public bool IsRunning{ get; set; }

		public DateTime LastBackup { get; set; }

		public McServerProperties Properties { get; set; }

		public string Directory { get; set; }

		#endregion

		#region Public Methods

		public void Start()
		{
			this.Process = new Process();
			this.Process.StartInfo.WorkingDirectory = this.Directory;
			this.Process.StartInfo.FileName = "java";
			this.Process.StartInfo.Arguments = String.Format("-jar -Xmx{0}M -Xms{1}M {2} {3}",
			                                                  this.MaxMemory, this.InitialMemory, this.Jar, this.Gui);

			this.Process.StartInfo.UseShellExecute = false;

			this.Process.StartInfo.RedirectStandardOutput = true;
			this.Process.StartInfo.RedirectStandardInput = true;
			this.Process.StartInfo.RedirectStandardError = true;
			this.Process.ErrorDataReceived += ErrorReceived;
			this.Process.OutputDataReceived += DataReceived;

			this.IsRunning = this.Process.Start();

			this.Process.BeginOutputReadLine();
			this.Process.BeginErrorReadLine();
		}

		public void Stop()
		{
			this.Process.StandardInput.WriteLine("stop");
			this.Process.CloseMainWindow();
			this.IsRunning = this.Process.HasExited;
			this.Process.Close();
			this.Process.Dispose();
		}

		public void Restart()
		{
			Stop();
			Start();
		}

		public void Backup()
		{
			// check out sharpziplib
		}

		public void SetEula(bool agree)
		{
			var filePath = this.Directory + "/eula.txt";
			string[] eula;

			if (File.Exists(filePath)) {
				eula = File.ReadAllLines(filePath);

				for (int i = 0; i < eula.Length; i++) {
					if (eula[i].StartsWith("eula=")) {
						eula[i] = agree ? "eula=true" : "eula=false";
					}
				}
			} else {
				eula = new string[1];
				eula[0] = agree ? "eula=true" : "eula=false";
			}

			File.WriteAllLines(filePath, eula);
		}

		public void LoadServerProperties()
		{
			this.Properties.Load(this.Directory);
		}

		public void SaveServerProperties()
		{
			this.Properties.Save(this.Directory);
		}

		public void SaveWhiteList(List<dynamic> players)
		{
			SaveJsonObects(this.Directory + "/whitelist.json", players);
		}

		public List<dynamic> GetWhiteList()
		{
			return GetJsonObjects(this.Directory + "/whilelist.json");
		}

		public void SaveOps(List<dynamic> players)
		{
			SaveJsonObects(this.Directory + "/ops.json", players);
		}

		public List<dynamic> GetOps()
		{
			return GetJsonObjects(this.Directory + "/ops.json");
		}

		public void SaveBannedPlayers(List<dynamic> players)
		{
			SaveJsonObects(this.Directory + "/banned-players.json", players);
		}

		public List<dynamic> GetBannedPlayers()
		{
			return GetJsonObjects(this.Directory + "/banned-players.json");
		}
			
		public void SendCommand()
		{

		}

		public string GetJarName()
		{
			var jar = "";
			var di = new DirectoryInfo(Directory);
			foreach(var fi in di.GetFiles()) {
				if (fi.Name.Contains("minecraft_server") && fi.Name.EndsWith(".jar")) {
					jar = fi.Name;
				}
			}
			return jar;
		}

		public static string GetUserProfile(string userId)
		{
			string profile = "";
			using (var wc = new WebClient()) {
				profile = wc.DownloadString("https://api.mojang.com/users/profiles/minecraft/" + userId);
			}
			return profile;
		}
			
		#endregion

		#region Private Methods

		private void SaveUserCache(List<dynamic> players)
		{
			SaveJsonObects(this.Directory + "/usercache.json", players);
		}

		private List<dynamic> GetUserCache()
		{
			return GetJsonObjects(this.Directory + "/usercache.json");
		}

		private List<dynamic> GetJsonObjects(string filePath)
		{
			List<dynamic> jsonObjects;
			string jsonString = null;

			if (File.Exists(filePath)) {
				jsonString = File.ReadAllText(filePath);	 
			}

			if (jsonString != null) {
				jsonObjects = new Nancy.Json.JavaScriptSerializer().DeserializeObject(jsonString) as List<dynamic>;		
			}
			else {
				jsonObjects = new List<dynamic>();
			}

			return jsonObjects;
		}

		private void SaveJsonObects(string filePath, List<dynamic> jsonObjects)
		{
			var jsonString = new Nancy.Json.JavaScriptSerializer().Serialize(jsonObjects);
			File.WriteAllText(filePath, jsonString);
		}

		#endregion

		#region Constructors

		public McServer(string directory)
		{
			Directory = directory;

			// if server exists, load data. 
			// else create new directory and associated files.

			Name = "server";
			MaxMemory = 1024;
			InitialMemory = 1024;
			Gui = "nogui";
			Jar = "minecraft_server.1.8.jar";
			IsRunning = false;
		}

		#endregion
	}
}

