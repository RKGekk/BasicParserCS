using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Expr : Node {
        public Token op;
        public BasicType type;
        public Expr(Token tok, BasicType p) {
            op = tok;
            type = p;
        }
        public Expr gen() {
            return this;
        }
        public Expr reduce() {
            return this;
        }
        public void jumping(int t, int f) {
            emitjumps(ToString(), t, f);
        }
        public void emitjumps(string test, int t, int f) {
            if(t != 0 && f != 0) {
                emit("if " + test + " goto L" + t);
                emit("goto L" + f);
            }
            else if(t != 0) {
                emit("if " + test + " goto L" + t);
            }
            else if(f != 0) {
                emit("iffalse " + test + " goto L" + f);
            }
        }
        public override string ToString() {
            return op.ToString();
        }
    }
}
