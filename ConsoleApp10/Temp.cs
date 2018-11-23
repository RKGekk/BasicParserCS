using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Temp : Expr {
        static int count = 0;
        int number = 0;
        public Temp(BasicType p) : base(Word.temp, p) {
            number = ++count;
        }
        public override string ToString() {
            return "t" + number;
        }
    }
}
