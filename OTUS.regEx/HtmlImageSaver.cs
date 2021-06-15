using System;
using System.IO;
using System.Net;

class HtmlImageSaver
{
	private readonly IHtmlImageFinder _htmlImageFinder;
	private readonly IContentProvider _contentProvider;
	private Uri _baseUrl;

	public HtmlImageSaver(IHtmlImageFinder htmlImageFinder, IContentProvider contentProvider)
	{
		_htmlImageFinder = htmlImageFinder;
		_contentProvider = contentProvider;
	}

	public void SaveImages(string url, string destinationPath)
	{
		_baseUrl = new Uri(url);

		this.createDirectoryIfNeeded(destinationPath);

		var content = _contentProvider.GetPageContent(url);
		var images = _htmlImageFinder.FindImages(content);

		foreach (var imageUrl in images)
		{
			//save to directory
			this.DownloadImage(imageUrl, destinationPath);
		}

		System.Console.WriteLine($"{images.Count} images have been saved");
	}

	private void createDirectoryIfNeeded(string destinationDir)
	{
		Directory.CreateDirectory(destinationDir);
	}

	private void DownloadImage(string remoteFileUrl, string destinationPath)
    {
		var fileName = Path.GetFileName(remoteFileUrl);
		var localPath = Path.Combine(destinationPath, fileName);
		Uri imageUrl;

		//Download file from remote url and save it in destination dir
		using (WebClient webClient = new WebClient())
		{
			imageUrl = new Uri(remoteFileUrl, UriKind.RelativeOrAbsolute);
			if (!imageUrl.IsAbsoluteUri)
			{
				//if it's relative uri then convert it to absolute
				if (Uri.TryCreate(_baseUrl, remoteFileUrl, out var absolute))
				{ 
					remoteFileUrl = absolute.ToString();
				}
			}

			webClient.DownloadFile(remoteFileUrl, localPath);
		}
	}
}