using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class SetElem : Stmt {
        public Id array;
        public Expr index;
        public Expr expr;
        public SetElem(Access x, Expr y) {
            array = x.array;
            index = x.index;
            expr = y;
            if(check(x.type, expr.type) == null) {
                error("type error");
            }
        }
        public BasicType check(BasicType p1, BasicType p2) {
            if(p1 is BasicArray || p2 is BasicArray) {
                return null;
            }
            else if(p1 == p2) {
                return p2;
            }
            else if(BasicType.numeric(p1) && BasicType.numeric(p2)) {
                return p2;
            }
            else {
                return null;
            }
        }
        public void gen(int a, int b) {
            string s1 = index.reduce().ToString();
            string s2 = expr.reduce().ToString();
            emit(array.ToString() + " [ " + s1 + " ] = " + s2);
        }
    }
}
