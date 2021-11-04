using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualKeyboard
{
    public partial class Form1 : Form
    {
        private List<HeavenKeypad> m_keyPadList = new List<HeavenKeypad>();
        
        private List<Hangeul> m_inputList = new List<Hangeul>();
        private HeavenKeypad m_lastKeypad;
        private Hangeul m_lastHangeul;
        public Form1()
        {
            InitializeComponent();
            Initialize();
            SplitHangeul('쑤');
        }
        
        public void Initialize()
        {
            m_keyPadList.Add(new HeavenKeypad(0, new List<Hangeul>{
                new Hangeul('ㅣ', Hangeul.Division.VOWEL) }));
            m_keyPadList.Add(new HeavenKeypad(1, new List<Hangeul>{
                new Hangeul('ㆍ', Hangeul.Division.VOWEL) }));
            m_keyPadList.Add(new HeavenKeypad(2, new List<Hangeul>{
                new Hangeul('ㅡ', Hangeul.Division.VOWEL) }));
            m_keyPadList.Add(new HeavenKeypad(3, new List<Hangeul>{
                new Hangeul('ㄱ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅋ', Hangeul.Division.CONSONANT),
                new Hangeul('ㄲ', Hangeul.Division.CONSONANT)}));
            m_keyPadList.Add(new HeavenKeypad(4, new List<Hangeul>{
                new Hangeul('ㄴ', Hangeul.Division.CONSONANT),
                new Hangeul('ㄹ', Hangeul.Division.CONSONANT)
            }));
            m_keyPadList.Add(new HeavenKeypad(5, new List<Hangeul>{
                new Hangeul('ㄷ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅌ', Hangeul.Division.CONSONANT),
                new Hangeul('ㄸ', Hangeul.Division.CONSONANT)
            }));
            m_keyPadList.Add(new HeavenKeypad(6, new List<Hangeul>{
                new Hangeul('ㅂ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅍ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅃ', Hangeul.Division.CONSONANT)
            }));
            m_keyPadList.Add(new HeavenKeypad(7, new List<Hangeul>{
                new Hangeul('ㅅ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅎ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅆ', Hangeul.Division.CONSONANT)
            }));
            m_keyPadList.Add(new HeavenKeypad(8, new List<Hangeul>{
                new Hangeul('ㅈ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅊ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅉ', Hangeul.Division.CONSONANT)
            }));
            m_keyPadList.Add(new HeavenKeypad(9, new List<Hangeul>{
                new Hangeul('ㅇ', Hangeul.Division.CONSONANT),
                new Hangeul('ㅁ', Hangeul.Division.CONSONANT),
            }));


        }

        private IEnumerable<HeavenKeypad> GetKey(int idx)
        {
            var ch = from keypad in m_keyPadList
                     where keypad.IDX == idx
                     select keypad;
            return ch;
        }

        private void OnButtonClickedKey(object sender, EventArgs e)
        {
            var ctrl = sender as Button;
            int idx = int.Parse(ctrl.Tag.ToString());
            var keyPad = GetKey(idx).First();
            var hangeul = keyPad.Current;
            var value = hangeul.VALUE;
            char[] arrCh;
            if (m_inputList.Count == 0)
            {
                m_inputList.Add(hangeul);
            }
            else if (m_lastKeypad.IDX == idx)
            {
                arrCh = textBox1.Text.ToArray();
                arrCh[arrCh.Length - 1] = value;
                m_inputList[m_inputList.Count - 1] = hangeul;
            }
            else
            {
                var compHangeul = CompositionHangeul(m_lastHangeul, hangeul );
                m_inputList[m_inputList.Count - 1] = hangeul;
            }

            m_lastKeypad = keyPad;
            m_lastHangeul = hangeul;
            textBox1.Text = new string(GetArrayInputChar());

        }
        public char[] GetArrayInputChar()
        {
            var ret = new List<char>();
            for ( int i = 0; i < m_inputList.Count; i++ )
            {
                ret.Add(m_inputList[i].VALUE);
            }
            return ret.ToArray();
        }
        public void SplitHangeul(char ch )
        {
            char[] cho = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            char[] jung = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ',
                            'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
            char[] jong = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ',
                            'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
        
            string m초성;
        string m중성;
        string m종성;
        ushort mUniCode한글Base = 0xAC00;
             ushort mUniCode한글Last = 0xD79F;
        int i초성Idx, i중성Idx, i종성Idx; // 초성,중성,종성의 인덱스
            ushort uTempCode = 0x0000;       // 임시 코드용
            //Char을 16비트 부호없는 정수형 형태로 변환 - Unicode
            uTempCode = Convert.ToUInt16(ch);
            // 캐릭터가 한글이 아닐 경우 처리
            if ((uTempCode < mUniCode한글Base) || (uTempCode > mUniCode한글Last))
            {
                m초성 = ""; m중성 = ""; m종성 = "";
            }
            // iUniCode에 한글코드에 대한 유니코드 위치를 담고 이를 이용해 인덱스 계산.
            int iUniCode = uTempCode - mUniCode한글Base;
            i초성Idx = iUniCode / (21 * 28);
            iUniCode = iUniCode % (21 * 28);
            i중성Idx = iUniCode / 28;
            iUniCode = iUniCode % 28;
            i종성Idx = iUniCode;
            m초성 = new string(cho[i초성Idx], 1);
            m중성 = new string(jung[i중성Idx], 1);
            m종성 = new string(jong[i종성Idx], 1);
        }
        public Hangeul CompositionHangeul(Hangeul last, Hangeul cur )
        {
            char value = ' ';
            if (last.DIVISION == Hangeul.Division.CONSONANT
                 && cur.DIVISION == Hangeul.Division.CONSONANT)
            {
                if (last.VALUE == 'ㄱ' && cur.VALUE == 'ㅅ')
                {
                    value = 'ㄳ';
                }
                else if (last.VALUE == 'ㄴ' && cur.VALUE == 'ㅈ')
                {
                    value = 'ㄵ';
                }
                else if (last.VALUE == 'ㄴ' && cur.VALUE == 'ㅎ')
                {
                    value = 'ㄶ';
                }
            }
            else if (last.DIVISION == Hangeul.Division.CONSONANT
                && cur.DIVISION == Hangeul.Division.VOWEL)
            {
                value = hangleHap(last.VALUE, cur.VALUE, ' ');
            }
            
            if (value != ' ')
                return new Hangeul(value, Hangeul.Division.COMPOSITION_CONSONANT);
            else
                return null;
                
        }
       
        public static char hangleHap(char c1, char c2, char c3)  // 초성, 중성은 무조건 와야하며, 종성이 없으면 ' '을 입력해야함         
       ﻿{
            char[] cho = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            char[] jung = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ',
                            'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
            char[] jong = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ',
                            'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
            int cho_i = 0, jung_i = 0, jong_i = 0;
         
            for (int i = 0; i < cho.Length; i++)
                if (cho[i] == c1) cho_i = i;


            for (int i = 0; i < jung.Length; i++)
                if (jung[i] == c2) jung_i = i;

            for (int i = 0; i < jong.Length; i++)
                if (jong[i] == c3) jong_i = i;

            int uniValue = (cho_i * 21 * 28) + (jung_i * 28) + (jong_i) + 0xAC00;
            return (char)uniValue;
        }
        
    }
}
