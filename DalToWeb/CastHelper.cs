using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalToWeb.ORM;

namespace DalToWeb
{
    public static class CastHelper
    {
        public static string ToTagsString(this ICollection<Tag> tags)
        {
            var sb = new StringBuilder();
            if (tags.Count != 0)
            {
                foreach (var tag in tags)
                {
                    sb.Append(tag.TagField);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
    }
}
