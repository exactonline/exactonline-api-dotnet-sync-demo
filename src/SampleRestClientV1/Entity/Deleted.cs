using System;
using System.Collections.Generic;

namespace SampleRestClientV1.Entity.Deleted
{
	/// <summary>
	/// These classes represents the data model stucture of sync/Deleted Endpoint.
	/// Refer to: https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx?name=SyncDeleted
	/// </summary>
	public class Metadata
	{
		public string uri { get; set; }
		public string type { get; set; }
	}

	public class Result
	{
		public Metadata __metadata { get; set; }
		public Guid EntityKey { get; set; }
		public int Division { get; set; }
		public int EntityType { get; set; }
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
