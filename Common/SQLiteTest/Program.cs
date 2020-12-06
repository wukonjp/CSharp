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

		static void Main(string[] args)
		{
			if (File.Exists(DataFileName))
			{
				File.Delete(DataFileName);
			}

			var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = DataFileName };

			using (var cn = new SQLiteConnection(sqlConnectionSb.ToString()))
			{
				cn.Open();

				using (var cmd = new SQLiteCommand(cn))
				{
					// sequences テーブル作成
					cmd.CommandText =
						"CREATE TABLE IF NOT EXISTS sequences(" +
							"sequence_id INTEGER," +
							"mmsi INTEGER," +
							"loa INTEGER," +
							"PRIMARY KEY(sequence_id)" +
						")";
					cmd.ExecuteNonQuery();

					// logs テーブル作成
					cmd.CommandText =
						"CREATE TABLE IF NOT EXISTS logs (" +
							"scan_time TEXT," +
							"lat INTEGER," +
							"lon INTEGER," +
							"sequence_id INTEGER" +
						")";
					cmd.ExecuteNonQuery();

					//データ追加
					{
						var transaction = cn.BeginTransaction();
						try
						{
							var random = new Random();

							// sequences
							for (int i = 0; i < 4000; i++)
							{
								var mmsi = random.Next() % 999999999;
								var loa = random.Next() % 999;

								cmd.CommandText =
								string.Format("REPLACE INTO sequences VALUES ({0},{1},{2})",
									i,
									mmsi,
									loa
								);
								cmd.ExecuteNonQuery();
							}

							// logs
							for (int i = 0; i < 1000000; i++)
							{
								var scanTime = DateTime.Now + TimeSpan.FromMinutes(i);
								var lat = random.Next();
								var lon = random.Next();
								var sequence_id = random.Next() % 3999;

								cmd.CommandText =
								string.Format("INSERT INTO logs VALUES ('{0}',{1},{2},{3})",
									scanTime,
									lat,
									lon,
									sequence_id
								);
								cmd.ExecuteNonQuery();
							}

							transaction.Commit();
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
			}

			Console.ReadKey();
		}
	}
}
