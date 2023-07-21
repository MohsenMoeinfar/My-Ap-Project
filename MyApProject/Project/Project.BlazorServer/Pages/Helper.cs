using System.Net.Http;

namespace Example
{
    public class ApiClient
    {
        public static HttpClient HttpClient = new HttpClient();
        public swaggerClient Client = new swaggerClient("http://localhost:5000/", HttpClient);
    }
}