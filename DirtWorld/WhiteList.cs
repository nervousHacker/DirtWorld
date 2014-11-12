using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirtWorld
{
	public class WhiteList
	{
		#region Methods

		public void AddUser (string userId)
		{

		}

		public void RemoveUser (string userId)
		{

		}

		public ReadOnlyCollection<User> Users {
			get {
				return new List<User> ().AsReadOnly ();
			}
		}


		private void Save ()
		{

		}

		#endregion

		#region Constructors

		public WhiteList (string serverName)
		{
		}

		#endregion
	}
}

