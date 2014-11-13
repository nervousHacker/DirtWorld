using System;
using System.Net;

namespace DirtWorld
{
	public class User
	{
		#region Properties

		public string Uuid{ get; private set; }

		public string Name{ get; private set; }

		public bool Legacy{ get; private set; }

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

		#endregion

		#region Constructors

		public User (string name)
		{
		}

		#endregion
	}
}

