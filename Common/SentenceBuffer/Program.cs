using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentenceBuffer
{
	/// <summary>
	/// センテンスを疑似送受信して復元する
	/// </summary>
	class Program
	{
		/// <summary>
		/// パケットサイズ
		/// </summary>
		const int PacketMax = 7;

		/// <summary>
		/// エントリーポイント
		/// </summary>
		static void Main(string[] args)
		{
			// 送信文字列
			var sendText = "あいうえお\r\n1\r\nかきくけこ\r\n\r\n22\r\nさしすせそ\r\n3";

			// 疑似送信
			Console.WriteLine("送信：");
			Console.WriteLine("「{0}」", sendText);
			var sendBuffer = Encoding.UTF8.GetBytes(sendText);

			// 疑似受信（パケット分割）
			var packetList = CreatePacketList(sendBuffer);
			Console.WriteLine("\r\n受信： パケット数{0}", packetList.Count);
			foreach (var packet in packetList)
			{
				Console.WriteLine(packet.Length);
			}

			// 復元1: NG
			{
				Console.WriteLine("\r\n復元1：　パケット文字列化 → 文字列連結");
				var sb = new StringBuilder();
				foreach (var packet in packetList)
				{
					sb.Append(Encoding.UTF8.GetString(packet));
				}

				var restoredText = sb.ToString();
				Console.WriteLine("「{0}」", restoredText);
			}

			// 復元2: GOOD
			{
				Console.WriteLine("\r\n復元2：　パケット連結 → 文字列化");
				var readBuffer = Join(packetList);

				var restoredText = Encoding.UTF8.GetString(readBuffer);
				Console.WriteLine("「{0}」", restoredText);
			}

			// 復元3: GOOD
			{
				Console.WriteLine("\r\n復元3：　パケット連結 → CRLF分割 → 文字列化");
				var readBuffer = Join(packetList);

				byte[] rest;
				var sentenceList = SplitByCRLF(readBuffer, Encoding.UTF8, out rest);
				foreach (var sentence in sentenceList)
				{
					Console.WriteLine("「{0}」", sentence);
				}

				if (rest != null)
				{
					Console.WriteLine("余りバイト数： {0}", rest.Length);
				}
			}

			Console.ReadKey();
		}

		/// <summary>
		/// パケットリストを作成する
		/// </summary>
		private static List<byte[]> CreatePacketList(byte[] sendBuffer)
		{
			var packetList = new List<byte[]>();
			for (int i = 0; i < sendBuffer.Length; i += PacketMax)
			{
				int blockSize = sendBuffer.Length - i;
				if (blockSize > PacketMax)
				{
					blockSize = PacketMax;
				}

				var block = new byte[blockSize];
				Buffer.BlockCopy(sendBuffer, i, block, 0, blockSize);
				packetList.Add(block);
			}

			return packetList;
		}

		/// <summary>
		/// パケットを連結する
		/// </summary>
		private static byte[] Join(List<byte[]> packetList)
		{
			int total = 0;
			foreach (var packet in packetList)
			{
				total += packet.Length;
			}

			var buffer = new byte[total];
			int index = 0;
			foreach (var packet in packetList)
			{
				Buffer.BlockCopy(packet, 0, buffer, index, packet.Length);
				index += packet.Length;
			}

			return buffer;
		}

		/// <summary>
		/// バイト配列をCRLFで分割する
		/// </summary>
		private static List<string> SplitByCRLF(byte[] buffer, Encoding encoding, out byte[] rest)
		{
			rest = null;
			var list = new List<string>();

			for (int currentIndex = 0; currentIndex < buffer.Length; )
			{
				int foundIndex = FindCRLF(buffer, currentIndex);
				if (foundIndex < 0)
				{                                                           // CRLF無し
					int size = buffer.Length - currentIndex;
					rest = new byte[size];
					Buffer.BlockCopy(buffer, currentIndex, rest, 0, size);
					break;
				}
				else
				{															// CRLF有り
					int size = foundIndex - currentIndex;
					var text = encoding.GetString(buffer, currentIndex, size);
					list.Add(text);
					currentIndex += size + 2;
				}
			}

			return list;
		}

		/// <summary>
		/// バイト配列内でCRLFを検索してインデックスを返す
		/// </summary>
		private static int FindCRLF(byte[] buffer, int startIndex)
		{
			unsafe
			{
				fixed (Byte* pHead = buffer)
				{
					int max = buffer.Length - 1;
					for (int i = startIndex; i < max; i++)
					{
						var p = (UInt16*)(pHead + i);
						if (*p == 0x0A0D)
						{													// CRLF有り
							return i;
						}
					}

					return -1;
				}
			}
		}
	}
}
