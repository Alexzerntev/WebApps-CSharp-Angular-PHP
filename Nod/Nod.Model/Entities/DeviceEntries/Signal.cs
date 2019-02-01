using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public class Signal
    {
        public int Index { get; set; }
        public int Value { get; set; }

        public Signal()
        {
        }

        public Signal(int index, int value)
        {
            Index = index;
            Value = value;
        }
    }
}
