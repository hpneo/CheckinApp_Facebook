using System;

using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace CheckinApp_Facebook
{
	public class Facebook
	{
		public string AppToken { get; set; }
		public string AppSecret { get; set; }
		public string UserToken { get; set; }

		public Facebook (string appToken, string appSecret)
		{
			AppToken = appToken;
			AppSecret = appSecret;
		}

		async public Task<string> publishFeed(string message) {
			string url = "https://graph.facebook.com/v2.1/me/feed?access_token=" + UserToken;
			string content = "access_token=" + UserToken;

			content = "message=" + message;
			content += "&link=https://www.themoviedb.org/movie/10658-howard-the-duck";
			content += "&picture=https://image.tmdb.org/t/p/original/gEaC5qL3Q6LDb9XS0Rp27hwoglm.jpg";
			content += "&name=Howard the Duck";
			content += "&caption=1986";
			content += "&description=A scientific experiment unknowingly brings extraterrestrial life forms to the Earth through a laser beam. First is the cigar smoking drake Howard from the duck's planet. A few kids try to keep him from the greedy scientists and help him back to his planet. But then a much less friendly being arrives through the beam...";
			content += "&privacy[value]=SELF";

			Console.WriteLine (url);
			Console.WriteLine (content);

			byte[] byteContent = Encoding.UTF8.GetBytes(content);

			WebRequest request = WebRequest.Create (url);
			request.UseDefaultCredentials = true;

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = byteContent.Length;

			Task<string> response = null;

			try {
				System.IO.Stream stream = await request.GetRequestStreamAsync ().ConfigureAwait (false);
				stream.Write (byteContent, 0, byteContent.Length);
				stream.Close ();

				WebResponse webResponse = await request.GetResponseAsync ();
				System.IO.StreamReader requestHeader = new System.IO.StreamReader (webResponse.GetResponseStream ());
				response = requestHeader.ReadToEndAsync ();
				webResponse.Close ();
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

			return response.Result;
		}
	}
}

