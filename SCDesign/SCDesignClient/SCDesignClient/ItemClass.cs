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
        Parallelogram1 = 10010,
        Image100001 = 100001,
        Image100002 = 100002,
        Image100003 = 100003,
        Image100004 = 100004,
        Image100005 = 100005,
        Image100006 = 100006,
        Image100007 = 100007,
        Image100008 = 100008,
        Image100009 = 100009,
        Image100010 = 100010,
        Image100011 = 100011,
        Image100012 = 100012,
        Image100013 = 100013,
        Image100014 = 100014,
        Image100015 = 100015,
        Image100016 = 100016,
        Image100017 = 100017,
        Image100018 = 100018,
        Image100019 = 100019,
        Image100020 = 100020 ,

        Image200001 = 200001,
        Image200002 = 200002,
        Image200003 = 200003,
        Image200004 = 200004,
        Image200005 = 200005,
        Image200006 = 200006,
        Image200007 = 200007,
        Image200008 = 200008,
        Image200009 = 200009,
        Image200010 = 200010,
        Image200011 = 200011,
        Image200012 = 200012,
        Image200013 = 200013,
        Image200014 = 200014,
        Image200015 = 200015,
        Image200016 = 200016,
        Image200017 = 200017,
        Image200018 = 200018,
        Image200019 = 200019,
        Image200020 = 200020,


        Image300001 = 300001,
        Image300002 = 300002,
        Image300003 = 300003,
        Image300004 = 300004,
        Image300005 = 300005,
        Image300006 = 300006,
        Image300007 = 300007,
        Image300008 = 300008,
        Image300009 = 300009,
        Image300010 = 300010,
        Image300011 = 300011,
        Image300012 = 300012,
        Image300013 = 300013,
        Image300014 = 300014,
        Image300015 = 300015,
        Image300016 = 300016,
        Image300017 = 300017,
        Image300018 = 300018,
        Image300019 = 300019,
        Image300020 = 300020 

    }

    public enum ItemType
    {
        Control =  901,
        Module = 902,
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

        internal void printValue()
        {
            Console.WriteLine(itemInfo);
            Console.WriteLine(itemname);
        }
    }
}
