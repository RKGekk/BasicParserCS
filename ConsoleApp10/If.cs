using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class If : Stmt {
        public Expr expr;
        public Stmt stmt;
        public If(Expr x, Stmt s) {
            expr = x;
            stmt = s;
            if(expr.type != BasicType.Bool) {
                expr.error("boolean required in if");
            }
        }
        public void gen(int b, int a) {
            int label = newlabel();
            expr.jumping(0, a);
            emitlabel(label);
            stmt.gen(label, a);
        }
    }
}
