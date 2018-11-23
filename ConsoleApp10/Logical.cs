using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Logical : Expr {
        public Expr expr1;
        public Expr expr2;
        public Logical(Token tok, Expr x1, Expr x2) : base(tok, null) {
            expr1 = x1;
            expr2 = x2;
            type = check(expr1.type, expr2.type);
            if(type == null) {
                error("type error");
            }
        }
        public BasicType check(BasicType p1, BasicType p2) {
            if(p1 == BasicType.Bool && p2 == BasicType.Bool) {
                return BasicType.Bool;
            }
            else {
                return null;
            }
        }
        public Expr gen() {
            int f = newlabel();
            int a = newlabel();
            Temp temp = new Temp(type);
            jumping(0, f);
            emit(temp.ToString() + " = true");
            emit("goto L" + a);
            emitlabel(f);
            emit(temp.ToString() + " = false");
            emitlabel(a);
            return temp;
        }
        public override string ToString() {
            return expr1.ToString() + " " + op.ToString() + " " + expr2.ToString();
        }
    }
}
