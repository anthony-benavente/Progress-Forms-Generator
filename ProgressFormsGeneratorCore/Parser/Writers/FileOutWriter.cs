using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressFormsGeneratorCore.Parser.Writers
{
    public abstract class FileOutWriter : Writer
    {
        private Uri outFile;

        public FileOutWriter(Uri outFile)
        {
            this.outFile = outFile;
        }

        protected Uri OutFile { get { return outFile;  } }
    }
}
