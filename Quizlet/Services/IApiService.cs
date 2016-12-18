using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Quizlet.Models;

namespace Quizlet.Services
{
    /// <summary>
    /// Interface for Quizlet API.
    /// </summary>
    public interface IApiService
    {
        /// <summary>
        /// Send a GET request to the server.
        /// </summary>
        Task<QuizletResponse> GetRequest(string method);

        /// <summary>
        /// Send a POST request to the server.
        /// </summary>
        Task<AuthToken> PostRequest(Dictionary<string, string> data, string method, params object[] param);
    }
}
