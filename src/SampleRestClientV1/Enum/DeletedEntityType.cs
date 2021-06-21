namespace SampleRestClientV1.Enum
{
	/// <summary>
	/// An enum to represent sync/Deleted Endpoint Entity Type field.
	/// Refer to: https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx?name=SyncDeleted
	/// </summary>
	public enum DeletedEntityType
	{
		TransactionLines = 1,
		Accounts = 2,
		Addresses = 3,
		Attachments = 4,
		Contacts = 5,
		Documents = 6,
		GLAccounts = 7,
		SalesItemPrices = 8,
		Items = 9,
		PaymentTerms = 10,
		Quotations = 11,
		SalesOrders = 12,
		SalesInvoices = 13,
		TimeCostTransactions = 14
	}
}
