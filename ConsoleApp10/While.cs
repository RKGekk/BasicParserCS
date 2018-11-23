using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class While : Stmt {
        Expr expr;
        Stmt stmt;
        public While() {
            expr = null;
            stmt = null;
        }
        public void init(Expr x, Stmt s) {
            expr = x;
            stmt = s;
            if(expr.type != BasicType.Bool) {
                expr.error("boolean required in while");
            }
        }
        public void gen(int b, int a) {
            after = a;
            expr.jumping(0, a);
            int label = newlabel();
            emitlabel(label);
            stmt.gen(label, b);
            emit("goto L" + b);
        }
    }
}
