using SampleRestClientV1.Enum;
using System;
using System.Collections.Generic;
using System.IO;

namespace SampleRestClientV1
{
	/// <summary>
	/// Tool to log messages
	/// </summary>
	class Logger
	{
		private List<string> Log { get; set; }
		public DateTime? LastUpdate { get; set; }
		public StreamWriter fileLog = null;

		public Logger()
		{
			Log = new List<string>();
		}

		public void AddToLog(string s, LogDestination logDest = LogDestination.Both)
		{
			string s1 = $"[{ DateTime.Now }] { s }";

			if (logDest == LogDestination.Console || logDest == LogDestination.Both)
			{
				Console.WriteLine(s1);
			}
			if (logDest == LogDestination.File || logDest == LogDestination.Both)
			{
				Log.Add(s1);

				if (fileLog == null)
				{
					File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".log"));
					fileLog = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".log"));
					fileLog.AutoFlush = true;
				}

				fileLog.WriteLine(s1);
			}
		}

		public string ExceptionHandler(Exception ex)
		{
			string s = ex.Message + System.Environment.NewLine + ex.StackTrace;

			if (ex.InnerException != null)
			{
				s = s + System.Environment.NewLine + " *** " + ExceptionHandler(ex.InnerException);
			}

			return s;
		}
	}

}
