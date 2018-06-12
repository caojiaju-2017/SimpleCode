using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDesignClient
{
    public class ItemClass
    {
        public Dictionary<string, InputData> inputDict = new Dictionary<string, InputData>();
        public OutputData outPut;

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
    }
}
