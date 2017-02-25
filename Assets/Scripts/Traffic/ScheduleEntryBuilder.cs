using System.Collections.Generic;
using System;

namespace Assets.Scripts.Traffic
{

    

    public class BuilderException: Exception
    {
        public BuilderException (string message): base(message) {}
    }


    public class ScheduleEntryBuilder
    {
        private ScheduleEntry entry;
        private static Random rand;
        public static int carIdxMax;

        public static ScheduleEntryBuilder start()
        {
            if(rand == null)
            {
                rand = new Random();
            }

            return new ScheduleEntryBuilder();
        }


        public ScheduleEntryBuilder()
        {
            entry = new ScheduleEntry();
            entry.carType = rand.Next(0, carIdxMax);
        }

        public ScheduleEntryBuilder setLane(int lane)
        {
            entry.lane = lane;

            return this;
        }

        public ScheduleEntryBuilder setCarType(int carType)
        {
            entry.carType = carType;

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
