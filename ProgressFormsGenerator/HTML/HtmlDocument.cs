using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class HtmlDocument
    {
        public event EventHandler DocumentChanged;

        private HtmlElement rootHtml;

        public HtmlElement RootElement { get { return rootHtml; } }

        public HtmlDocument() : this("html") { }

        public HtmlDocument(string tagName) 
        {
            rootHtml = new HtmlElement(tagName);
            rootHtml.ElementChanged += (s, e) => DocumentChanged?.Invoke(s, e);
        }

        public HtmlElement GetById(string id)
        {
            HtmlElement[] result = GetBy(x => x.GetId() == id);
            return result.Length == 0 ? null : result[0];
        }

        public HtmlElement[] GetByTagName(string tagName)
        {
            return GetBy(x => x.TagName == tagName);
        }

        public HtmlElement[] GetBy(Predicate<HtmlElement> pred)
        {
            List<HtmlElement> result = null;

            var searchDomain = new Stack<HtmlElement>();
            searchDomain.Push(rootHtml);
            while (result == null && searchDomain.Count != 0)
            {
                var current = searchDomain.Pop();
                if (pred(current))
                {
                    result.Add(current);
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        searchDomain.Push(child);
                    }
                }
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return RootElement.ToString();
        }
    }
}
