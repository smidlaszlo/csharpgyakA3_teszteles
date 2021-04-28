using System;

namespace NegyzetNevter
{
    public class Negyzet
    {
        private double oldalhossz;

        public Negyzet(double oldal)
        {
            oldalhossz = oldal;
        }

        public double Oldalhossz
        {
            get
            {
                return oldalhossz;
            }
            set
            {
                oldalhossz = value;
            }
        }

        public double Atlo
        {
            get
            {
                return oldalhossz * Math.Sqrt(2);
            }
        }

        private void aa()
        {

        }

        public double Kerulet()
        {
            return oldalhossz * 4;
        }

        public double Terulet()
        {
            return Math.Pow(oldalhossz, 2);
        }

        public double TeglatestTerfogata(double magassag)
        {
            if (magassag <= 0)
                throw new ArgumentException("A magasságnak 0-nál nagyobbnak kell lennie!");
            
            return Terulet() * magassag;
        }
    }
}
