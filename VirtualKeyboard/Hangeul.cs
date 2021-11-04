using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public class Hangeul
    {
        public enum Type
        {
            CHOSUNG,
            JUNGSUNG,
            JONGSUNG,
            JAMO
        }
        public enum Division
        {
            CONSONANT,
            COMPOSITION_CONSONANT,
            VOWEL
        }
        public char VALUE { get; }
        public Type TYPE { get; }
        public Division DIVISION { get; }
        public Hangeul ( char value, Division division )
        {
            VALUE = value;
            DIVISION = division;
        }
    }
}
