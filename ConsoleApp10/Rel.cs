using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Rel : Logical {
        public Rel(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) {

        }
        public BasicType check(BasicType p1, BasicType p2) {
            if (p1 is BasicArray || p2 is BasicArray) {
                return null;
            }
            else if (p1 == p2) {
                return BasicType.Bool;
            }
            else {
                return null;
            }
        }
        public void jumping(int t, int f) {
            Expr a = expr1.reduce();
            Expr b = expr2.reduce();
            string test = a.ToString() + " " + op.ToString() + " " + b.ToString();
            emitjumps(test, t, f);
        }
    }
}
