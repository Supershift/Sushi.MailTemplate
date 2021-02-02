using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wim.Data;
using Wim.Framework;

namespace Sushi.MailTemplate.UI
{
    internal static class StyleExtensions
    {
        /// <summary>
        /// Adds a style element to the page header
        /// </summary>
        /// <param name="head"></param>
        /// <param name="path">relative path to the file</param>
        /// <param name="appendApplicationPath">when false the application path will not be added to the path param</param>
        public static void AddStyle(this Wim.Framework.Head head, string path, bool appendApplicationPath = true)
        {
            string _path = (appendApplicationPath) ? Wim.Utility.AddApplicationPath(path) : path;
            if (appendApplicationPath)
                _path = string.Concat(_path, $"?v={System.Configuration.ConfigurationManager.AppSettings["FILEVERSION"]}");

            head.Add(string.Format(@"<link rel=""stylesheet"" href=""{0}"" type=""text/css"" media=""all"" />", _path));
        }

        ///// <summary>
        ///// Adds a script element to the page header
        ///// </summary>
        ///// <param name="head"></param>
        ///// <param name="path">relative path to the file</param>
        //public static void AddScriptVariables(this Wim.Framework.Head head, string variables)
        //{
        //    head.Add(string.Format(@"<script type=""text/javascript"">{0}</script>",
        //        variables));
        //}

        ///// <summary>
        ///// Adds font-awesome.min.css from the maxcdn to the page
        ///// </summary>
        ///// <param name="head"></param>
        ///// <param name="path">relative path to the file</param>
        //public static void AddFontAwesome(this Wim.Framework.Head head)
        //{
        //    head.AddStyle("//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css", false);
        //}


        //public static int NumberOfNights(this DateTime? date, DateTime? enddate)
        //{
        //    int days = 0;
        //    if (date.HasValue && enddate.HasValue)
        //    {
        //        var delta = enddate.Value.Subtract(date.Value);
        //        days = delta.Days;
        //    }
        //    return days;
        //}

        //public static int NumberOfNights(this DateTime date, DateTime enddate)
        //{
        //    int days = 0;
        //    var delta = enddate.Subtract(date);
        //    days = delta.Days;
        //    return days;
        //}

        //public static DateTime? GetEndDate(this DateTime? date, int nights)
        //{
        //    return date?.AddDays(nights);
        //}

        //public static double ToJavaScriptMilliseconds(this DateTime dt)
        //{
        //    return (dt - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        //}

        ///// <summary>
        ///// Parses a wim Sublist to a int Array
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static int[] ToIntArray(this SubList list)
        //{
        //    return list.GetIDList().ToArray();
        //}

        ///// <summary>
        ///// returns a string formatted as #.##
        ///// </summary>
        ///// <param name="_decimal"></param>
        ///// <returns></returns>
        //[Obsolete("Use currency from table")]
        //public static string ToCurrencyFormat(this decimal _decimal, string currenyHTMLName = "")
        //{
        //    string c = String.Format("{0:0.00}", _decimal);
        //    if (!string.IsNullOrEmpty(currenyHTMLName))
        //        c = $"{currenyHTMLName} {c}";
        //    return c;
        //}

        //public static string GetCurrentQueryUrl(this Wim.Framework.WimComponentRoot wim, bool includeWimPath, params KeyValue[] keyvalues)
        //{
        //    string build = Wim.Utility.GetCustomQueryString(keyvalues);
        //    if (includeWimPath && wim.Page != null)
        //    {
        //        return string.Concat(wim.Page.Url, build);
        //    }
        //    return build;
        //}

        ///// <summary>
        ///// Replaces textbox specific characters to html
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static string ToTextHTML(this string input)
        //{
        //    if (!string.IsNullOrEmpty(input))
        //    {
        //        input = input
        //            .Replace("\r\n", "<br />");
        //    }
        //    return input;
        //}
        //public static void AddHtml(this Wim.Framework.Body body, string path, System.Web.HttpServerUtility server, bool clearBaseTemplateBody = false)
        //{
        //    string filePath = server.MapPath(path);
        //    string bodyContent = File.ReadAllText(filePath);
        //    body.Add(bodyContent, clearBaseTemplateBody, Body.BodyTarget.Below);
        //}

        //public static byte[] GetAsByteArray(this HttpPostedFile file)
        //{
        //    byte[] bytes = null;
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        using (BinaryReader b = new BinaryReader(file.InputStream))
        //        {
        //            bytes = b.ReadBytes(file.ContentLength);
        //        }
        //    }
        //    return bytes;
        //}

        ///// <summary>
        ///// Returns the current list url without any other queryparam
        ///// </summary>
        ///// <param name="wim"></param>
        ///// <param name="item">sets the Item param</param>
        ///// <returns></returns>
        //public static string GetCurrentListSearchUrl(this WimComponentListRoot wim, string item = null)
        //{
        //    List<KeyValue> keys = new List<KeyValue>();
        //    keys.Add(new KeyValue() { Key = "list", Value = wim.CurrentList.ID.ToString() });
        //    if (!string.IsNullOrEmpty(item))
        //    {
        //        keys.Add(new KeyValue() { Key = "item", Value = item });
        //    }
        //    return wim.GetCurrentQueryUrl(true, keys.ToArray());
        //}
    }
}