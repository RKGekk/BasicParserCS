using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Token {
        public int tag;
        public Token(int t) {
            tag = t;
        }
        public override string ToString() {
            return ((char)tag).ToString();
        }
    }
}
