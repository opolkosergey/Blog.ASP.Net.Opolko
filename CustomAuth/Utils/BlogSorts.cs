using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using CustomAuth.ViewModels;

namespace CustomAuth.Utils
{
    public static class BlogSorts
    {
        public static int Comparer(BlogViewModel x, BlogViewModel y)
        {
            if (x.ArticleCount > y.ArticleCount)
                return -1;
            else if (x.ArticleCount < y.ArticleCount)
                return 1;
            return 0;
        }
        public static IComparer<BlogViewModel> GetMethod(int code, out string infoGlyphicon)
        {
            switch (code)
            {
                case 0:
                    infoGlyphicon = "postsDown";
                    return new PostsDownSort();
                case 1:
                    infoGlyphicon = "postsUp";
                    return new PostsUpSort();
            }
            infoGlyphicon = "none";
            return null;
        } 
    }

    public class PostsDownSort : IComparer<BlogViewModel>
    {
        public int Compare(BlogViewModel x, BlogViewModel y)
        {
            if (x.ArticleCount > y.ArticleCount)
                return -1;
            else if (x.ArticleCount < y.ArticleCount)
                return 1;
            return 0;
        }
    }

    public class PostsUpSort : IComparer<BlogViewModel>
    {
        public int Compare(BlogViewModel x, BlogViewModel y)
        {
            if (x.ArticleCount > y.ArticleCount)
                return 1;
            else if (x.ArticleCount < y.ArticleCount)
                return -1;
            return 0;
        }
    }
}