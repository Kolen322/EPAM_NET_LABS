using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    public interface ICustomIterator<T>
    {
        T Current
        {
            get;
        }
        bool MoveNext();
        void Reset();
    }
}
