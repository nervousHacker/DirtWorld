using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DirtWorld
{
	public class UserList
	{
		#region Properties

		public string Path{ get; private set; }

		public ReadOnlyCollection<dynamic> Users {
			get {
				return new List<dynamic> ().AsReadOnly ();
			}
		}

		#endregion

		#region Methods

		public void AddUser (dynamic user)
		{
			var users = this.Load();
			users.Add(user);
			this.Save(users);
		}

		public void RemoveUser (string userId)
		{

		}

		private List<dynamic> Load(){
			List<dynamic> users;

			if (File.Exists(this.Path)) {
				var userJson = File.ReadAllText(this.Path);
				users = new Nancy.Json.JavaScriptSerializer().DeserializeObject(userJson) as List<dynamic>;		
			} 
			else {
				users = new List<dynamic>();
			}

			return users;
		}

		private void Save (List<dynamic> users)
		{
			var userJson = new Nancy.Json.JavaScriptSerializer().Serialize(users);
			File.WriteAllText(this.Path, userJson);
		}

		#endregion

		#region Constructors

		public UserList (string path)
		{
			this.Path = path;
		}

		#endregion
	}
}

