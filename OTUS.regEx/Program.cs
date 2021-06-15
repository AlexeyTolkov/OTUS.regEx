using System;
using System.IO;

namespace OTUS.regEx
{
    class Program
    {
        static void Main(string[] args)
        {
			var htmlRegExImageFinder = new HtmlRegExImageFinder(@"<img.*?src=""(http.*?)""");
			var contentProvider = new ContentProvider();
			var htmlImageSaver = new HtmlImageSaver(htmlRegExImageFinder, contentProvider);

			var destinationDir = Path.Combine(Environment.CurrentDirectory, "images");
			var webSite = "https://stocksnap.io/";
			htmlImageSaver.SaveImages(webSite, destinationDir);
		}
    }
}