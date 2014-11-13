using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirtWorld
{
	public class UserList
	{
		#region Properties

		public string Path{ get; private set; }

		public ReadOnlyCollection<User> Users {
			get {
				return new List<User> ().AsReadOnly ();
			}
		}

		#endregion

		#region Methods

		public void AddUser (string userId)
		{

		}

		public void RemoveUser (string userId)
		{

		}

		public void Save ()
		{

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

