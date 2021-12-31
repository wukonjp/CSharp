using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace StringToSizedBytes
{
	class Program
	{
		const uint CP_ACP = 0;
		const int ERROR_INSUFFICIENT_BUFFER = 122;
		const uint WC_NO_BEST_FIT_CHARS = 0x00000400;

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		static extern int WideCharToMultiByte(uint CodePage, uint dwFlags,
			[MarshalAs(UnmanagedType.LPWStr)] string lpWideCharStr, int cchWideChar,
			[MarshalAs(UnmanagedType.LPArray)] byte[] lpMultiByteStr, int cbMultiByte,
			IntPtr lpDefaultChar, IntPtr lpUsedDefaultChar);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		static extern unsafe int WideCharToMultiByte(uint CodePage, uint dwFlags,
			char* lpWideCharStr, int cchWideChar,
			sbyte* lpMultiByteStr, int cbMultiByte,
			IntPtr lpDefaultChar, IntPtr lpUsedDefaultChar);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		static extern int MultiByteToWideChar(uint CodePage, uint dwFlags,
			[MarshalAs(UnmanagedType.LPArray)] byte[] lpMultiByteStr, int cbMultiByte,
			[Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpWideCharStr, int cchWideChar);

		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
		static extern unsafe int MultiByteToWideChar(uint CodePage, uint dwFlags,
			sbyte* lpMultiByteStr, int cbMultiByte,
			[Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpWideCharStr, int cchWideChar);

		static void Main(string[] args)
		{
			var srcText = "あｶいうaえおかきくけこ１２３４５６７８９０";
			Console.WriteLine("srcText = {0}", srcText);

			// 変換テスト
			for (int byteCount = 1; byteCount <= 43; byteCount++)
			{
				Console.WriteLine("byteCount = {0}", byteCount);

				var dst1Text = LimitByteCount1(srcText, byteCount);
				Console.WriteLine("Test1:{0};", dst1Text);

				var dst2Text = LimitByteCount2(srcText, byteCount);
				Console.WriteLine("Test2:{0};", dst2Text);

				var dst3Text = LimitByteCount3(srcText, byteCount);
				Console.WriteLine("Test3:{0};", dst3Text);
			}

			// 速度テスト
			{
				int byteCount = 39;
				var stopWatch = new Stopwatch();

				stopWatch.Restart();
				for (int i = 0; i < 10000000; i++)
				{
					LimitByteCount1(srcText, byteCount);
				}
				stopWatch.Stop();
				Console.WriteLine("Test1 Elapsed = {0}ms;", stopWatch.ElapsedMilliseconds);

				stopWatch.Restart();
				for (int i = 0; i < 10000000; i++)
				{
					LimitByteCount2(srcText, byteCount);
				}
				stopWatch.Stop();
				Console.WriteLine("Test2 Elapsed = {0}ms;", stopWatch.ElapsedMilliseconds);

				stopWatch.Restart();
				for (int i = 0; i < 10000000; i++)
				{
					LimitByteCount3(srcText, byteCount);
				}
				stopWatch.Stop();
				Console.WriteLine("Test3 Elapsed = {0}ms;", stopWatch.ElapsedMilliseconds);
			}

			Console.WriteLine("Complete");
			Console.ReadKey();
		}

		/// <summary>
		/// マネージのみ
		/// </summary>
		static string LimitByteCount1(string srcText, int byteCount)
		{
			if (string.IsNullOrEmpty(srcText))
			{
				return string.Empty;
			}

			var encoding = Encoding.Default;
			var buffer = encoding.GetBytes(srcText);
			if (buffer.Length <= byteCount)
			{
				return srcText;
			}

			var dstText = encoding.GetString(buffer, 0, byteCount);
			return dstText;
		}

		/// <summary>
		/// Win32API + マネージ
		/// </summary>
		static string LimitByteCount2(string srcText, int byteCount)
		{
			if(string.IsNullOrEmpty(srcText))
			{
				return string.Empty;
			}

			var buffer = new byte[byteCount + 1];

			int newSize = WideCharToMultiByte(
				CP_ACP, WC_NO_BEST_FIT_CHARS,
				srcText, srcText.Length,
				buffer, byteCount,
				IntPtr.Zero, IntPtr.Zero);
			if(newSize > 0)
			{
				return srcText;
			}

			int error = Marshal.GetLastWin32Error();
			if (error != ERROR_INSUFFICIENT_BUFFER)
			{
				return string.Empty;
			}

			var encoding = Encoding.Default;
			var dstText = encoding.GetString(buffer, 0, byteCount);
			return dstText;
		}

		/// <summary>
		/// Win32API + アンマネージ
		/// </summary>
		static unsafe string LimitByteCount3(string srcText, int byteCount)
		{
			if (string.IsNullOrEmpty(srcText))
			{
				return string.Empty;
			}

			sbyte* pBuffer = stackalloc sbyte[byteCount + 1];

			fixed (char* pSrcText = srcText)
			{
				int newSize = WideCharToMultiByte(
					CP_ACP, WC_NO_BEST_FIT_CHARS,
					pSrcText, srcText.Length,
					pBuffer, byteCount,
					IntPtr.Zero, IntPtr.Zero);
				if (newSize > 0)
				{
					return srcText;
				}
			}

			int error = Marshal.GetLastWin32Error();
			if (error != ERROR_INSUFFICIENT_BUFFER)
			{
				return string.Empty;
			}

			var dstText = new string(pBuffer);
			return dstText;
		}
	}
}
