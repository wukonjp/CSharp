using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace StringToStruct
{
	/// <summary>
	/// 文字列を構造体にバイナリ展開する
	/// </summary>
	class Program
	{
		/// <summary>
		/// バイナリデータ構造体
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4, Size = 36)]
		struct BynaryData
		{
			public byte B1;
			public ushort W1;
			public uint DW1;

			[MarshalAs(UnmanagedType.LPTStr)]
			public string LPTText;									// c/c++ char*

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
			public string ByValText;                                // c/c++ char[SizeConst]

			public byte B2;
			public ushort W2;
			public uint DW2;
		}

		/// <summary>
		/// エントリーポイント
		/// </summary>
		static void Main(string[] args)
		{
			var srcData = new BynaryData();
			srcData.B1 = 0x11;
			srcData.W1 = 0xFF11;
			srcData.DW1 = 0xFFFFFF11;
			srcData.LPTText = "０１２";
			srcData.ByValText = "３４５";
			srcData.B2 = 0x22;
			srcData.W2 = 0xFF22;
			srcData.DW2 = 0xFFFFFF22;
			Console.WriteLine("変換前LPT文字列: {0}", srcData.LPTText);
			Console.WriteLine("変換前ByVal文字列: {0}", srcData.ByValText);

			var buffer1 = StructureToBytes(srcData);
			Console.WriteLine("変換後の構造体サイズ： {0}", buffer1.Length);
			foreach (var b in buffer1)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.WriteLine();
			var dstData = (BynaryData)BytesToStructure(buffer1, typeof(BynaryData));
			DestroyStructure(buffer1, typeof(BynaryData));
			Console.WriteLine("変換データ解放：");
			foreach (var b in buffer1)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.WriteLine("\r\n");
			Console.WriteLine("復元後LPT文字列: {0}", dstData.LPTText);
			Console.WriteLine("復元後ByVal文字列: {0}", dstData.ByValText);

			var buffer2 = StructureToBytes(dstData);
			Console.WriteLine("復元後の構造体サイズ： {0}", buffer2.Length);
			foreach (var b in buffer2)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.WriteLine();
			DestroyStructure(buffer2, typeof(BynaryData));
			Console.WriteLine("復元データ解放：");
			foreach (var b in buffer2)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// 構造体からバイト配列を生成する
		/// ※構造体にマネージ参照を含んでいる場合はバイト配列上にアンマネージ参照が生成される。
		/// 　バイト配列の使用後はMarshal.DestroyStructure()で参照先のアンマネージメモリを
		/// 　開放しなければメモリリークする。
		/// </summary>
		private static byte[] StructureToBytes(object structure)
		{
			int size = Marshal.SizeOf(structure);
			var buffer = new byte[size];

			var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			try
			{
				var ptr = gch.AddrOfPinnedObject();

				// Marshal.StructureToPtr()がUnmanagedType.ByValTStrに対して行う挙動
				// ・文字列を固定長に丸める。
				// ・文字コードをShiftJISコードへ変換する。
				// ・文字列の(SizeConst-1)位置へヌル文字を書き込む。
				// ・ヌル文字書き込み位置に全角文字があった場合は例外を発生する。
				// 　※そのためSizeConstを十分な大きさに設定する必要がある。
				Marshal.StructureToPtr(structure, ptr, false);
			}
			finally
			{
				gch.Free();
			}

			return buffer;
		}

		/// <summary>
		/// バイト配列から構造体を生成する
		/// </summary>
		private static object BytesToStructure(byte[] buffer, Type structureType)
		{
			object structure;

			var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			try
			{
				var ptr = gch.AddrOfPinnedObject();
				structure = Marshal.PtrToStructure(ptr, structureType);
			}
			finally
			{
				gch.Free();
			}

			return structure;
		}

		/// <summary>
		/// バイト配列上の構造体からアンマネージ参照を開放する
		/// </summary>
		private static void DestroyStructure(byte[] buffer, Type structureType)
		{
			var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			try
			{
				var ptr = gch.AddrOfPinnedObject();
				Marshal.DestroyStructure(ptr, structureType);
			}
			finally
			{
				gch.Free();
			}
		}
	}
}
