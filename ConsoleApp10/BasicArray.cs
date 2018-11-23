using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class BasicArray : BasicType {
        public BasicType of;
        public int size = 1;
        public BasicArray(int sz, BasicType p) : base(Tag.INDEX, "[]", sz * p.width) {
            size = sz;
            of = p;
        }
        public override string ToString() {
            return "[" + size + "]" + of.ToString();
        }
    }
}
