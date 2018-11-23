using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class BasicType : Word {
        public int width = 0;
        public BasicType(int tag, string s, int w) : base(tag, s) {
            width = w;
        }
        public static BasicType Int = new BasicType(Tag.BASIC, "int", 4);
        public static BasicType Float = new BasicType(Tag.BASIC, "float", 8);
        public static BasicType Char = new BasicType(Tag.BASIC, "char", 1);
        public static BasicType Bool = new BasicType(Tag.BASIC, "bool", 1);
        public static bool numeric(BasicType p) {
            if (p == BasicType.Char || p == BasicType.Int || p == BasicType.Float) {
                return true;
            }
            else {
                return false;
            }
        }
        public static BasicType max(BasicType p1, BasicType p2) {
            if (!numeric(p1) || !numeric(p2)) {
                return null;
            }
            else if (p1 == BasicType.Float || p2 == BasicType.Float) {
                return BasicType.Float;
            }
            else if (p1 == BasicType.Int || p2 == BasicType.Int) {
                return BasicType.Int;
            }
            else {
                return BasicType.Char;
            }
        }
    }
}
