using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Id : Expr {
        public int offset;
        public Id(Word id, BasicType p, int b) : base(id, p) {
            offset = b;
        }
    }
}
