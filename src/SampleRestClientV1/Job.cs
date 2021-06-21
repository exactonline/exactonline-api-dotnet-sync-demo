using SampleRestClientV1.Process;
using System;

namespace SampleRestClientV1
{
	/// <summary>
	/// A job to intiate Sync APIs processing in sequence
	/// </summary>
	public class Job
	{
		private Logger _logger { get; set; }
		private EOLSettings _eolSettings { get; set; }

		public Job()
		{
			_logger = new Logger();
			_eolSettings = new EOLSettings();
		}

		public void Execute()
		{
			try
			{
				_logger.AddToLog($"Start process for division {_eolSettings.Division}");

				var crmAccountProcess = new CrmAccountProcess(_eolSettings, _logger);
				crmAccountProcess.Execute();

				var deletedProcess = new DeletedProcess(_eolSettings, _logger);
				deletedProcess.Execute();

				_logger.AddToLog($"Finished for division {_eolSettings.Division}");
			}
			catch (Exception ex)
			{
				Console.WriteLine("");
				_logger.AddToLog(_logger.ExceptionHandler(ex));
			}
		}
	}
}
