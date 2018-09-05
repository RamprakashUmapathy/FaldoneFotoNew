using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web
{
    public class ImagesController : ApiController
    {

        // GET api/images/ABA000001RSS
        public async Task<HttpResponseMessage> Get(string id)
        {
            return await Get(id, 0);
        }

        // GET api/images/ABA000001RSS/0
        public async Task<HttpResponseMessage> Get(string id,byte islarge)
        {
            string urlBase = ConfigurationManager.AppSettings["WebServerFotoURL"].ToString();
            string url = urlBase.Replace("#ARTICOLO#", id);
            url = string.Concat(url, "&", islarge == 0 ? "" : "UseNoThumb=1");
            using (HttpClient client = new HttpClient())
            {
                var r = await client.GetAsync(url);
                r.EnsureSuccessStatusCode();
                byte[] imageBytes = await r.Content.ReadAsByteArrayAsync();
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imageBytes)
                };
                string mediaType = r.Content.Headers.ContentType.MediaType;
                string ext = mediaType.Split('/')[1];
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = $"{id}.{ext}"
                };
                return result;
            }
        }


    }
}