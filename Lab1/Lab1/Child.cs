using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    [Serializable]
    class Child
    {
        public string Name { get; }
        int size;
        string attributes;
        public SortedList<Child, int> childs { get; }

        public Child(FileSystemInfo fsi)
        {
            this.Name = fsi.Name;

            if (fsi is DirectoryInfo)
            {
                this.childs = new SortedList<Child, int>(new NameComparer());
                this.size = this.getChilds((DirectoryInfo)fsi);
            }
            else
            {
                this.childs = null;
                this.size = Convert.ToInt32(((FileInfo)fsi).Length);
            }

            this.attributes = fsi.getAttributes();
        }

        private int getChilds(DirectoryInfo di)
        {
            foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
            {
                Child newChild = new Child(fsi);
                this.childs.Add(newChild, newChild.size);
            }

            return this.childs.Count;
        }

        private string print(string tabs)
        {
            String result = this.getFullName();

            if (this.childs != null)
            {
                foreach (KeyValuePair<Child, int> kvp in this.childs)
                {
                    result += tabs + kvp.Key.print(tabs + "\t");
                }
            }

            return result;
        }

        private string getFullName()
        {
            return this.Name + " (" + this.size + ") " + this.attributes + "\n";
        }

        public override string ToString()
        {
            return this.print("\t");
        }
    }
}
