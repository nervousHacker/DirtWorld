using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace DirtWorld
{
	public class Server
	{
		public event DataReceivedEventHandler DataReceived = delegate {};
		public event DataReceivedEventHandler ErrorReceived = delegate {};

		protected virtual void OnDataReceived (DataReceivedEventArgs e)
		{
			DataReceived (this, e);
		}

		protected virtual void OnErrorReceived (DataReceivedEventArgs e)
		{
			ErrorReceived (this, e);
		}

		#region Properties

		public Process Process { get; set; }

		public string Name { get; set; }

		public int MaxMemory { get; set; }

		public int InitialMemory { get; set; }

		public string Gui { get; set; }

		public string Jar { get; set; }

		public bool IsRunning{ get; set; }

		public DateTime LastBackup { get; set; }

		public ServerProperties Properties { get; set; }

		public string Directory { get; set; }

		#endregion

		#region Methods

		public void Start ()
		{
			this.Process = new Process ();
			this.Process.StartInfo.WorkingDirectory = this.Directory;
			this.Process.StartInfo.FileName = "java";
			this.Process.StartInfo.Arguments = String.Format ("-jar -Xmx{0}M -Xms{1}M {3} {4}",
				this.MaxMemory, this.InitialMemory, this.Jar, this.Gui);

			this.Process.StartInfo.UseShellExecute = false;

			this.Process.StartInfo.RedirectStandardOutput = true;
			this.Process.StartInfo.RedirectStandardInput = true;
			this.Process.StartInfo.RedirectStandardError = true;
			this.Process.ErrorDataReceived += ErrorReceived;
			this.Process.OutputDataReceived += DataReceived;

			this.IsRunning = this.Process.Start ();

			this.Process.BeginOutputReadLine ();
			this.Process.BeginErrorReadLine ();
		}

		public void Stop ()
		{
			this.Process.StandardInput.WriteLine ("stop");
			this.Process.Close ();
			this.Process.Dispose ();

			this.IsRunning = this.Process.HasExited;
		}

		public void Restart ()
		{
			Stop ();
			Start ();
		}

		public void Backup ()
		{
			// check out sharpziplib
		}

		public void SetEula (bool agree)
		{
			var filePath = this.Directory + "/eula.txt";
			string[] eula;

			if (File.Exists (filePath)) {
				eula = File.ReadAllLines (filePath);

				for (int i = 0; i < eula.Length; i++) {
					if (eula [i].StartsWith ("eula=")) {
						eula [i] = agree ? "eula=true" : "eula=false";
					}
				}
			} else {
				eula = new string[1];
				eula [0] = agree ? "eula=true" : "eula=false";
			}

			File.WriteAllLines (filePath, eula);
		}

		public void LoadServerProperties ()
		{
			this.Properties.Load (this.Directory);
		}

		public void SaveServerProperties ()
		{
			this.Properties.Save (this.Directory);
		}

		public void SendCommand()
		{

		}
		#endregion

		#region Constructors

		public Server (string directory)
		{
			Directory = directory;
			Name = "minecraft server";
			MaxMemory = 1024;
			InitialMemory = 1024;
			Gui = "nogui";
			Jar = "minecraft_server.1.8.jar";
			IsRunning = false;
		}

		#endregion
	}
}

