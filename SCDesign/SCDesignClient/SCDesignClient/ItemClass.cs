using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDesignClient
{
    public enum ItemShape
    {
        Triangle0 = 10001,
        Triangle1 = 10002,
        Circle = 10003,
        Square = 10004,
        Rectangle0 = 10005,
        Rectangle1 = 10006,
        Trapezoid0 = 10007,
        Trapezoid1 = 10008,
        Parallelogram0 = 10009,
        Parallelogram1 = 10010
    }

    public enum ItemType
    {
        Control =  901,
        Base = 902,
        Collection = 903
    }
    public class ItemClass
    {
        public Dictionary<string, InputData> inputDict = new Dictionary<string, InputData>();
        public OutputData outPut;
        public List<int> size;
        public List<int> location;
        public ItemShape shapetype;
        public ItemType itemtype;
        public string itemInfo;
        public string itemname;

        public class InputData
        {
            public int inputIndex;
            public Object maxValue;
            public Object minValue;
            public int type;
            public Object value;
        }

        public class OutputData
        {
            public int type;
            public Object value;
        }



        internal Point MyLocation()
        {
            return new Point(this.location[0], this.location[1]);
        }
    }
}
