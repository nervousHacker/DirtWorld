using System;
using System.Net;

namespace DirtWorld
{
	public class User
	{
		#region Properties

		public string Uuid{ get; set; }

		public string Name{ get; set; }

		public bool Legacy{ get; set; }

		#endregion

		#region Methods

		public static string GetProfile (string userId)
		{
			string profile = "";
			using(var wc = new WebClient()) {
				profile = wc.DownloadString ("https://api.mojang.com/users/profiles/minecraft/" + userId);
			}
			return profile;
		}

		public string ToJson(){
			return new Nancy.Json.JavaScriptSerializer().Serialize(this);
		}

		#endregion

		#region Constructors

		public User (string name)
		{
			this.Name = name;
		}

		#endregion
	}
}

