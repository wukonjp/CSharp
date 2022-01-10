using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;

namespace SQLiteTest
{
	class Program
	{
		const string DataFileName = "test.db";
		const int FieldCount = 200;
		const int RecordCount = 200000;

		static string[] fieldLabels = new string[FieldCount];

		static void Main(string[] args)
		{
			for (int i = 0; i < FieldCount; i++)
			{
				fieldLabels[i] = string.Format("column{0}", i);
			}

			if (File.Exists(DataFileName))
			{
				File.Delete(DataFileName);
			}

			var scsb = new SQLiteConnectionStringBuilder
			{
				DataSource = DataFileName,
				//UseUTF16Encoding = true,
			};

			using (var connection = new SQLiteConnection(scsb.ToString()))
			{
				connection.Open();

				using (var cmd = new SQLiteCommand(connection))
				{
					// テーブル作成
					{
						var sb = new StringBuilder();
						for (int i = 0; i < FieldCount; i++)
						{
							sb.AppendFormat("{0} TEXT, ", fieldLabels[i]);
						}
						var fields = sb.ToString();
						cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS body(id INTEGER, {0}PRIMARY KEY(id))", fields);
						cmd.ExecuteNonQuery();
					}

					//データ追加
					var transaction = connection.BeginTransaction();
					try
					{
						var stopwatch = Stopwatch.StartNew();

						var random = new Random();
						for (int r = 0; r < RecordCount; r++)
						{
							var sb = new StringBuilder();
							sb.AppendFormat("{0}, ", (r + 1).ToString());
							for (int i = 0; i < FieldCount; i++)
							{
								sb.AppendFormat("'行{0:000000000} 列{1:0000000000} AABBCCDDEEFF'", r, i);
								//sb.AppendFormat("NULL");
								//sb.AppendFormat("''");
								if (i != (FieldCount - 1))
								{
									sb.Append(',');
								}
							}

							var fields = sb.ToString();
							cmd.CommandText = string.Format("REPLACE INTO body VALUES ({0})", fields);
							cmd.ExecuteNonQuery();

							if ((r % 10000) == 0)
							{
								Console.WriteLine("{0}: Elapsed {1} msec", r, stopwatch.ElapsedMilliseconds);
							}
						}

						transaction.Commit();

						Console.WriteLine("Complete. Elapsed {0} msec", stopwatch.ElapsedMilliseconds);
					}
					catch
					{
						transaction.Rollback();
					}
					finally
					{
						transaction.Dispose();
					}
				}
			}

			Console.ReadKey();
		}
	}
}
