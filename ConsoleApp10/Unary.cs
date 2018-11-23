using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Unary : Op {
        public Expr expr;
        public Unary(Token tok, Expr x) : base(tok, null) {
            expr = x;
            type = BasicType.max(BasicType.Int, expr.type);
            if(type == null) {
                error("type error");
            }
        }
        public Expr gen() {
            return new Unary(op, expr.reduce());
        }
        public override string ToString() {
            return op.ToString() + " " + expr.ToString();
        }
    }
}
