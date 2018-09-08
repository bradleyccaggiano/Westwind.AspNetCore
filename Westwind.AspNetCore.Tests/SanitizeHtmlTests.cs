﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Westwind.AspNetCore.Markdown.Utilities;

/// <summary>
/// Note: this is the internal implementation which should duplicate what
/// Westwind.Utilities.HtmlUtils.SanitizeHtml() does
/// </summary>
namespace Westwind.AspNetCore.Tests
{

    [TestClass]
    public class SanitizeHtmlTests
    {
        [TestMethod]
        public void HtmlSanitizeScriptTags()
        {
            string html = "<div>User input with <ScRipt>alert('Gotcha');</ScRipt></div>";

            var result = StringUtils.SanitizeHtml(html);

            Console.WriteLine(result);
            Assert.IsTrue(!result.Contains("<ScRipt>"));
        }


        [TestMethod]
        public void HtmlSanitizeJavaScriptTags()
        {
            string html = "<div>User input with <a href=\"javascript: alert('Gotcha')\">Don't hurt me!<a/></div>";

            var result = StringUtils.SanitizeHtml(html);

            Console.WriteLine(result);
            Assert.IsTrue(!result.Contains("javascript:"));
        }

        [TestMethod]
        public void HtmlSanitizeJavaScriptTagsSingleQuotes()
        {
            string html = "<div>User input with <a href='javascript: alert(\"Gotcha\");'>Don't hurt me!<a/></div>";

            var result = StringUtils.SanitizeHtml(html);

            Console.WriteLine(result);
            Assert.IsTrue(!result.Contains("javascript:"));
        }

        [TestMethod]
        public void HtmlSanitizeJavaScriptTagsWithUnicodeQuotes()
        {
            string html = "<div>User input with <a href='&#106;&#97;&#118;&#97;&#115;&#99;&#114;&#105;&#112;&#116;:alert(\"javascript active\");'>Don't hurt me!<a/></div>";

            var result = StringUtils.SanitizeHtml(html);

            Console.WriteLine(result);
            Assert.IsTrue(!result.Contains("&#106;&#97;&#118"));
        }


        [TestMethod]
        public void HtmlSanitizeEventAttributes()
        {
            string html = "<div onmouseover=\"alert('Gotcha!')\">User input with " +
                          "<div onclick='alert(\"Gotcha!\");'>Don't hurt me!<div/>" +
                          "</div>";

            var result = StringUtils.SanitizeHtml(html);

            Console.WriteLine(result);
            Assert.IsTrue(!result.Contains("onmouseover:") && !result.Contains("onclick"));
        }
    }
}
