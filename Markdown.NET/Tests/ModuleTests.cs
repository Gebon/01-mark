using System;
using System.Collections.Generic;
using System.Linq;
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
            var expected = "<p>Hello, world!</p>";
            Assert.AreEqual(expected, Markdown.Transform(text));
        }
        [Test]
        public void ReturnParagraphForPlainText()
        {
            var text = "Hello, world!";
            var expected = "<p>Hello, world!</p>";
            Assert.AreEqual(expected, Markdown.Transform(text));
        }

        [Test]
        public void Paragraph()
        {
            var text = "\n\nparagraph\n\n";
            var expected = "<p>paragraph</p>";
            Assert.AreEqual(expected, Markdown.Transform(text));
        }

        [Test]
        public void ParagraphWithSpacesAndTabs()
        {
            var text = "\n\t\t  \n   \t\nparagraph\n\t\t\n  ";
            var expected = "<p>paragraph</p>";
            Assert.AreEqual(expected, Markdown.Transform(text));
        }

        [Test]
        public void MultipleParagraphs()
        {
            var text = "\n\t  \n  \nfirst paragraph\n\nsecond paragraph\n\n";
            var expected = "<p>first paragraph</p><p>second paragraph</p>";
            Assert.AreEqual(expected, Markdown.Transform(text));
        }

        [Test]
        public void ItalicTest()
        {
//            var text = 
        }
    }
}
