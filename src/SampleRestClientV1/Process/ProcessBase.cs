using DotNetOpenAuth.OAuth2;
using RestSharp;
using System;
using System.Net;

namespace SampleRestClientV1.Process
{
	/// <summary>
	/// Base class for the processing of sync endpoints
	/// </summary>
	abstract class ProcessBase
	{
		protected string BaseUri { get; set; }
		protected EOLSettings EolSettings { get; set; }
		private RestClient _client { get; set; }

		public ProcessBase(EOLSettings eolSettings)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			this.EolSettings = eolSettings;

			this.BaseUri = $"{ eolSettings.Uri }/api/v1/{ EolSettings.Division }";

			_client = new RestClient(BaseUri);
			_client.ClearHandlers();
			_client.AddHandler("application/json", new RestSharp.Serialization.Json.JsonDeserializer());
		}

		/// <summary>
		/// Start the execution of process
		/// </summary>
		public abstract void Execute();

		/// <summary>
		/// Execution of the given request
		/// </summary>
		protected IRestResponse<T> ClientExecute<T>(RestRequest request)
		{
			// Check whether token is empty or expired before using it.
			if (IsTokenEmptyOrExpired())
			{
				// Token is empty or expired, so get a new one.
				GetSetNewOAuthTokens();
			}
			_client.Authenticator = new RestSharp.Authenticators.OAuth2AuthorizationRequestHeaderAuthenticator(EolSettings.AccessToken, "Bearer");
			return _client.Execute<T>(request);
		}

		/// <summary>
		/// Check expiry of current token
		/// </summary>
		private bool IsTokenEmptyOrExpired()
		{
			return string.IsNullOrWhiteSpace(EolSettings.AccessToken) || EolSettings.AccessTokenExpirationUtc <= DateTime.UtcNow;
		}

		/// <summary>
		/// Retrieves new access and refresh token from the give environment and updates the settings object
		/// </summary>
		private void GetSetNewOAuthTokens()
		{
			var client = new UserAgentClient(EolSettings.Uri, new Uri($"{ EolSettings.Uri }/api/oauth2/token"), EolSettings.ClientID, EolSettings.ClientSecret);
			var authorization = new AuthorizationState();
			authorization.RefreshToken = EolSettings.RefreshToken;

			if (client.RefreshAuthorization(authorization))
			{
				EolSettings.RefreshToken = authorization.RefreshToken;
				EolSettings.AccessToken = authorization.AccessToken;
				EolSettings.AccessTokenExpirationUtc = authorization.AccessTokenExpirationUtc;
				EolSettings.SaveSettings();
			}
		}
	}
}
