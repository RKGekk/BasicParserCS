using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Do : Stmt {
        Expr expr;
        Stmt stmt;
        public Do() {
            expr = null;
            stmt = null;
        }
        public void init(Expr x, Stmt s) {
            expr = x;
            stmt = s;
            if(expr.type != BasicType.Bool) {
                expr.error("boolean required in do");
            }
        }
        public void gen(int b, int a) {
            after = a;
            int label = newlabel();
            stmt.gen(b, label);
            emitlabel(label);
            expr.jumping(b, 0);
        }
    }
}
