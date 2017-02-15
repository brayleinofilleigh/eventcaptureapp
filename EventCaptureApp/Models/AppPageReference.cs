using System;

namespace EventCaptureApp.Models
{
	public class AppPageReference
	{
		private Type _type;

		public AppPageReference(Type type)
		{
			this.Type = type;
		}

		public Type Type
		{
			get { return _type; }
			private set { _type = value; }
		}

		public string Name
		{
			get { return _type.Name; }
		}
	}
}
