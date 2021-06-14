using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class HtmlRegExImageFinder : IHtmlImageFinder
{
    private List<string> _listOfImageUrls = new List<string>();

    private string _regExPattern;

    public HtmlRegExImageFinder(string regExPattern)
    {
        _regExPattern = regExPattern;
    }

    public List<string> FindImages(string content)
	{
        // Define a regular expression for repeated words.
        Regex rx = new Regex(_regExPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Find matches.
        MatchCollection matches = rx.Matches(content);

        // Report on each match.
        foreach (Match match in matches)
        {
            GroupCollection groups = match.Groups;
            _listOfImageUrls.Add(groups[1].Value);
        }

        return _listOfImageUrls;
    }
}