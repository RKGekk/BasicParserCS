using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Stmt : Node {
        public Stmt() {

        }
        public static Stmt Null = new Stmt();
        public void gen(int b, int a) {

        }
        public int after = 0;
        public static Stmt Enclosing = Stmt.Null;
    }
}
