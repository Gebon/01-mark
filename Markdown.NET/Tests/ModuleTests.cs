using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Markdown.NET.Tests
{
    [TestFixture]
    public class ModuleTests
    {
        [Test]
        public void ReturnStringWithoutSingleCarrageReturn()
        {
            var text = "Hello, \nworld!";
            var expected = "<p>Hello,  world!</p>";
            TestIt(expected, text);
        }
        [Test]
        public void ReturnParagraphForPlainText()
        {
            var text = "Hello, world!";
            var expected = "<p>Hello, world!</p>";
            TestIt(expected, text);
        }

        [Test]
        public void Paragraph()
        {
            var text = "\n\nparagraph\n\n";
            var expected = "<p>paragraph</p>";
            TestIt(expected, text);
        }

        [Test]
        public void ParagraphWithSpacesAndTabs()
        {
            var text = "\n\t\t  \n   \t\nparagraph\n\t\t\n  ";
            var expected = "<p>paragraph</p>";
            TestIt(expected, text);
        }

        [Test]
        public void MultipleParagraphs()
        {
            var text = "\n\t  \n  \nfirst paragraph\n\nsecond paragraph\n\n";
            var expected = "<p>first paragraph</p><p>second paragraph</p>";
            TestIt(expected, text);
        }

        private static void TestIt(string expected, string text)
        {
            Assert.AreEqual(expected, Markdown.Transform(text));
        }

        [Test]
        public void EmTest()
        {
            var text = "_text_";
            var expected = "<p><em>text</em></p>";
            TestIt(expected, text);
        }

        [Test]
        public void EmWithMultipleParagraphs()
        {
            var text = "This text _italic_\n\nAnd _this_ too";
            var expected = "<p>This text <em>italic</em></p><p>And <em>this</em> too</p>";
            TestIt(expected, text);
        }

        [Test]
        public void InsideText()
        {
            var text = "Some_thing_ text";
            var expected = "<p>Some<em>thing</em> text</p>";
            TestIt(expected, text);
        }

        [Test]
        public void EmAndStrong()
        {
            var text = "___strong and em text___";
            var expected = "<p><em><strong>strong and em text</strong></em></p>";
            TestIt(expected, text);
        }

        [Test]
        public void StrongInsideEm()
        {
            var text = "_em text with __strong___";
            var expected = "<p><em>em text with <strong>strong</strong></em></p>";
            TestIt(expected, text);
        }

        [Test]
        public void EmInsideStrong()
        {
            var text = "__strong text with _em___";
            var expected = "<p><strong>strong text with <em>em</em></strong></p>";
            TestIt(expected, text);
        }

        [Test]
        public void NotPair()
        {
            var text = "__text_";
            var expected = "<p>_<em>text</em></p>";
            TestIt(expected, text);
        }

        [Test]
        public void StrongTest()
        {
            var text = "__strong__";
            var expected = "<p><strong>strong</strong></p>";
            TestIt(expected, text);
        }
    }
}
