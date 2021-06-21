using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleRestClientV1.Table
{
	/// <summary>
	/// These classes represents the data model stucture of database table.
	/// Use Sql scripts provided (under Database Scripts folder) to create database used for this sample
	/// </summary>
	[Table("CrmAccount")]
	public class CrmAccount
	{
		[Key]
		public Guid ID { get; set; }

		[MaxLength(200)]
		public string Code { get; set; }

		[MaxLength(500)]
		public string Name { get; set; }

		[MaxLength(1000)]
		public string Website { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public long Timestamp { get; set; }

	}
}
