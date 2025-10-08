namespace SciTrack.web.Services
{
    public class TaiSanApi
    {
        private readonly HttpClient _http;
        public TaiSanApi(IHttpClientFactory f) => _http = f.CreateClient("api");
        public async Task<List<dynamic>?> GetListAsync() =>
            await _http.GetFromJsonAsync<List<dynamic>>("/api/v1/tskhcn");
    }
}
