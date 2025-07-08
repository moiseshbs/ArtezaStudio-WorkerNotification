using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtezaStudio.WorkerNotification.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7037/");
        }

        public async Task<string?> ObterEmailAutorPorPublicacaoId(Guid publicacaoId)
        {
            var response = await _httpClient.GetAsync($"api/Publicacao/{publicacaoId}/autor/email");

            if (!response.IsSuccessStatusCode)
                return null;

            var email = await response.Content.ReadAsStringAsync();
            return email;
        }
    }
}
