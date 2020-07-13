using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    public interface ICustomEnumerator<T>
    {
        ICustomIterator<T> GetEnumerator();
    }
}
