using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard
{
    public class HeavenKeypad : IEnumerator<Hangeul>
    {
        private int m_pos;
        private List<Hangeul> m_hangeulList;
        public int IDX { get; }
        public int COUNT => m_hangeulList.Count;

        public HeavenKeypad( int idx, List<Hangeul> hangeuls )
        {
            IDX = idx;
            m_hangeulList = hangeuls;
            foreach( var it in hangeuls )
            {
                it.IDX = idx;
            }

        }
        public override string ToString()
        {
            return Current.ToString();
        }
        public Hangeul Current
        {
            get
            {
                var cur = m_pos;
                m_pos++;
                if (m_pos >= m_hangeulList.Count)
                {
                    Reset();
                }
                return m_hangeulList[cur];
            }
        }

        object IEnumerator.Current => m_hangeulList[m_pos];

        public void Dispose()
        {
           
        }

        public bool MoveNext()
        {
            if ( m_pos >= m_hangeulList.Count )
            {
                Reset();
            }
            return true;
        }

        public void Reset()
        {
            m_pos = 0;
        }
    }
}
