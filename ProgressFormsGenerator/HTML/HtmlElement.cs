using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGenerator.HTML
{
    public class HtmlElement
    {
        public event EventHandler ElementChanged;

        private string tagName;
        private string innerHTML;
        private Dictionary<string, string> attributes;
        private HashSet<string> classes;
        private List<HtmlElement> children;

        public string TagName { get { return tagName; } set { tagName = value; } }
        public string InnerHTML { get { return innerHTML; } set { innerHTML = value; } }
        public Dictionary<string, string> Attributes { get { return attributes; } }
        public List<HtmlElement> Children { get { return children; } }


        public HtmlElement(string tagName)
        {
            this.tagName = tagName;
            this.innerHTML = "";
            this.attributes = new Dictionary<string, string>();
            this.children = new List<HtmlElement>();
            this.classes = new HashSet<string>();
        }

        public void AddChildren(params HtmlElement[] elements)
        {
            foreach (HtmlElement element in elements) 
            {
                children.Add(element);
                element.ElementChanged += (s, e) => ElementChanged?.Invoke(s, e);
            }
            ElementChanged?.Invoke(this, new EventArgs());
        }

        public void Append(HtmlElement f)
        {
            Children.Add(f);
        }

        public int Remove(Func<HtmlElement, bool> pred)
        {
            int result = 0;
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (pred(child))
                {
                    Children.Remove(child);
                    result++;
                }
            }
            return result;
        }

        public string GetAttribute(string v)
        {
            return Attributes.Keys.Contains(v) ? Attributes["v"] : "";
        }

        public HtmlElement Find(Func<HtmlElement, bool> pred)
        {
            foreach (HtmlElement child in Children)
            {
                if (pred(child)) return child;
            }
            return null;
        }

        internal void Append()
        {
            throw new NotImplementedException();
        }

        public void AddAttribute(string attributeName, string attributeValue = "")
        {
            attributes.Add(attributeName, attributeValue);
            ElementChanged?.Invoke(this, new EventArgs());
        }

        public void AddClass(string className)
        {
            classes.Add(className);
            ElementChanged?.Invoke(this, new EventArgs());
        }

        public void RemoveClass(string className)
        {
            classes.Remove(className);
            ElementChanged?.Invoke(this, new EventArgs());
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("<" + tagName + " ");
            foreach (var attr in attributes)
            {
                result.Append(String.Format("{0}=\"{1}\" ", attr.Key, attr.Value));
            }
            if (classes.Count > 0)
            {
                result.Append(String.Format("class=\"{0}\" ", String.Join(" ", classes)));
            }
            result.Append(">").Append(innerHTML);
            foreach (HtmlElement child in children)
            {
                result.Append(child.ToString());
            }
            result.Append("</").Append(tagName).Append(">");
            return result.ToString();
        }

        public string GetId()
        {
            return Attributes.ContainsKey("id") ? Attributes["id"] : "";
        }
    }
}
