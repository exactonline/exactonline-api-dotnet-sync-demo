# Introduction 
This sample program demonstrates how to use EOL Sync APIs. It is a console application that extracts data from EOL through sync/CRM/Accounts and sync/Deleted endpoints.

API References:
- [sync/CRM/Accounts](https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx?name=SyncCRMAccounts)
- [sync/Deleted](https://start.exactonline.nl/docs/HlpRestAPIResourcesDetails.aspx?name=SyncDeleted)
- [Other Sync APIs](https://start.exactonline.nl/docs/HlpRestAPIResources.aspx?SourceAction=10)

# Getting Started
1. Create database used for this sample by executing SQL Scripts under Database scripts folder.
2. Open up App.config file, and modify connection string to point to the newly created database.
3. Configure Settings file but providing your OAuth client details. If you modify a value, make sure the attribute encrypted="0".
4. Compile and execute the program.