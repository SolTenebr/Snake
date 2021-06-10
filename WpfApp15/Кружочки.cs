using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp15
{
    class Кружочки
    {
        private int x;
        private int y;
        public Кружочки()
        {
            x = 0;
            y = 0;
        }
        public void PlusX()
        {
            this.x++;
        }
        public void PlusY()
        {
            this.y++;
        }
        public void MinoosY()
        {
            this.y--;
        }
        public void MinoosX()
        {
            this.x--;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public void SetX(int x)
        {
            this.x = x;
        }
        public void SetY(int y)
        {
            this.y = y;
        }
    }
}
