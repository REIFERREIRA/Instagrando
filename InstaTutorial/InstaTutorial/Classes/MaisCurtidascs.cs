using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTutorial
{
    public class MaisCurtidas : IEnumerable
    {
        public string nome { get; set; }
        public int curtidas { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
