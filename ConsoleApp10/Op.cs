using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Op : Expr {
        public Op(Token tok, BasicType p) : base(tok, p) {

        }
        public Expr reduce() {
            Expr x = gen();
            Temp t = new Temp(type);
            emit(t.ToString() + " = " + x.ToString());
            return t;
        }
    }
}
