using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Break : Stmt {
        public Stmt stmt;
        public Break() {
            if(Stmt.Enclosing == null) {
                error("unclosed break");
            }
            stmt = Stmt.Enclosing;
        }
        public void gen(int b, int a) {
            emit("goto L" + stmt.after);
        }
    }
}
