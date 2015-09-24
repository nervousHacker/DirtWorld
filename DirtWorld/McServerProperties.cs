using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirtWorld
{
	public class McServerProperties
	{
		#region Enums
		public enum Difficulties
		{
			Peaceful,
			Easy,
			Normal,
			Hard

		}

		public enum GameModes
		{
			Survival,
			Creative,
			Adventure,
			Spectator

		}

		public enum LevelTypes
		{
			DEFAULT,
			FLAT,
			LARGEBIOMES,
			AMPLIFIED,
			CUSTOMIZED

		}
		#endregion

		#region Private Fields
		private Dictionary<string, string> _properties;
		#endregion

		#region Properties
		public bool AllowFlight {
			get {
				return bool.Parse(_properties["allow-flight"]);
			}
			set {
				_properties["allow-flight"] = value.ToString().ToLower();
			}
		}

		public bool AllowNether {
			get {
				return bool.Parse(_properties["allow-nether"]);
			}
			set {
				_properties["allow-nether"] = value.ToString().ToLower();
			}
		}

		public bool AnnouncePlayerAchievements {
			get {
				return bool.Parse(_properties["announce-player-achievements"]);
			}
			set {
				_properties["announce-player-achievements"] = value.ToString().ToLower();
			}
		}

		public Difficulties Difficulty {
			get {
				return (Difficulties)(int.Parse(_properties["difficulty"]));
			}
			set {
				_properties["difficulty"] = value.ToString();
			}
		}

		public bool EnableQuery {
			get {
				return bool.Parse(_properties["enable-query"]);
			}
			set {
				_properties["enable-query"] = value.ToString().ToLower();
			}
		}

		public bool EnableRcon {
			get {
				return bool.Parse(_properties["enable-rcon"]);
			}
			set {
				_properties["enable-rcon"] = value.ToString().ToLower();
			}
		}

		public bool EnableCommandBlock {
			get {
				return bool.Parse(_properties["enable-command-block"]);
			}
			set {
				_properties["enable-command-block"] = value.ToString().ToLower();
			}
		}

		public bool ForceGameMode {
			get {
				return bool.Parse(_properties["force-gamemode"]);
			}
			set {
				_properties["force-gamemode"] = value.ToString().ToLower();
			}
		}

		public GameModes GameMode {
			get {
				return (GameModes)(int.Parse(_properties["gamemode"]));
			}
			set {
				_properties["gamemode"] = value.ToString();
			}
		}

		public bool GenerateStructures {
			get {
				return bool.Parse(_properties["generate-structures"]);
			}
			set {
				_properties["generate-structures"] = value.ToString().ToLower();
			}
		}

		public string GeneratorSettings {
			get {
				return _properties["generator-settings"];
			}
			set {
				_properties["generator-settings"] = value;
			}
		}

		public bool Hardcore {
			get {
				return bool.Parse(_properties["hardcore"]);
			}
			set {
				_properties["hardcore"] = value.ToString().ToLower();
			}
		}

		public string LevelName {
			get {
				return _properties["level-name"];
			}
			set {
				_properties["level-name"] = value;
			}
		}

		public string LevelSeed {
			get {
				return _properties["level-seed"];
			}
			set {
				_properties["level-seed"] = value;
			}
		}

		public LevelTypes LevelType {
			get {
				return (LevelTypes)Enum.Parse(typeof(LevelTypes), _properties["level-type"]);
			}
			set {
				_properties["level-type"] = value.ToString();
			}
		}

		public int MaxBuildHeight {
			get {
				return int.Parse(_properties["max-build-height"]);
			}
			set {
				_properties["max-build-height"] = value.ToString();
			}
		}

		public int MaxPlayers {
			get {
				return int.Parse(_properties["max-players"]);
			}
			set {
				_properties["max-players"] = value.ToString();
			}
		}

		public int MaxTickTime {
			get {
				return int.Parse(_properties["max-tick-time"]);
			}
			set {
				_properties["max-tick-time"] = value.ToString();
			}
		}

		public int MaxWorldSize {
			get {
				return int.Parse(_properties["max-world-size"]);
			}
			set {
				_properties["max-world-size"] = value.ToString();
			}
		}

		public string Motd {
			get {
				return _properties["motd"];
			}
			set {
				_properties["motd"] = value;
			}
		}

		public int NetworkCompressionThreshold {
			get {
				return int.Parse(_properties["network-compression-threshold"]);
			}
			set {
				_properties["network-compression-threshold"] = value.ToString();
			}
		}

		public bool OnlineMode {
			get {
				return bool.Parse(_properties["online-mode"]);
			}
			set {
				_properties["online-mode"] = value.ToString().ToLower();
			}
		}

		public int OpPermissionLevel {
			get {
				return int.Parse(_properties["op-permission-level"]);
			}
			set {
				_properties["op-permission-level"] = value.ToString();
			}
		}

		public int PlayerIdleTimeout {
			get {
				return int.Parse(_properties["player-idle-timeout"]);
			}
			set {
				_properties["player-idle-timeout"] = value.ToString();
			}
		}

		public bool Pvp {
			get {
				return bool.Parse(_properties["pvp"]);
			}
			set {
				_properties["pvp"] = value.ToString().ToLower();
			}
		}

		public int QueryPort {
			get {
				return int.Parse(_properties["query.port"]);
			}
			set {
				_properties["query.port"] = value.ToString();
			}
		}

		public string RconPassword {
			get {
				return _properties["rcon.password"];
			}
			set {
				_properties["rcon.password"] = value;
			}
		}

		public int RconPort {
			get {
				return int.Parse(_properties["rcon.port"]);
			}
			set {
				_properties["rcon.port"] = value.ToString();
			}
		}

		public string ResourcePack {
			get {
				return _properties["resource-pack"];
			}
			set {
				_properties["resource-pack"] = value;
			}
		}

		public string ResourcePackHash {
			get {
				return _properties["resource-pack-hash"];
			}
			set {
				_properties["resource-pack-hash"] = value;
			}
		}

		public string ServerIp {
			get {
				return _properties["server-ip"];
			}
			set {
				_properties["server-ip"] = value;
			}
		}

		public int ServerPort {
			get {
				return int.Parse(_properties["server-port"]);
			}
			set {
				_properties["server-port"] = value.ToString();
			}
		}

		public bool SnooperEnabled {
			get {
				return bool.Parse(_properties["snooper-enabled"]);
			}
			set {
				_properties["snooper-enabled"] = value.ToString().ToLower();
			}
		}

		public bool SpawnAnimals {
			get {
				return bool.Parse(_properties["spawn-animals"]);
			}
			set {
				_properties["spawn-animals"] = value.ToString().ToLower();
			}
		}

		public bool SpawnMonsters {
			get {
				return bool.Parse(_properties["spawn-monsters"]);
			}
			set {
				_properties["spawn-monsters"] = value.ToString().ToLower();
			}
		}

		public bool SpawnNpcs {
			get {
				return bool.Parse(_properties["spawn-npcs"]);
			}
			set {
				_properties["spawn-npcs"] = value.ToString().ToLower();
			}
		}

		public int SpawnProtection {
			get {
				return int.Parse(_properties["spawn-protection"]);
			}
			set {
				_properties["spawn-protection"] = value.ToString();
			}
		}

		public int ViewDistance {
			get {
				return int.Parse(_properties["view-distance"]);
			}
			set {
				_properties["view-distance"] = value.ToString();
			}
		}

		public bool WhiteList {
			get {
				return bool.Parse(_properties["white-list"]);
			}
			set {
				_properties["white-list"] = value.ToString().ToLower();
			}
		}
		#endregion

		#region Methods
		public void Load(string directory)
		{
			var filePath = directory + "/server.properties";

			if (File.Exists(filePath)) {
				var configLines = File.ReadAllLines(filePath);

				string[] kvp;
				foreach (var line in configLines) {
					kvp = line.Split('=');
					_properties[kvp[0]] = kvp[1];
				}			
			}
		}

		public void Save(string directory)
		{
			var filePath = directory + "/server.properties";
			var configLines = _properties.Select(x => x.Key + "=" + x.Value).ToArray();
			File.WriteAllLines(filePath, configLines);
		}
		#endregion

		#region Constructors
		public McServerProperties()
		{
			_properties = new Dictionary<string, string>();
			_properties.Add("allow-flight", "false");
			_properties.Add("allow-nether", "true");
			_properties.Add("announce-player-achievements", "true");
			_properties.Add("difficulty", "1");
			_properties.Add("enable-query", "false");
			_properties.Add("enable-rcon", "false");
			_properties.Add("enable-command-block", "false");
			_properties.Add("force-gamemode", "false");
			_properties.Add("gamemode", "0");
			_properties.Add("generate-structures", "true");
			_properties.Add("generator-settings", "");
			_properties.Add("hardcore", "false");
			_properties.Add("level-name", "world");
			_properties.Add("level-seed", "");
			_properties.Add("level-type", "DEFAULT");
			_properties.Add("max-build-height", "256");
			_properties.Add("max-players", "20");
			_properties.Add("max-tick-time", "60000");
			_properties.Add("max-world-size", "29999984");
			_properties.Add("motd", "A Minecraft Server");
			_properties.Add("network-compression-threshold", "256");
			_properties.Add("online-mode", "true");
			_properties.Add("op-permission-level", "4");
			_properties.Add("player-idle-timeout", "0");
			_properties.Add("pvp", "true");
			_properties.Add("query.port", "25565");
			_properties.Add("rcon.password", "");
			_properties.Add("rcon.port", "25576");
			_properties.Add("resource-pack", "");
			_properties.Add("resource-pack-hash", "");
			_properties.Add("server-ip", "");
			_properties.Add("server-port", "25565");
			_properties.Add("snooper-enabled", "false");
			_properties.Add("spawn-animals", "true");
			_properties.Add("spawn-monsters", "true");
			_properties.Add("spawn-npcs", "true");
			_properties.Add("spawn-protection", "16");
			_properties.Add("view-distance", "10");
			_properties.Add("white-list", "true");
		}

		public McServerProperties(string[] configLines)
			: base()
		{
			string[] kvp;

			foreach (var line in configLines) {
				kvp = line.Split('=');
				_properties[kvp[0]] = kvp[1];
			}
		}
		#endregion
	}
}
