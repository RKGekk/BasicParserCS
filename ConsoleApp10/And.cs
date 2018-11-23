using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class And : Logical {
        public And(Token tok, Expr x1, Expr x2) : base(tok, x1, x2) {

        }
        public void jumping(int t, int f) {
            int label = f != 0 ? f : newlabel();
            expr1.jumping(0, label);
            expr2.jumping(t, f);
            if(f == 0) {
                emitlabel(label);
            }
        }
    }
}
