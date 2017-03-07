using System.Collections.Generic;
using System;

namespace Traffic
{
    public class BuilderException: Exception
    {
        public BuilderException (string message): base(message) {}
    }

    public class ScheduleEntryBuilder
    {
        private ScheduleEntry entry;

        public static ScheduleEntryBuilder start()
        {
            return new ScheduleEntryBuilder();
        }

        public ScheduleEntryBuilder()
        {
            entry = new ScheduleEntry();
        }

        public ScheduleEntryBuilder setLane(int lane)
        {
            entry.lane = lane;

            return this;
        }

        public ScheduleEntryBuilder appearAt(float time, float speed, bool appearAtBottom = true)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Appear;
            e.time = time;
            e.speed = speed;
            e.appearAtBottom = appearAtBottom;

            entry.events.Add(e);

            return this;
        }

        public ScheduleEntryBuilder accelerateAt(float time, float rate, float accelDuration)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Accelerate;
            e.time = time;
            e.rate = rate;
            e.duration = accelDuration;

            entry.events.Add(e);

            return this;
        }

        public ScheduleEntryBuilder explodeAt(float time)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Explode;
            e.time = time;

            entry.events.Add(e);

            TrafficEvent be = new TrafficEvent();

            be.type = TrafficEvent.types.InitiateExplosion;
            be.time = time - 1;
            be.duration = 0.1f;

            entry.events.Add(be);

            return this;
        }

        public ScheduleEntry get()
        {
            if (entry.appearance() == null)
            {
                throw new BuilderException("TrafficScheduleEntry must have appear event");
            }

            return entry;
        }
    }
}
