using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility.HTML
{
    public class Parser
    {
        public static string SelectByClassPattern(string classAttribute) => $"//*[contains(concat(' ', @class, ' '), ' {classAttribute} ')]";
        public static string SelectChildrenByClassPattern(string classAttribute) => $"./*[contains(concat(' ', @class, ' '), ' {classAttribute} ')]";
        public HtmlDocument Document { get; private set; }
        public string Pattern { get; private set; }

        HtmlNodeCollection nodes;
        public HtmlNodeCollection Nodes
        {
            get
            {
                if(nodes == null || nodes.Count < 1)
                {
                    nodes = FetchNodes();
                }
                return nodes;
            }
        }

        public Parser(HtmlDocument document, string pattern)
        {
            this.Document = document;
            this.Pattern = pattern;
        }

        private HtmlNodeCollection FetchNodes()
        {
            return Document.DocumentNode.SelectNodes($"{Pattern}");
        }

        public static HtmlNode FindChildByClass(HtmlNode node, string classAttribute)
        {
            HtmlNode n = node.SelectSingleNode(SelectChildrenByClassPattern(classAttribute));
            if (n != null)
                return n;
            return null;
        }

    }
}
