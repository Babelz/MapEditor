using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ss = new List<string>()
            {
                "asd",
                "asd",
                "asd"
            };

            IEnumerable<string> sse = ss.Select(s => s);

            Console.WriteLine(sse.Count());

            ss.RemoveAt(0);

            Console.WriteLine(sse.Count());
        }

    }

    /*public class ReferenceWrapper<T> : IDisposable where T : IDisposable
    {
        private readonly T value;

        private int references;

        private bool disposed;

        public bool Disposed
        {
            get
            {
                return disposed;
            }
        }

        public ReferenceWrapper(T value)
        {
            this.value = value;
        }

        public void Reference()
        {
            references++;
        }
        public void Dereference()
        {
            references--;

            if (references <= 0) Dispose();
        }

        public void Dispose()
        {
            disposed = true;

            GC.SuppressFinalize(this);

            value.Dispose();
        }

        ~ReferenceWrapper()
        {
            Dispose();
        }
    }*/
}
