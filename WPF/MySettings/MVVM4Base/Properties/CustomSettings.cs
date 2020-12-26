using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MVVM4Base.Properties
{
	public class Person
	{
		public string Name { get; set; } = "山本";

		public int Age { get; set; } = 18;

		public static readonly Person Default = new Person();

		public Person()
		{
		}
	}

	internal sealed partial class Settings : ApplicationSettingsBase
	{
		[UserScopedSetting()]
		public List<Person> People
		{
			get
			{
				return (List<Person>)this[nameof(People)];
			}
			set
			{
				this[nameof(People)] = value;
			}
		}
	}
}
