using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public enum BlockType
    {
        Unknow = 1,

        StartTag = 2,

        EndTag = 3,

        Tags=4,
        /// <summary>
        /// explain the inner text or other html tags with no need for closed
        /// such as </br> etc..
        /// </summary>
        InnerText = 5,
    }
    public enum TagType
    {
        Unknlow = 1,
        NotTag = 2,
        Start = 3,
        End = 4,
    }
    public class HtmlTextBlock
    {
        public string Text { get; set; }

        public int Index { get; set; }

        public int IndexOf { get; set; }

        public int Length { get; set; }

        public string Tag { get; set; }

        public BlockType BlockType
        {
            get; set;
        }

    }
}
