using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Constant : Expr {
        public Constant(Token tok, BasicType p) : base(tok, p) {

        }
        public Constant(int i) : base(new Num(i), BasicType.Int) {

        }
        public static Constant True = new Constant(Word.True, BasicType.Bool);
        public static Constant False = new Constant(Word.False, BasicType.Bool);
        public void jumping(int t, int f) {
            if(this == True && t != 0) {
                emit("goto L" + t);
            }
            else if(this == False && f != 0) {
                emit("goto L" + f);
            }
        }
    }
}
