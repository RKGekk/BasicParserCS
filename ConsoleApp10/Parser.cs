using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp27 {
    public class Parser {
        public Lexer lex;
        public Token look;
        public Env top = null;
        public int used = 0;
        public Parser(Lexer l) {
            lex = l;
            move();
        }
        public void move() {
            look = lex.scan();
        }
        public void error(string s) {
            throw new Exception("near line " + Lexer.line + ": " + s);
        }
        public void match(int t) {
            if (look.tag == t) {
                move();
            }
            else {
                error("syntax error");
            }
        }
        public void program() {
            Stmt s = block();
            int begin = s.newlabel();
            int after = s.newlabel();
            s.emitlabel(begin);
            s.gen(begin, after);
            s.emitlabel(after);
        }
        public Stmt block() {
            match('{');
            Env savedEnv = top;
            top = new Env(top);
            decls();
            Stmt s = stmts();
            match('}');
            top = savedEnv;
            return s;
        }
        public void decls() {
            while (look.tag == Tag.BASIC) {
                BasicType p = type();
                Token tok = look;
                match(Tag.ID);
                match(';');
                Id id = new Id((Word)tok, p, used);
                top.put(tok, id);
                used += p.width;
            }
        }
        public BasicType type() {
            BasicType p = (BasicType)look;
            match(Tag.BASIC);
            if (look.tag != '[') {
                return p;
            }
            else {
                return dims(p);
            }
        }
        public BasicType dims(BasicType p) {
            match('[');
            Token tok = look;
            match(Tag.NUM);
            match(']');
            if (look.tag == '[') {
                p = dims(p);
            }
            return new BasicArray(((Num)tok).value, p);
        }
        public Stmt stmts() {
            if (look.tag == '}') {
                return Stmt.Null;
            }
            else {
                return new Seq(stmt(), stmts());
            }
        }
        public Stmt stmt() {
            Expr x;
            Stmt s, s1, s2;
            Stmt savedStmt;
            switch (look.tag) {
                case ';':
                    move();
                    return Stmt.Null;
                case Tag.IF:
                    match(Tag.IF);
                    match('(');
                    x = boolean();
                    match(')');
                    s1 = stmt();
                    if (look.tag != Tag.ELSE) {
                        return new If(x, s1);
                    }
                    match(Tag.ELSE);
                    s2 = stmt();
                    return new Else(x, s1, s2);
                case Tag.WHILE:
                    While whilenode = new While();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = whilenode;
                    match(Tag.WHILE);
                    match('(');
                    x = boolean();
                    match(')');
                    s1 = stmt();
                    whilenode.init(x, s1);
                    Stmt.Enclosing = savedStmt;
                    return whilenode;
                case Tag.DO:
                    Do donode = new Do();
                    savedStmt = Stmt.Enclosing;
                    Stmt.Enclosing = donode;
                    match(Tag.DO);
                    s1 = stmt();
                    match(Tag.WHILE);
                    match('(');
                    x = boolean();
                    match(')');
                    match(';');
                    donode.init(x, s1);
                    Stmt.Enclosing = savedStmt;
                    return donode;
                case Tag.BREAK:
                    match(Tag.BREAK);
                    match(';');
                    return new Break();
                case '{':
                    return block();
                default:
                    return assign();
            }
        }
        public Stmt assign() {
            Stmt stmt;
            Token t = look;
            match(Tag.ID);
            Id id = top.get(t);
            if (id == null) {
                error(t.ToString() + " undeclared");
            }
            if (look.tag == '=') {
                move();
                stmt = new Set(id, boolean());
            }
            else {
                Access x = offset(id);
                match('=');
                stmt = new SetElem(x, boolean());
            }
            match(';');
            return stmt;
        }
        public Expr boolean() {
            Expr x = join();
            while (look.tag == Tag.OR) {
                Token tok = look;
                move();
                x = new Or(tok, x, join());
            }
            return x;
        }
        public Expr join() {
            Expr x = equality();
            while (look.tag == Tag.AND) {
                Token tok = look;
                move();
                x = new And(tok, x, equality());
            }
            return x;
        }
        public Expr equality() {
            Expr x = rel();
            while (look.tag == Tag.EQ || look.tag == Tag.NE) {
                Token tok = look;
                move();
                x = new Rel(tok, x, rel());
            }
            return x;
        }
        public Expr rel() {
            Expr x = expr();
            switch (look.tag) {
                case '<':
                case Tag.LE:
                case Tag.GE:
                case '>':
                    Token tok = look;
                    move();
                    return new Rel(tok, x, expr());
                default:
                    return x;
            }
        }
        public Expr expr() {
            Expr x = term();
            while (look.tag == '+' || look.tag == '-') {
                Token tok = look;
                move();
                x = new Arith(tok, x, term());
            }
            return x;
        }
        public Expr term() {
            Expr x = unary();
            while (look.tag == '*' || look.tag == '/') {
                Token tok = look;
                move();
                x = new Arith(tok, x, unary());
            }
            return x;
        }
        public Expr unary() {
            if (look.tag == '-') {
                move();
                return new Unary(Word.minus, unary());
            }
            else if (look.tag == '!') {
                Token tok = look;
                move();
                return new Not(tok, unary());
            }
            else {
                return factor();
            }
        }
        public Expr factor() {
            Expr x = null;
            switch (look.tag) {
                case '(':
                    move();
                    x = boolean();
                    match(')');
                    return x;
                case Tag.NUM:
                    x = new Constant(look, BasicType.Int);
                    move();
                    return x;
                case Tag.REAL:
                    x = new Constant(look, BasicType.Float);
                    move();
                    return x;
                case Tag.TRUE:
                    x = Constant.True;
                    move();
                    return x;
                case Tag.FALSE:
                    x = Constant.False;
                    move();
                    return x;
                case Tag.ID:
                    string s = look.ToString();
                    Id id = top.get(look);
                    if (id == null) {
                        error(look.ToString() + " undeclared");
                    }
                    move();
                    if (look.tag != '[') {
                        return id;
                    }
                    else {
                        return offset(id);
                    }
                default:
                    error("syntax error");
                    return x;
            }
        }
        Access offset(Id a) {
            Expr i;
            Expr w;
            Expr t1;
            Expr t2;
            Expr loc;
            BasicType type = a.type;
            match('[');
            i = boolean();
            match(']');
            type = ((BasicArray)type).of;
            w = new Constant(type.width);
            t1 = new Arith(new Token('*'), i, w);
            loc = t1;
            while (look.tag == '[') {
                match('[');
                i = boolean();
                match(']');
                type = ((BasicArray)type).of;
                w = new Constant(type.width);
                t1 = new Arith(new Token('*'), i, w);
                t2 = new Arith(new Token('+'), loc, t1);
                loc = t2;
            }
            return new Access(a, loc, type);
        }
    }
}
