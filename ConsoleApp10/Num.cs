using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Num : Token {
        public int value;
        public Num(int v) : base(Tag.NUM) {
            value = v;
        }
        public override string ToString() {
            return value.ToString();
        }
    }
}
