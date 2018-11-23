using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27 {
    public class Env {
        private Dictionary<Token, Id> table = new Dictionary<Token, Id>();
        protected Env prev;
        public Env(Env p) {
            prev = p;
        }
        public void put(Token w, Id i) {
            table.Add(w, i);
        }
        public Id get(Token w) {
            for (Env e = this; e != null; e = e.prev) {
                Id found = e.table[w];
                if (found != null) {
                    return found;
                }
            }
            return null;
        }
    }
}
