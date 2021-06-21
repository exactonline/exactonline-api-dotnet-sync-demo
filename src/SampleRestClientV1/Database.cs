using SampleRestClientV1.Table;
using System.Data.Entity;

namespace SampleRestClientV1
{
	/// <summary>
	/// DbContext to connect to database for this sample
	/// </summary>
	class Database : DbContext
	{
		public Database() : base("DefaultConnection")
		{
		}

		public IDbSet<CrmAccount> CrmAccounts { get; set; }
	}
}
