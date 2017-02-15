using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using EventCaptureApp.Enums;

namespace EventCaptureApp.Services
{
	public class RestService
	{
		private static RestService _instance;
		private static HttpClient _httpClient;

		public static RestService Instance {
			get {
				if (_instance == null) {
					_instance = new RestService ();
					_httpClient = new HttpClient();
					_httpClient.DefaultRequestHeaders.Accept.Clear();
					_httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
					_httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(Language.EnglishUS));
					_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
					_httpClient.MaxResponseContentBufferSize = 256000;
				}
				return _instance;
			}
		}

		public async Task<RestResponse> ExecRequest(string requestUrl, object postData = null)
		{
			HttpResponseMessage httpResponse;
			RestResponse restResponse = new RestResponse();
			if (postData == null)
			{
				httpResponse = await _httpClient.GetAsync(requestUrl);
			}
			else {
				StringContent postContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
				httpResponse = await _httpClient.PostAsync(requestUrl, postContent);
			}
			restResponse.RequestSuccess = httpResponse.IsSuccessStatusCode;
			restResponse.StatusCode = httpResponse.StatusCode;
			if (restResponse.RequestSuccess)
				restResponse.Content = await httpResponse.Content.ReadAsStringAsync ();
			return restResponse;
		}
	}

	public class RestResponse
	{
		public bool RequestSuccess { get; set; } = false;

		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

		public string Content { get; set; } = string.Empty;
	}
}

