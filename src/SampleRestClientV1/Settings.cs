using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SampleRestClientV1
{
	/// <summary>
	/// Read and write settings from Settings.xml
	/// </summary>
	public class EOLSettings
	{
		public XDocument settingsXML { get; set; }

		public string Code { get; private set; }
		public Uri Uri { get; private set; }
		public int Division { get; private set; }
		public string ClientID { get; private set; }
		public string ClientSecret { get; private set; }
		public Uri Callback { get; private set; }
		public string RefreshToken { get; set; }
		public string AccessToken { get; set; }
		public DateTime? AccessTokenExpirationUtc { get; set; }
		public long CrmAccountTimestamp { get; set; }
		public long DeletedTimestamp { get; set; }


		public EOLSettings()
		{
			ReadSettings();
		}

		private void ReadSettings()
		{
			string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");

			settingsXML = XDocument.Load(settingsFile);

			var envTag = settingsXML.Descendants("Environment").First();
			var apisTag = settingsXML.Descendants("Api");
			var crmAccountTimestampElement = apisTag.Single(a => a.Element("Name").Value == "CrmAccount").Elements("Timestamp").Single();
			var deletedTimestampElement = apisTag.Single(a => a.Element("Name").Value == "Deleted").Elements("Timestamp").Single();

			Code = (string)envTag.Element("Code");
			Uri = new Uri((string)envTag.Element("Uri"));
			Division = (int)envTag.Element("Division");

			ClientID = DecryptElement(envTag.Element("ClientID"));
			ClientSecret = DecryptElement(envTag.Element("ClientSecret"));
			Callback = new Uri(DecryptElement(envTag.Element("CallbackUri")));
			ClientID = DecryptElement(envTag.Element("ClientID"));
			RefreshToken = DecryptElement(envTag.Element("RefreshToken"));

			CrmAccountTimestamp = (long)crmAccountTimestampElement;
			DeletedTimestamp = (long)deletedTimestampElement;
		}

		public void SaveSettings()
		{
			string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings.xml");

			var envTag = settingsXML.Descendants("Environment").First();
			var apisTag = settingsXML.Descendants("Api");
			var crmAccountTimestampElement = apisTag.Single(a => a.Element("Name").Value == "CrmAccount").Elements("Timestamp").Single();
			var deletedTimestampElement = apisTag.Single(a => a.Element("Name").Value == "Deleted").Elements("Timestamp").Single();

			EncryptElement(ClientID, envTag.Element("ClientID"));
			EncryptElement(ClientSecret, envTag.Element("ClientSecret"));
			EncryptElement(Callback.AbsoluteUri, envTag.Element("CallbackUri"));
			EncryptElement(RefreshToken, envTag.Element("RefreshToken"));

			crmAccountTimestampElement.Value = CrmAccountTimestamp.ToString();
			deletedTimestampElement.Value = DeletedTimestamp.ToString();

			settingsXML.Save(settingsFile);

		}

		private string DecryptElement(XElement envTag)
		{
			string value = (string)envTag;

			if ((int)envTag.Attribute("encrypted") == 1)
			{
				return Crypto.Decrypt(value, Constant.ENCRYPTION_PASSWORD);
			}

			return value;
		}

		private static void EncryptElement(string value, XElement element)
		{
			element.Value = Crypto.Encrypt(value, Constant.ENCRYPTION_PASSWORD);
			element.Attribute("encrypted").SetValue(1);
		}
	}
}


