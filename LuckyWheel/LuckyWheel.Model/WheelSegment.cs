using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyWheel.Model
{
    public class WheelSegment : Entity
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Win { get; set; }
        public string ResultText { get; set; }
        public double Multiplier { get; set; }
    }
}
