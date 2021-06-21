using RestSharp;
using SampleRestClientV1.Entity.Deleted;
using SampleRestClientV1.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleRestClientV1.Process
{
	/// <summary>
	/// Processing of the result for sync/Deleted endpoint
	/// </summary>
	class DeletedProcess : ProcessBase
	{
		private Database _database { get; set; }
		private Logger _logger { get; set; }
		private long _currentTimestamp { get; set; }

		public DeletedProcess(EOLSettings eolSettings, Logger logger)
			: base(eolSettings)
		{
			_database = new Database();
			_logger = logger;
			_currentTimestamp = eolSettings.DeletedTimestamp;
		}

		public override void Execute()
		{
			var request = new RestRequest();
			request.RootElement = "d";

			// Get records which have not been processed, based on last processed timestamp
			// Returns all entity types of deleted records
			// Select only the fields which are needed
			request.Resource = $"sync/Deleted?$filter=Timestamp gt { _currentTimestamp }L and EntityType eq 2&$select=EntityKey,EntityType,Timestamp";

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
				EolSettings.DeletedTimestamp = _currentTimestamp;
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

			foreach (var deleted in data)
			{
				switch (deleted.EntityType)
				{
					// Delete records based on Entity Type
					case (int)DeletedEntityType.Accounts:
						DeleteCRMAccount(deleted);
						break;
					// TO-DO: Continue implementation for other entity types
				}
			}

			_database.SaveChanges();
		}

		private void DeleteCRMAccount(Result deleted)
		{
			// Only delete the records when the received timestamp is larger than the current one.
			var record = _database.CrmAccounts.SingleOrDefault(a => a.ID == deleted.EntityKey && a.Timestamp <= deleted.Timestamp);

			if (record != null)
			{
				_database.CrmAccounts.Remove(record);
			}

			// Store the largest timestamp received from endpoint.
			_currentTimestamp = (deleted.Timestamp > _currentTimestamp) ? deleted.Timestamp : _currentTimestamp;
		}
	}
}
