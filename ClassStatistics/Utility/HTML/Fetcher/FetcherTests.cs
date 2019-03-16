using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Utility.HTML
{
    public class FetcherTests
    {
        Fetcher localFetcher;
        Fetcher webFetcher;
        string startUpPath;
        string webPath;

        [SetUp]
        public void SetUp()
        {
            startUpPath = CONSTANTS.StartUpPath;
            webPath = "https://pages.github.com";
            localFetcher = new Fetcher(Source.Local, startUpPath);
            webFetcher = new Fetcher(Source.Web, webPath, "");
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
            Assert.AreEqual(webPath, webFetcher.Location);
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
