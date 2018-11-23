using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Lexer {

        public static int line = 1;
        public int pos = 0;
        List<char> str = new List<char>();

        private char peek = ' ';
        private Dictionary<string, Token> words = new Dictionary<string, Token>();

        void reserve(Word w) {
            words.Add(w.lexeme, w);
        }

        public Lexer(string path) {
            reserve(new Word(Tag.IF, "if"));
            reserve(new Word(Tag.ELSE, "else"));
            reserve(new Word(Tag.WHILE, "while"));
            reserve(new Word(Tag.DO, "do"));
            reserve(new Word(Tag.BREAK, "break"));
            reserve(Word.True);
            reserve(Word.False);
            reserve(BasicType.Int);
            reserve(BasicType.Char);
            reserve(BasicType.Bool);
            reserve(BasicType.Float);

            using (StreamReader sr = new StreamReader(path)) {
                char[] c = null;
                int len = 0;
                while (sr.Peek() >= 0) {
                    c = new char[255];
                    len += sr.Read(c, 0, c.Length);
                    str.AddRange(c);
                    Console.Write(c);
                }
            }
        }

        void readch() {

            peek = str[pos];
            pos++;
        }

        bool readch(char c) {
            readch();
            if (peek != c) {
                return false;
            }
            peek = ' ';
            return true;
        }

        public Token scan() {

            for (; ; readch()) {
                if (peek == ' ' || peek == '\t' || peek == '\r') {
                    continue;
                }
                else if (peek == '\n') {
                    line++;
                }
                else
                    break;
            }

            switch (peek) {
                case '&':
                    if (readch('&')) {
                        return Word.and;
                    }
                    else {
                        return new Token('&');
                    }
                case '|':
                    if (readch('|')) {
                        return Word.or;
                    }
                    else {
                        return new Token('|');
                    }
                case '=':
                    if (readch('=')) {
                        return Word.eq;
                    }
                    else {
                        return new Token('=');
                    }
                case '!':
                    if (readch('=')) {
                        return Word.ne;
                    }
                    else {
                        return new Token('!');
                    }
                case '<':
                    if (readch('=')) {
                        return Word.le;
                    }
                    else {
                        return new Token('|');
                    }
                case '>':
                    if (readch('=')) {
                        return Word.ge;
                    }
                    else {
                        return new Token('>');
                    }
            }

            if (Char.IsDigit(peek)) {
                int v = 0;
                do {
                    v = 10 * v + int.Parse(peek.ToString(), System.Globalization.NumberStyles.Integer);
                    readch();
                } while (Char.IsDigit(peek));
                if (peek != '.') {
                    return new Num(v);
                }
                float x = v;
                float d = 10.0f;
                for(;;) {
                    readch();
                    if(!Char.IsDigit(peek)) {
                        break;
                    }
                    x = x + int.Parse(peek.ToString(), System.Globalization.NumberStyles.Integer) / d;
                    d *= 10.0f;
                }
                return new Real(x);
            }
            if (Char.IsLetter(peek)) {
                StringBuilder b = new StringBuilder();
                do {
                    b.Append(peek);
                    readch();
                } while (Char.IsLetterOrDigit(peek));
                string s = b.ToString();
                if (words.ContainsKey(s)) {
                    return (Word)words[s];
                }
                Word w = new Word(Tag.ID, s);
                words.Add(s, w);
                return w;
            }
            Token t = new Token(peek);
            peek = ' ';
            return t;
        }
    }
}
