using System;
using System.Collections.Generic;

namespace SampleRestClientV1.Entity.CRMAccounts
{
	/// <summary>
	/// These classes represents the data model stucture of sync/CRM/Accounts Endpoint.
	/// Refer to: https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx?name=SyncCRMAccounts
	/// </summary>
	public class Metadata
	{
		public string uri { get; set; }
		public string type { get; set; }
	}

	public class Result
	{
		public Metadata __metadata { get; set; }
		public Guid ID { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Website { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public long Timestamp { get; set; }
	}

	public class DataSet
	{
		public List<Result> results { get; set; }
		public string __next { get; set; }
	}

	public class RootObject
	{
		public DataSet dataSet { get; set; }
	}
}
