using System;

namespace NerveMeter.Data
{
    public struct SensorReading
    {
        public readonly int SensorId;
        public readonly long Value;
        public readonly DateTime Time;

        public SensorReading(long value, int sensorId, DateTime time)
        {
            Value = value;
            SensorId = sensorId;
            Time = time;
        }
    }
}
