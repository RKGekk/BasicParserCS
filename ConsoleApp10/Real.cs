using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    class Real : Token {
        public float value;
        public Real(float v) : base(Tag.REAL) {
            value = v;
        }
        public override string ToString() {
            return value.ToString();
        }
    }
}
