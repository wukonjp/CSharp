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
		/// ・書式指定されたclass/structが対象
		/// ・CharSetに設定した値でTCHARの型が変化する
		/// ・UnmanagedType.LPxxxx指定するとアンマネージメモリが確保され、そのポインタが埋め込まれる（要開放）
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4, Size = 36)]
		sealed class BinaryData
		{
			// Marshal.StructureToPtrメソッドがUnmanagedType.LPTStrに対して行う動作
			// CharSet = CharSet.Ansi の場合
			// ・Unicode文字列をShiftJIS文字列へ変換する。
			// ・ShiftJIS文字列をアンマネージメモリ※に格納し、そのポインタをアンマネージ構造体にセットする。
			// ※Marshal.DestroyStructureメソッドで開放が必要。
			[MarshalAs(UnmanagedType.LPTStr)]
			public string LPTText;                                  // 8  byte (TCHAR*)

			public byte B1;                                         // 1  byte
																	// 1  byte (Padding)
			public ushort W1;										// 2  byte
			public uint DW1;                                        // 4  byte

			// Marshal.StructureToPtrメソッドがUnmanagedType.ByValTStrに対して行う動作
			// CharSet = CharSet.Ansi の場合
			// ・Unicode文字列をShiftJIS文字列へ変換する。
			// ・ShiftJIS文字列を固定長(SizeConst-1)に切り詰めてアンマネージ構造体に埋め込む。
			// ・末尾位置(SizeConst-1)にはNULL文字を書き込む。
			// ・末尾位置(SizeConst-1)がトレイルバイトの場合は、例外が発生することがある。
			// 対策1. CharSet = CharSet.Unicode を使用する。
			// 対策2. 変換後にSizeConstを超えないよう、あらかじめUnicode文字列を切り詰めておく。
			// 対策3. SizeConstを十分に大きくする。
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
			public string ByValText;                                // 7  byte (TCHAR[SizeConst])
																	// 1  byte (Padding)

			// Marshal.StructureToPtrメソッドがUnmanagedType.ByValArrayに対して行う動作
			// ・配列を固定長(SizeConst)に切り詰めてアンマネージ構造体に埋め込む。
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] BArray;                                   // 4  byte (BYTE[SizeConst])

			public byte B2;                                         // 1  byte
																	// 1  byte (Padding)
			public ushort W2;                                       // 2  byte

																	// 4  byte (Padding)
		}

		/// <summary>
		/// エントリーポイント
		/// </summary>
		static void Main(string[] args)
		{
			var srcData = new BinaryData();
			srcData.LPTText = "０１２３４５";
			srcData.B1 = 0x12;
			srcData.W1 = 0xFF34;
			srcData.DW1 = 0xFFFFFF56;
			srcData.ByValText = "あいう9";
			srcData.BArray = new byte[6] { 0xFF, 0xFF, 0xFF, 0x11, 0x22, 0x33 };
			srcData.B2 = 0x78;
			srcData.W2 = 0xFF99;
			Console.WriteLine("変換前LP文字列: {0}", srcData.LPTText);
			Console.WriteLine("変換前ByVal文字列: {0}", srcData.ByValText);

			var buffer1 = StructureToBytes(srcData);
			Console.WriteLine("変換後の構造体サイズ： {0}", buffer1.Length);
			foreach (var b in buffer1)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.WriteLine();
			var dstData = (BinaryData)BytesToStructure(buffer1, typeof(BinaryData));
			DestroyBytes(buffer1, typeof(BinaryData));
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
			DestroyBytes(buffer2, typeof(BinaryData));
			Console.WriteLine("復元データ解放：");
			foreach (var b in buffer2)
			{
				Console.Write("{0:X2} ", b);
			}

			Console.ReadKey();
		}

		/// <summary>
		/// マネージ構造体からアンマネージ構造体を生成する
		/// ※アンマネージ構造体にポインタ参照（アンマネージメモリ）を含んでいる場合は、
		/// 　Marshal.DestroyStructureメソッドで開放しなければメモリリークする。
		/// </summary>
		private static byte[] StructureToBytes(object structure)
		{
			int size = Marshal.SizeOf(structure);
			var buffer = new byte[size];

			var gch = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			try
			{
				var ptr = gch.AddrOfPinnedObject();
				Marshal.StructureToPtr(structure, ptr, false);
			}
			finally
			{
				gch.Free();
			}

			return buffer;
		}

		/// <summary>
		/// アンマネージ構造体からマネージ構造体を生成する
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
		/// アンマネージ構造体上のアンマネージメモリを開放する
		/// </summary>
		private static void DestroyBytes(byte[] buffer, Type structureType)
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
