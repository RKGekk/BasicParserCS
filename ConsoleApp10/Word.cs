using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Word : Token {
        public string lexeme = "";
        public Word(int t, string s) : base(t) {
            lexeme = s;
        }
        public override string ToString() {
            return lexeme;
        }
        public static Word and = new Word(Tag.AND, "&&");
        public static Word or = new Word(Tag.OR, "||");
        public static Word eq = new Word(Tag.EQ, "==");
        public static Word ne = new Word(Tag.NE, "!=");
        public static Word le = new Word(Tag.LE, "<=");
        public static Word ge = new Word(Tag.GE, ">=");
        public static Word minus = new Word(Tag.MINUS, "minus");
        public static Word True = new Word(Tag.TRUE, "true");
        public static Word False = new Word(Tag.FALSE, "false");
        public static Word temp = new Word(Tag.TEMP, "t");
    }
}
