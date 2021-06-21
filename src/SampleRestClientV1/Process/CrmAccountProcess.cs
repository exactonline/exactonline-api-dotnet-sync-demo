using RestSharp;
using SampleRestClientV1.Entity.CRMAccounts;
using SampleRestClientV1.Table;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleRestClientV1.Process
{
	/// <summary>
	/// Processing of the result for sync/CRM/Account endpoint
	/// </summary>
	class CrmAccountProcess : ProcessBase
	{
		private Database _database { get; set; }
		private Logger _logger { get; set; }
		private long _currentTimestamp { get; set; }

		public CrmAccountProcess(EOLSettings eolSettings, Logger logger)
			: base(eolSettings)
		{
			_database = new Database();
			_logger = logger;
			_currentTimestamp = eolSettings.CrmAccountTimestamp;
		}

		public override void Execute()
		{
			var request = new RestRequest();
			request.RootElement = "d";

			// Get records which have not been processed, based on last processed timestamp
			// Select only the fields which are needed
			request.Resource = $"sync/CRM/Accounts?$filter=Timestamp gt { _currentTimestamp }L&$select=ID,Code,Name,Website,StartDate,EndDate,Timestamp";

			_logger.AddToLog($"Calling { BaseUri }{ request.Resource }");
			_logger.AddToLog("Get first set");

			var dataSetResponse = ClientExecute<DataSet>(request);
			if (dataSetResponse.StatusCode != System.Net.HttpStatusCode.OK)
			{
				throw new InvalidOperationException();
			}

			var dataSet = dataSetResponse.Data;

			if (dataSet.results != null && dataSet.results.Count() > 0)
			{
				_logger.AddToLog("Process first set");
				SaveToDatabase(dataSet.results);

				while (!String.IsNullOrEmpty(dataSet.__next))
				{
					_logger.AddToLog("Get next set");
					request.Resource = dataSet.__next.Replace(BaseUri, "");
					dataSetResponse = ClientExecute<DataSet>(request);
					dataSet = dataSetResponse.Data;

					_logger.AddToLog("Process next set");
					SaveToDatabase(dataSet.results);
				}

				_logger.AddToLog("All sets retrieved and processed");

				// The current timestamp must be saved to settings file, so that the next processing will start from this timestamp.
				EolSettings.CrmAccountTimestamp = _currentTimestamp;
				EolSettings.SaveSettings();
			}
			else
			{
				_logger.AddToLog("No record to process");
			}
		}

		private void SaveToDatabase(List<Result> data)
		{
			if (data == null)
			{
				return;
			}

			foreach (Result account in data)
			{
				// Check if record exist
				var record = _database.CrmAccounts.SingleOrDefault(a => a.ID == account.ID);

				if (record == null)
				{
					AddCrmAccount(account);
				}
				else
				{
					// Only update the records when the received record is newer than the existing one.
					if (account.Timestamp > record.Timestamp)
					{
						UpdateCrmAccount(account, record);
					}
					else
					{
						SkipCrmAccount(account);
					}
				}

				// Store the largest timestamp received from endpoint.
				_currentTimestamp = (account.Timestamp > _currentTimestamp) ? account.Timestamp : _currentTimestamp;
			}

			_database.SaveChanges();
		}

		private void AddCrmAccount(Result account)
		{
			_logger.AddToLog($"Add account '{account.ID}'");

			var record = new CrmAccount();
			CopyValue(account, record);

			_database.CrmAccounts.Add(record);
		}

		private void UpdateCrmAccount(Result account, CrmAccount record)
		{
			_logger.AddToLog($"Update account '{account.ID}'");

			CopyValue(account, record);
		}

		private void SkipCrmAccount(Result account)
		{
			_logger.AddToLog($"Skipped account '{account.ID}'");
		}

		private void CopyValue(Result account, CrmAccount record)
		{
			record.ID = account.ID;
			record.Code = account.Code?.Trim();
			record.Name = account.Name;
			record.Website = account.Website;
			record.StartDate = account.StartDate;
			record.EndDate = account.EndDate;
			record.Timestamp = account.Timestamp;
		}
	}
}
