using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp27 {
    class Program {

        static void Main(string[] args) {
            //Parser parser = new Parser();
            //parser.expr();

            //Lexer l1 = new Lexer();
            //Token t1 = l1.scan();
            //Token t2 = l1.scan();
            //Token t3 = l1.scan();
            //Token t4 = l1.scan();
            //Token t5 = l1.scan();

            Lexer lex = new Lexer("test.src");
            Parser parse = new Parser(lex);
            parse.program();

            Console.ReadKey();
        }
    }

    
}
