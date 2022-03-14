using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;

namespace Exam.Models
{
    public class ExamDefAdminViewModel
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} gerekli"), Display(Name = "Başlık"), StringLength(500, MinimumLength = 3, ErrorMessage = "{0} en az {2} en fazla {1} karakter olabilir.")]
        public string Name { get; set; }
        public string Link { get; set; }

        [Required(ErrorMessage = "{0} gerekli"), Display(Name = "Yazı"), StringLength(65000, MinimumLength = 3, ErrorMessage = "{0} en az {2} en fazla {1} karakter olabilir.")]
        public string Text { get; set; }
        public List<QuestionDefAdminViewModel> Questions { get; set; }

        public List<SelectListItem> ChoiceTypes { get; set; }

        public List<SelectListItem> NewsItems { get; set; }

        public List<SelectListItem> Links { get; set; }
        private List<SelectListItem> GetAllChoiceTypes()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "A", Value = "0" });
            items.Add(new SelectListItem { Text = "B", Value = "1" });
            items.Add(new SelectListItem { Text = "C", Value = "2" });
            items.Add(new SelectListItem { Text = "D", Value = "3" });
            return items;
        }

        [Display(Name = "Tarih")]
        public string CreatedDate { get; set; }

        public ExamDefAdminViewModel()
        {
            ChoiceTypes = GetAllChoiceTypes();
            string webPageContentString = GetWebPageContentString("https://www.wired.com/");
            SetNewsLinks(GetMostRecentLinks(webPageContentString));
            SetNewsItems(GetMostRecentNews(webPageContentString));
        }

        public static string GetContentByHeader(string link)
        {
            return GetWebParagraphContent("https://www.wired.com" + link,
                "\"body__inner-container\">",
                "More Great WIRED"
                );
        }

        public static string GetWebPageContentString(string url)
        {
            WebResponse response = null;
            StreamReader reader = null;
            string result = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            catch 
            {
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }

            return result;
        }

        public string[] GetMostRecentNews(string webPageContentString)
        {
            string[] news = GetContentStrings(webPageContentString, "SummaryCollageEightGridItemList-drfwxm kcItAB", "SummaryItemHed\">", "<", 5);
            return news;
        }

        private string[] GetMostRecentLinks(string webPageContentString)
        {
            string[] alllinks = GetContentStrings(webPageContentString, "SummaryCollageEightGridItemList-drfwxm kcItAB", "href=\"", "\"", 50);
            string[] links = new string[5];
            int i = 0;
            foreach (var link in alllinks)
            {
                if (link.Contains("author"))
                    continue;
                if (i > 0)
                {
                    if (link == links[i - 1])
                        continue;
                }
                links[i++] = link;
                if (i >= 5)
                    break;
            }
            return links;
        }

        private static string[] GetContentStrings(string webPageContentString, string itemsContainerFindString, string itemFindString, string itemEndString, int size)
        {
            string[] news = new string[size];
            int start = webPageContentString.IndexOf(itemsContainerFindString);
            if (start > 0)
            {
                for (int i = 0; i < news.Length; i++)
                {
                    start = webPageContentString.IndexOf(itemFindString, start);
                    if (start < 0)
                        break;

                    start += itemFindString.Length;
                    int end = webPageContentString.IndexOf(itemEndString, start);
                    int length = end - start;
                    news[i] = webPageContentString.Substring(start, length);
                    start = end;
                }
            }

            return news;
        }

        public void SetNewsItems(string[] newsItems)
        { 
            NewsItems = new List<SelectListItem>();
            for(int i=0; i<newsItems.Length; i++)
                NewsItems.Add(new SelectListItem { Text = newsItems[i], Value = newsItems[i] });
        }

        public void SetNewsLinks(string[] strLinks)
        {
            Links = new List<SelectListItem>();
            for (int i = 0; i < strLinks.Length; i++)
                Links.Add(new SelectListItem { Text = strLinks[i], Value = strLinks[i] });
        }

        public static string GetWebParagraphContent(string url, string strStart, string strEnd)
        {
            string content = Exam.Models.ExamDefAdminViewModel.GetWebPageContentString(url);
            content = Exam.Models.ExamDefAdminViewModel.RemoveHtmlTags(content, strStart, strEnd);
            return content;
        }

        public static string RemoveHtmlTags(string webContentString, string strStart, string strEnd)
        {
            int start = 0;
            if (!String.IsNullOrEmpty(strStart))
            {
                start = webContentString.IndexOf(strStart);
                if (start >= 0)
                {
                    start += strStart.Length;
                    int l = webContentString.IndexOf(">", start);
                    if (l > 0)
                        start = l+1;
                }
                else
                    start = 0;
            }

            string paragraphs = "";
            while (true)
            {
                int end = webContentString.IndexOf("<", start);
                if (end < 0)
                    break;
                int length = end - start;
                paragraphs += webContentString.Substring(start, length);

                start = webContentString.IndexOf(">", start);
                if (start < 0)
                    break;
                start += 1;
            }

            if (!String.IsNullOrEmpty(strEnd))
            {
                int end = paragraphs.IndexOf(strEnd);
                if (end > 0)
                    paragraphs = paragraphs.Substring(0, end);
            }

            return paragraphs;
        }
    }

    public static class ExamDefinition
    {
        public static int QuestionCount = 4;
        public static int ChoiceCount = 4;
    }
}
