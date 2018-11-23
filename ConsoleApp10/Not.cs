using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Not : Logical {
        public Not(Token tok, Expr x2) : base(tok, x2, x2) {

        }
        public void jumping(int t, int f) {
            expr2.jumping(f, t);
        }
        public override string ToString() {
            return op.ToString() + " " + expr2.ToString();
        }
    }
}
