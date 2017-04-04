using System;
using System.Collections.Generic;

namespace Traffic
{
    public class ScheduleEntry
    {
        private static int idxIncrementer = 1;
        public int id;

        public ScheduleEntry()
        {
            id = idxIncrementer++;
            events = new List<TrafficEvent>();
        }

        public int lane { get; set; }

        public List<TrafficEvent> events { get; set; }

        public TrafficEvent appearance()
        {
            foreach (TrafficEvent e in events)
            {
                if (e.type == TrafficEvent.types.Appear)
                {
                    return e;
                }
            }

            return null;
        }

        public float appearAt()
        {
            float result = -1.0f;

            foreach (TrafficEvent e in events)
            {
                if (e.type == TrafficEvent.types.Appear)
                {
                    result = e.time;
                    break;
                }
            }

            //Todo throw some sort of exception if we didn't have an Appear event
            return result;
        }

        public bool appearAtBottom()
        {
            foreach (TrafficEvent e in events)
            {
                if (e.type == TrafficEvent.types.Appear)
                {
                    return e.appearAtBottom;
                }
            }

            return false;
        }


    }

    public class TrafficEvent
    {
        public enum types { Appear, Accelerate, InitiateExplosion, Explode };
        public types type;

        //Appear at time data
        public bool appearAtBottom;
        public float time;

        //Appear relative information
        public int? otherScheduleRef = null;
        public float relativeDistance;

        public float speed;

        //Acceleration Information
        public float duration;
        public float rate;

        public bool occuring(float timeNow)
        {
            bool startOccured = (time < timeNow);

            if(type == types.Accelerate)
            {
                bool endOccured = (time + duration) < timeNow;
                return (startOccured && !endOccured);
            }

            return startOccured;
        }
    }
}
