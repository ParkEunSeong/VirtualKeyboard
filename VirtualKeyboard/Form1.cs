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
        private List<CombineHangeul> m_combineHangeulList = new List<CombineHangeul>();
        private HeavenKeypad m_lastKeypad;
        private Hangeul m_lastInputValue;
        private CombineHangeul m_curHangeul = new VirtualKeyboard.CombineHangeul();
        
        public Form1()
        {
            InitializeComponent();
            Initialize();
            
        }
        
        public void Initialize()
        {
            m_keyPadList.Add(new HeavenKeypad(0, new List<Hangeul>{
                new Hangeul('ㅣ', Hangeul.Type.JUNGSUNG) }));
            m_keyPadList.Add(new HeavenKeypad(1, new List<Hangeul>{
                new Hangeul('ㆍ',  Hangeul.Type.JUNGSUNG) }));
            m_keyPadList.Add(new HeavenKeypad(2, new List<Hangeul>{
                new Hangeul('ㅡ',  Hangeul.Type.JUNGSUNG) }));
            m_keyPadList.Add(new HeavenKeypad(3, new List<Hangeul>{
                new Hangeul('ㄱ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅋ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㄲ', Hangeul.Type.CHOSUNG)}));
            m_keyPadList.Add(new HeavenKeypad(4, new List<Hangeul>{
                new Hangeul('ㄴ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㄹ', Hangeul.Type.CHOSUNG)
            }));
            m_keyPadList.Add(new HeavenKeypad(5, new List<Hangeul>{
                new Hangeul('ㄷ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅌ',Hangeul.Type.CHOSUNG),
                new Hangeul('ㄸ',Hangeul.Type.CHOSUNG)
            }));
            m_keyPadList.Add(new HeavenKeypad(6, new List<Hangeul>{
                new Hangeul('ㅂ',Hangeul.Type.CHOSUNG),
                new Hangeul('ㅍ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅃ', Hangeul.Type.CHOSUNG)
            }));
            m_keyPadList.Add(new HeavenKeypad(7, new List<Hangeul>{
                new Hangeul('ㅅ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅎ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅆ', Hangeul.Type.CHOSUNG)
            }));
            m_keyPadList.Add(new HeavenKeypad(8, new List<Hangeul>{
                new Hangeul('ㅈ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅊ', Hangeul.Type.CHOSUNG),
                new Hangeul('ㅉ',Hangeul.Type.CHOSUNG)
            }));
            m_keyPadList.Add(new HeavenKeypad(9, new List<Hangeul>{
                new Hangeul('ㅇ',Hangeul.Type.CHOSUNG),
                new Hangeul('ㅁ', Hangeul.Type.CHOSUNG),
            }));


        }

        private IEnumerable<HeavenKeypad> GetKey(int idx)
        {
            var ch = from keypad in m_keyPadList
                     where keypad.IDX == idx
                     select keypad;
            return ch;
        }
        private void AddInput( Hangeul hangeul )
        {
            if ( m_inputList.Count > 0 )
            {
                m_inputList[m_inputList.Count - 1] = hangeul;
            }
            else
            {
                m_inputList.Add(hangeul);
            }
        }
        private void OnButtonClickedKey(object sender, EventArgs e)
        {
            var ctrl = sender as Button;
            int idx = int.Parse(ctrl.Tag.ToString());
            if (idx != 11 && idx != 10)
            {
                var keyPad = GetKey(idx).First();
                var curInputValue = keyPad.Current;
                if (m_lastKeypad != null && m_lastKeypad != keyPad )
                {
                    m_lastKeypad.Reset();
                }
                var value = curInputValue.VALUE;


                if (curInputValue.TYPE == Hangeul.Type.JUNGSUNG
                    && !m_lastInputValue.IsDot && !curInputValue.IsDot
                    && m_curHangeul.GetCurrentType() == Hangeul.Type.JONGSUNG
                    )
                {
                    var jongsung = m_curHangeul.GetJONGSUNG();
                    m_combineHangeulList.Add(m_curHangeul);
                    m_curHangeul = new CombineHangeul(jongsung, curInputValue);
                }
                else
                {
                    m_curHangeul.AddHangeul(curInputValue);
                }

              
                m_lastKeypad = keyPad;
                m_lastInputValue = curInputValue;
                var str = "";

                
                foreach ( var it in m_combineHangeulList)
                {
                    str += it.VALUE;
                }
                str += m_curHangeul.VALUE;
                
                textBox1.Text = str;
            }
            else
            {
          //     var ret = CombineHangeul(m_inputList );
       //         m_inputList.Clear();
         //       m_completionList.Add(ret);
            }
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
        public bool IsAvaliableCombine( List<Hangeul> combine )
        {
            return true;
        }

   
    }
}
