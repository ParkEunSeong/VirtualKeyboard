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

        public char VALUE { get; }
        public Type TYPE { get; }
        public bool IsDot { get => VALUE == 'ㆍ'; }
        public bool IsDot2 { get => VALUE == 'ᆢ'; }
        public int IDX { get; set; }
        public static readonly string HOLE2 = "ᆢ";
        public static readonly string HOLE = "ㆍ";
        public Hangeul ( char value, Type type )
        {
            VALUE = value;
            TYPE = type;
        }
        
        public override string ToString()
        {
            return VALUE.ToString();
        }
        public static List<Hangeul> SplitHangeul(Hangeul ch)
        {
            var ret = new List<Hangeul>();
            char[] cho = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            char[] jung = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ',
                            'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
            char[] jong = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ',
                            'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            foreach (var it in cho)
            {
                if (it == ch.VALUE)
                    return new List<Hangeul>() { ch };
            }
            foreach (var it in jung)
            {
                if (it == ch.VALUE)
                    return new List<Hangeul>() { ch };
            }

            foreach (var it in jong)
            {
                if (it == ch.VALUE)
                    return new List<Hangeul>() { ch };
            }

            ushort mUniCode한글Base = 0xAC00;
            ushort mUniCode한글Last = 0xD79F;
            int i초성Idx, i중성Idx, i종성Idx; // 초성,중성,종성의 인덱스
            ushort uTempCode = 0x0000;       // 임시 코드용
            //Char을 16비트 부호없는 정수형 형태로 변환 - Unicode
            uTempCode = Convert.ToUInt16(ch.VALUE);
            // 캐릭터가 한글이 아닐 경우 처리
            //  if ((uTempCode < mUniCode한글Base) || (uTempCode > mUniCode한글Last))

            // iUniCode에 한글코드에 대한 유니코드 위치를 담고 이를 이용해 인덱스 계산.
            int iUniCode = uTempCode - mUniCode한글Base;
            i초성Idx = iUniCode / (21 * 28);
            iUniCode = iUniCode % (21 * 28);
            i중성Idx = iUniCode / 28;
            iUniCode = iUniCode % 28;
            i종성Idx = iUniCode;

            ret.Add(new Hangeul(cho[i초성Idx], Hangeul.Type.CHOSUNG));
            ret.Add(new Hangeul(jung[i중성Idx], Hangeul.Type.JUNGSUNG));
            if (jong[i종성Idx] != ' ')
                ret.Add(new Hangeul(jong[i종성Idx], Hangeul.Type.JONGSUNG));

            return ret;
        }
        public static Hangeul hangleHap(Hangeul c1, Hangeul c2, Hangeul c3 = null)  // 초성, 중성은 무조건 와야하며, 종성이 없으면 ' '을 입력해야함         
       ﻿{
            char[] cho = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            char[] jung = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ',
                            'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
            char[] jong = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ',
                            'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            int cho_i = 0, jung_i = 0, jong_i = 0;

            for (int i = 0; i < cho.Length; i++)
                if (cho[i] == c1.VALUE) cho_i = i;


            for (int i = 0; i < jung.Length; i++)
                if (jung[i] == c2.VALUE)
                    jung_i = i;

            if (c3 != null)
            {
                for (int i = 0; i < jong.Length; i++)
                    if (jong[i] == c3.VALUE) jong_i = i;
            }

            int uniValue = (cho_i * 21 * 28) + (jung_i * 28) + (jong_i) + 0xAC00;
            return new Hangeul((char)uniValue, Type.JAMO);
        }
    }
}
