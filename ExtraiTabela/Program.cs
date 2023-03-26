using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        string url = "https://www.serebii.net/attackdex-rby/type/electric.shtml";
        string csvPath = @"E:\tabela.csv";

        WebClient client = new();
        string html = client.DownloadString(url);

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            foreach (HtmlNode table in tables)
            {
                HtmlNodeCollection rows = table.SelectNodes(".//tr");
                foreach (HtmlNode row in rows)
                {
                    HtmlNodeCollection cells = row.SelectNodes(".//td|.//th");
                    foreach (HtmlNode cell in cells)
                    {
                        writer.Write("\"" + cell.InnerText.Trim() + "\",");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
