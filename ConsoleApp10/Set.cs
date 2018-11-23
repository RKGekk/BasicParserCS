using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Set : Stmt {
        public Id id;
        public Expr expr;
        public Set(Id i, Expr x) {
            id = i;
            expr = x;
            if(check(id.type, expr.type) == null) {
                error("type error");
            }
        }
        public BasicType check(BasicType p1, BasicType p2) {
            if(BasicType.numeric(p1) && BasicType.numeric(p2)) {
                return p2;
            }
            else if (p1 == BasicType.Bool && p2 == BasicType.Bool) {
                return p2;
            }
            else {
                return null;
            }

        }
        public void gen(int b, int a) {
            emit(id.ToString() + " = " + expr.gen().ToString());
        }
    }
}
