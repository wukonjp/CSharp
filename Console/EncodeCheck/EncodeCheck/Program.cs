using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncodeCheck
{
	class Program
	{
		static void Main(string[] args)
		{
			byte[] src = 
			{
				0x00,
				0x01,
				0x89,
				0x40,
				0x8c,
				0x41,
				0x00,
				0x31,
				0x0d,
				0x0a
			};

			//var encoding = Encoding.GetEncoding(932);
			//var encoding = Encoding.ASCII;
			var encoding = Encoding.UTF8;

			var dst = encoding.GetString(src);
			var bytes = encoding.GetBytes(dst);
			var chars = dst.ToCharArray();
		}
	}
}
