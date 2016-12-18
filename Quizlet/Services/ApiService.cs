using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quizlet.Enums;
using Quizlet.Models;

namespace Quizlet.Services
{
    /// <summary>
    /// Implementation of Quizlet API service.
    /// </summary>
    public class ApiService : IApiService
    {
        #region Private fields.

        private ISettingsService settingsService;

        private string apiVersion = "2.0";
        private string baseUri = "https://api.quizlet.com/";

        #endregion

        #region Contructor

        /// <summary>
        /// Gets new intances of ApiService.
        /// </summary>
        public ApiService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        #endregion

        #region Public methods.

        /// <summary>
        /// Send a GET request to the server.
        /// </summary>
        public async Task<QuizletResponse> GetRequest(string method)
        {
            var client = new HttpClient();
            var quizletResponse = new QuizletResponse();
            try
            {
                var sb =
                    $"{this.baseUri}{this.apiVersion}/{method}?access_token={this.settingsService.GetToken().access_token}";

                var response = await client.GetStringAsync(sb);
                quizletResponse = new QuizletResponse();
                quizletResponse.IsSuccessful = true;
                quizletResponse.Content = response;
            }
            catch (Exception e)
            {
                quizletResponse.IsSuccessful = false;
                quizletResponse.ErrorMessage = e.Message;
            }
            return quizletResponse;
        }

        /// <summary>
        /// Send a POST request to the server.
        /// </summary>
        public async Task<AuthToken> PostRequest(Dictionary<string, string> data, string method, params object[] param)
        {
            var client = new HttpClient();
            //var content = new FormUrlEncodedContent(data);
            //var con = content as HttpContent;

            var sb = new StringBuilder();

            foreach (var content in data)
            {
                sb.Append(content.Key);
                sb.Append("=");
                sb.Append(content.Value + "&");
            }

            sb.Remove(sb.Length - 1, 1);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    "N2dHQXdyM2VlODp6VWhTY0FmTXVVN3k1eGtrbjlDWFY1");
                var request = new HttpRequestMessage(HttpMethod.Post, this.baseUri + method);
                request.Content = new StringContent(sb.ToString(), Encoding.UTF8,
                    "application/x-www-form-urlencoded");
                var response = await httpClient.SendAsync(request);
                var token = JsonConvert.DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());
                return token;
            }

            //throw new NotImplementedException();
        }

        #endregion
    }
}
