using System;

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

		public void GetProfile ()
		{

		}

		#endregion

		#region Constructors

		public User (string name)
		{
		}

		#endregion
	}
}

