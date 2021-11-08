using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public class CombineHangeul
    {
        private Hangeul CHOSUNG { get;  set; }
        private Hangeul JUNGSUNG { get; set; }
        private Hangeul JONGSUNG { get;  set; }
        public string VALUE {
            get
            {
                var hangeuls = Combine();
                var ret = "";
                foreach( var it in hangeuls )
                {
                    if ( it != null )
                        ret += it.VALUE;
                }
                return ret;
            }
        }
        public CombineHangeul()
        {

        }
        public CombineHangeul( Hangeul chosung, Hangeul jungsung = null, Hangeul jongsung  = null )
        {
            CHOSUNG = chosung;
            JUNGSUNG = jungsung;
            JONGSUNG = jongsung;
        }
        public List<Hangeul> Combine()
        {

            if ( CHOSUNG != null && JUNGSUNG != null)
            {
                if ( JONGSUNG != null)
                {
                    return new List<Hangeul>() { Hangeul.hangleHap(CHOSUNG, JUNGSUNG, JONGSUNG) };
                }
                else
                {
                    if (!JUNGSUNG.IsDot)
                        return new List<Hangeul>() { Hangeul.hangleHap(CHOSUNG, JUNGSUNG) };
                }
            }

            return new List<Hangeul>() { CHOSUNG, JUNGSUNG, JONGSUNG };
        }
        public Hangeul CombineDot( Hangeul input )
        {
            if (JUNGSUNG.VALUE == 'ㅣ' && input.IsDot)
                return new Hangeul('ㅏ', Hangeul.Type.JUNGSUNG);
            else if (JUNGSUNG.VALUE == 'ㅏ' && input.IsDot)
                return new Hangeul('ㅑ', Hangeul.Type.JUNGSUNG);

            else if (JUNGSUNG.IsDot && input.VALUE == 'ㅡ')
                return new Hangeul('ㅗ', Hangeul.Type.JUNGSUNG);
            else if (JUNGSUNG.VALUE == 'ㅗ' && input.IsDot)
                return new Hangeul('ㅛ', Hangeul.Type.JUNGSUNG);

            else if (JUNGSUNG.VALUE == 'ㅡ' && input.IsDot)
                return new Hangeul('ㅜ', Hangeul.Type.JUNGSUNG);
            else if (JUNGSUNG.VALUE == 'ㅜ' && input.IsDot)
                return new Hangeul('ㅠ', Hangeul.Type.JUNGSUNG);

            else if (JUNGSUNG.IsDot && input.VALUE == 'ㅣ')
                return new Hangeul('ㅓ', Hangeul.Type.JUNGSUNG);
            else if (JUNGSUNG.IsDot2 && input.VALUE == 'ㅣ')
                return new Hangeul('ㅕ', Hangeul.Type.JUNGSUNG);

            else if (JUNGSUNG.IsDot && input.VALUE == 'ㆍ')
                return new Hangeul('ᆢ', Hangeul.Type.JUNGSUNG);
            else if (JUNGSUNG.VALUE == 'ㅗ' && input.VALUE == 'ㅣ')
                return new Hangeul('ㅚ', Hangeul.Type.JUNGSUNG);
            return null;
        }
        public bool AddHangeul( Hangeul hangeul )
        {
            if (JONGSUNG != null && JONGSUNG.IDX != hangeul.IDX 
                && ( JONGSUNG != null && hangeul.TYPE == Hangeul.Type.CHOSUNG )
                || ( JONGSUNG != null && hangeul.TYPE == Hangeul.Type.JUNGSUNG ) )
            {
                return false;
            }

            if ( hangeul.TYPE == Hangeul.Type.CHOSUNG && JUNGSUNG == null )
            {
                CHOSUNG = hangeul;
            }
            else if ( hangeul.TYPE == Hangeul.Type.JUNGSUNG )
            {
                if (JUNGSUNG != null && hangeul.IsDot )
                {
                    
                    JUNGSUNG = CombineDot(hangeul);
                }
                else if ( JUNGSUNG != null && !hangeul.IsDot )
                {
                    JUNGSUNG = CombineDot(hangeul);
                }
                else
                    JUNGSUNG = hangeul;
            }
            else if ( hangeul.TYPE == Hangeul.Type.CHOSUNG)
            {
                JONGSUNG = hangeul;
            }
            if ( IsComplete() )
            {
                var list = Combine();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null )
                        Debug.Print(list[i].VALUE.ToString());
                }
            }
            return true;
        }
        public Hangeul.Type GetCurrentType()
        {
            if (CHOSUNG == null) return Hangeul.Type.CHOSUNG;
            else if (JUNGSUNG == null) return Hangeul.Type.JUNGSUNG;
            else return Hangeul.Type.JONGSUNG;
        }
        public bool IsComplete()
        {
            if (CHOSUNG != null && JUNGSUNG != null)
                return true;
            return false;
        }
        public void RemoveLast()
        {
            if (JONGSUNG != null)
            {
                JONGSUNG = null;
            }
            else if (JUNGSUNG != null)
            {
                JUNGSUNG = null;
            }
            else
            {
                CHOSUNG = null;
            }        
        }
        public Hangeul GetJONGSUNG()
        {
            var ret = JONGSUNG;
            JONGSUNG = null;
            return ret;
        }
        public Hangeul GetJUNGSUNG()
        {
            var ret = JUNGSUNG;
            JUNGSUNG = null;
            return ret;
        }
        public Hangeul GetCHOSUNG()
        {
            var ret = CHOSUNG;
            CHOSUNG = null;
            return ret;
        }
    }
}
