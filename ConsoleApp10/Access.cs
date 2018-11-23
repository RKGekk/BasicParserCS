using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Access : Op {
        public Id array;
        public Expr index;
        public Access(Id a, Expr i, BasicType p) : base(new Word(Tag.INDEX, "[]"), p) {
            array = a;
            index = i;
        }
        public Expr gen() {
            return new Access(array, index.reduce(), type);
        }
        public void jumping(int t, int f) {
            emitjumps(reduce().ToString(), t, f);
        }
        public override string ToString() {
            return array.ToString() + " [ " + index.ToString() + " ]";
        }
    }
}
