using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ClassStatistics
{
    public class FetcherTests
    {
        Fetcher localFetcher;
        Fetcher webFetcher;
        string startUpPath;

        [SetUp]
        public void SetUp()
        {
            startUpPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName, "HTML", "RawHTML.txt");
            localFetcher = new Fetcher(Source.Local, startUpPath);
            webFetcher = new Fetcher(Source.Web, "https://pages.github.com", "");
        }

        [Test]
        public void LocalFetcherValidArguments()
        {
            Assert.AreEqual(Source.Local, localFetcher.Source);
            Assert.AreEqual("", localFetcher.Arguments);
            Assert.AreEqual(startUpPath, localFetcher.Location);
        }

        [Test]
        public void LocalFetcherValidDocument()
        {
            Assert.IsTrue(localFetcher.Document.ToString().Length > 1);
            Assert.IsTrue(localFetcher.Document.Text.Contains("apple"));
        }

        [Test]
        public void WebFetcherValidArguments()
        {
            Assert.AreEqual(Source.Web, webFetcher.Source);
            Assert.AreEqual("", webFetcher.Arguments);
            Assert.AreEqual("https://pages.github.com", webFetcher.Location);
        }

        [Test]
        public void WebFetcherValidDocument()
        {
            Assert.IsNotNull(webFetcher.Document);
            Assert.IsTrue(webFetcher.Document.ToString().Length > 1);
            Assert.IsTrue(webFetcher.Document.Text.Contains(".github.io"));
        }

    }
}
