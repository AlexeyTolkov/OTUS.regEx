using System.IO;
using System.Net;

class ContentProvider : IContentProvider
{
	public string GetPageContent(string url)
	{
		var request = WebRequest.Create(url);
		using (var response = (HttpWebResponse)request.GetResponse())
		using (var stream = response.GetResponseStream())
		using (var reader = new StreamReader(stream))
		{
			return reader.ReadToEnd();
		}
	}
}