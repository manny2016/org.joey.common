
namespace Org.Joey.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Xml;

    public static class HtmlExtension
    {
        public static string StripTagsRegex(this string source)
        {
            return source.StripTagsRegex(null);
        }
        public static string StripTagsRegex(this string source, string[] removeWholeTags)
        {
            Guard.ArgumentNotNull(source, "HtmlExtension.StripTagsRegex.source");
            if (removeWholeTags != null && removeWholeTags.Length > 0)
            {
                foreach (var regex in removeWholeTags)
                    source = Regex.Replace(source, regex, string.Empty, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            }
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
        #region fix html tags
        public static string FixHtmlTags(this string source)
        {
            Guard.ArgumentNotNull(source, "HtmlExtension.FixHtmlTags.source");
            if (source.Trim().IndexOf("<!DOCTYPE html>", StringComparison.InvariantCultureIgnoreCase) != 0)
                throw new ArgumentException("The html flat <!DOCTYPE html> must start with point  line 0,column 0.");
            var blocks = source.GetHtmlTextBlocks().PreprocessHtmlTags();
            var starts = blocks.Where(o => o.BlockType == BlockType.StartTag);
            var ends = blocks.Where(o => o.BlockType == BlockType.EndTag);
            var innerTexts = blocks.Where(o => o.BlockType == BlockType.InnerText);

            if (starts.Count().Equals(ends.Count())) return source;
#if DEBUG
            foreach (var block in blocks.GroupBy(o => o.Tag).SelectMany(o => o.ToList()))
            {
                Debug.WriteLine(block.Text);

            }
#endif
            return string.Empty;
        }
        private static IEnumerable<HtmlTextBlock> GetHtmlTextBlocks(this string source)
        {
            MatchCollection matchs = Regex.Matches(source, "<.*?>");
            int index = 0;
            Match lastMatched = null;
            foreach (Match match in matchs)
            {
                yield return new HtmlTextBlock()
                {
                    BlockType = BlockType.Tags,
                    Index = index++,
                    Length = match.Value.Length,
                    IndexOf = match.Index,
                    Text = match.Value
                };
                if (lastMatched != null)
                {
                    int startIndex = lastMatched.Index + lastMatched.Value.Length;
                    int innerTextLength = match.Index - startIndex;
                    if (lastMatched.Index + 1 != match.Index)
                    {
                        yield return new HtmlTextBlock()
                        {
                            Index = index++,
                            BlockType = BlockType.InnerText,
                            Text = source.Substring(startIndex, innerTextLength),
                            IndexOf = lastMatched.Index + 1
                        };
                    }
                }
                lastMatched = match;
            }
        }
        static string[] exceptive = new string[] { "</br>", "<!DOCTYPE html>", "meta", "link", "script" };
        private static IEnumerable<HtmlTextBlock> PreprocessHtmlTags(this IEnumerable<HtmlTextBlock> blocks)
        {
            return blocks.Select((o) =>
            {
                bool bIsInnerText = (o.BlockType == BlockType.InnerText
                    || (o.BlockType == BlockType.Tags && o.Text.EndsWith("/>", StringComparison.InvariantCultureIgnoreCase))
                    || (o.BlockType == BlockType.Tags && o.Text.StartsWith("<!--", StringComparison.InvariantCultureIgnoreCase))
                    || (o.BlockType == BlockType.Tags && o.Text.StartsWith("<meta", StringComparison.InvariantCultureIgnoreCase))
                    || (o.BlockType == BlockType.Tags && o.Text.StartsWith("<script", StringComparison.InvariantCultureIgnoreCase))
                    || (o.BlockType == BlockType.Tags && exceptive.Contains(o.Text))
                    || (o.BlockType == BlockType.Tags && exceptive.Contains(o.Tag)));
                if (bIsInnerText)
                {
                    o.Tag = string.Empty;
                    o.BlockType = BlockType.InnerText;
                    return o;
                }
                else
                {
                    if (o.Text.IndexOf("</") < 0)//it is a end tag                
                        o.BlockType = BlockType.StartTag;
                    else
                        o.BlockType = BlockType.EndTag;

                    string[] array = o.Text.Split((char)32);
                    o.Tag = array[0].Replace("<", "").Replace("</", "").Replace(">", "").Replace("/", "");
                    return o;
                }
            });
        }
        #endregion
    }
}

