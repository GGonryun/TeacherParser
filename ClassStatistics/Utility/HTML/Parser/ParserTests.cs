using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Utility.HTML
{
    class ParserTests
    {
        Fetcher fetcher;
        Parser elementParser;
        string elementPattern;
        Parser classParser;
        string classPattern;
        string startUpPath;

        [SetUp]
        public void SetUp()
        {
            classPattern = "//*[contains(concat(' ', @class, ' '), ' apple ')]";
            elementPattern = "//b";
            startUpPath = CONSTANTS.RawHTMLPath;
            fetcher = new Fetcher(Source.Local, startUpPath);
            elementParser = new Parser(fetcher.Document, elementPattern);
            classParser = new Parser(fetcher.Document, classPattern);
        }

        [Test]
        public void ClassParserValidArguments()
        {
            Assert.AreEqual(fetcher.Document, classParser.Document);
            Assert.AreEqual(classPattern, classParser.Pattern);
        }

        [Test]
        public void ClassParserFindsClass()
        {
            Assert.IsNotNull(classParser.Nodes);
            Assert.AreEqual(1, classParser.Nodes.Count);
        }

        [Test]
        public void ElementParserValidArguments()
        {
            Assert.AreEqual(fetcher.Document, elementParser.Document);
            Assert.AreEqual(elementPattern, elementParser.Pattern);
        }

        [Test]
        public void ElementParserFindsElement()
        {
            Assert.IsNotNull(elementParser.Nodes);
            Assert.AreEqual(1, elementParser.Nodes.Count);
        }


    }
}
