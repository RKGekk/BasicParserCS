using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Node {
        public int lexline = 0;
        public Node() {
            lexline = Lexer.line;
        }
        public void error(string s) {
            throw new Exception("near line " + lexline + ": " + s);
        }
        public static int lables = 0;
        public int newlabel() {
            return ++lables;
        }
        public void emitlabel(int i) {
            Console.Write("L" + i + ":");
        }
        public void emit(string s) {
            Console.Write("\t" + s);
        }
    }
}
