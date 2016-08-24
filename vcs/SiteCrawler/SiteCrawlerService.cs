using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Betarium.SiteCrawler
{
    public class SiteCrawlerService
    {
        public struct SiteCrawlerInfo
        {
            public List<string> outerSiteList;
        }

        public struct SiteInfo
        {
            public string Title { get; set; }
            public string Path { get; set; }
            public string Html { get; set; }
            public bool Error { get; set; }
            public HttpStatusCode ErrorCode { get; set; }
        }

        public string CacheDirectory { get; set; }
        public int MaxFollow { get; set; }

        public void DownloadAll(string url, Dictionary<string, SiteInfo> downloadData)
        {
            Uri ur = new Uri(url);
            Uri server = new Uri(ur.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped));
            string path = ur.LocalPath;

            SiteCrawlerInfo crawlerInfo = new SiteCrawlerInfo();
            crawlerInfo.outerSiteList = new List<string>();

            //Dictionary<string, SiteInfo> downloadCache = new Dictionary<string, SiteInfo>();
            List<string> targetUrlList = new List<string>();
            targetUrlList.Add(url);
            while (targetUrlList.Count > 0)
            {
                DownloadPageRecursive(crawlerInfo, server, targetUrlList, path, 0, downloadData);
            }

        }

        public void DownloadPageRecursive(SiteCrawlerInfo crawlerInfo, Uri server, List<string> targetUrlList, string basePath, int follow, Dictionary<string, SiteInfo> downloadCache)
        {
            string path = targetUrlList[0];
            targetUrlList.RemoveAt(0);
            if (downloadCache.ContainsKey(path))
            {
                return;
            }

            if (basePath != path && path.StartsWith(basePath + "/"))
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("download=" + path);
            Uri targetUrl = new Uri(server, path);
            string html = null;
            try
            {
                html = DownloadPage(targetUrl.ToString());
            }
            catch (WebException we)
            {
                SiteInfo info2 = new SiteInfo();
                info2.Path = path;
                info2.Error = true;
                if (we.Response is HttpWebResponse)
                {
                    HttpWebResponse errorResponse = (HttpWebResponse)we.Response;
                    info2.ErrorCode = errorResponse.StatusCode;
                }

                if (info2.ErrorCode == HttpStatusCode.NotFound)
                {
                    System.Diagnostics.Debug.WriteLine("not found=" + path);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("error=" + path);
                }
                info2.Path = path;
                downloadCache.Add(path, info2);
                return;
            }

            SiteInfo info = new SiteInfo();
            info.Path = path;
            //info.Html = html;
            info.ErrorCode = HttpStatusCode.OK;
            downloadCache.Add(path, info);

            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentText = "";
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            html = null;

            List<string> linkList = new List<string>();
            var elements = browser.Document.All;
            foreach (HtmlElement ele in elements)
            {
                if (ele.TagName.ToLower() == "a")
                {
                    string href = ele.GetAttribute("href");
                    if (string.IsNullOrEmpty(href))
                    {
                        continue;
                    }
                    else if (href.StartsWith("about:"))
                    {
                        continue;
                    }
                    else if (href.StartsWith("javascript:"))
                    {
                        continue;
                    }
                    else if (href.StartsWith("mailto:"))
                    {
                        continue;
                    }
                    //System.Diagnostics.Debug.WriteLine("  href=" + href);
                    linkList.Add(href);
                }
                else if (ele.TagName.ToLower() == "title")
                {
                    info.Title = ele.InnerText;
                    downloadCache[path] = info;
                }
            }

            browser.Dispose();
            browser = null;

            foreach (string link in linkList)
            {
                Uri urtmp = null;
                if (!Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out urtmp))
                {
                    System.Diagnostics.Debug.WriteLine("  invalid=" + link);
                    continue;
                }
                if (!string.IsNullOrEmpty(urtmp.Host) && targetUrl.Host != urtmp.Host)
                {
                    if (crawlerInfo.outerSiteList.Contains(urtmp.Host))
                    {
                        continue;
                    }
                    crawlerInfo.outerSiteList.Add(urtmp.Host);
                    System.Diagnostics.Debug.WriteLine("  out site=" + link);
                    continue;
                }

                if (!urtmp.LocalPath.EndsWith("/"))
                {
                    if (urtmp.LocalPath.LastIndexOf("/") >= 0)
                    {
                        string filename = urtmp.LocalPath.Substring(urtmp.LocalPath.LastIndexOf("/") + 1);
                        string fileext = Path.GetExtension(filename);
                        if (fileext == ".bmp" || fileext == ".jpg" || fileext == ".jpeg" || fileext == ".gif" || fileext == ".png" || fileext == ".pdf")
                        {
                            continue;
                        }
                    }
                }

                //Uri ur = new Uri(targetUrl, path);
                string path2 = urtmp.LocalPath;
                Uri ur2 = new Uri(targetUrl, path2);
                string url2 = ur2.AbsolutePath;
                if (downloadCache.ContainsKey(url2))
                {
                    continue;
                }
                if (MaxFollow != 0 && follow >= MaxFollow)
                {
                    System.Diagnostics.Debug.WriteLine("  skip=" + url2);
                    continue;
                }
                //DownloadPageRecursive(crawlerInfo, server, basePath, url2, follow + 1, downloadCache);
                targetUrlList.Add(url2);
            }

        }

        public string DownloadPage(string url)
        {
            string cache = LoadCache(CacheDirectory, url);
            if (cache != null)
            {
                return cache;
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                byte[] buf = new byte[1024 * 1024];
                MemoryStream bufstream = new MemoryStream();
                using (var stream = response.GetResponseStream())
                {
                    while (true)
                    {
                        int rd = stream.Read(buf, 0, buf.Length);
                        if (rd == 0)
                        {
                            break;
                        }
                        bufstream.Write(buf, 0, rd);
                    }
                }
                bufstream.Seek(0, SeekOrigin.Begin);
                byte[] buf2 = bufstream.GetBuffer();
                bufstream.Close();
                string text = Encoding.UTF8.GetString(buf2);
                SaveCache(CacheDirectory, url, text);
                return text;
            }
        }

        private string LoadCache(string dir, string url)
        {
            string url2 = UrlToFileName(url);

            string path = Path.Combine(dir, url2);
            if (!File.Exists(path))
            {
                return null;
            }
            string result = File.ReadAllText(path);
            return result;
        }

        private void SaveCache(string dir, string url, string result)
        {
            string url2 = UrlToFileName(url);

            string path = Path.Combine(dir, url2);

            File.WriteAllText(path, result);
        }

        private string UrlToFileName(string url)
        {
            Uri uri = null;
            string path1 = null;
            if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
            {
                path1 = uri.PathAndQuery;
            }
            else
            {
                path1 = Uri.EscapeDataString(url);
            }
            path1 = path1.TrimStart('/');
            path1 = Regex.Replace(path1, "\\.html$", "");
            path1 = path1.Replace("/", "-");
            path1 = path1.Replace("?", "-");
            path1 = path1.Replace("&", "-");
            string url2 = Uri.EscapeDataString(path1);
            if (url2.Length > 100)
            {
                url2 = url2.Substring(0, 100);
            }
            var md5service = MD5CryptoServiceProvider.Create();
            byte[] hash = md5service.ComputeHash(Encoding.UTF8.GetBytes(url));
            md5service.Dispose();

            StringBuilder hashStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                hashStr.AppendFormat("{0:X2}", hash[i]);
            }

            url2 += "_" + hashStr.ToString();

            if (url2 == "")
            {
                url2 = "index";
            }
            url2 += ".html";
            return url2;
        }

    }
}
