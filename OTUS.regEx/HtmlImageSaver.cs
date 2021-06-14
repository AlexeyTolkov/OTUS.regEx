using System.IO;
using System.Net;

class HtmlImageSaver
{
	private readonly IHtmlImageFinder _htmlImageFinder;
	private readonly IContentProvider _contentProvider;

	public HtmlImageSaver(IHtmlImageFinder htmlImageFinder, IContentProvider contentProvider)
	{
		_htmlImageFinder = htmlImageFinder;
		_contentProvider = contentProvider;
	}

	public void SaveImages(string url, string destinationPath)
	{
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
		var localPath = destinationPath + "\\" + fileName;

		//Download file from remote url and save it in destination dir
		using (WebClient webClient = new WebClient())
		{
			webClient.DownloadFile(remoteFileUrl, localPath);
		}
	}
}