using System.Collections.Generic;
using System;

namespace Assets.Scripts.TrafficController
{

    public class TrafficQue
    {
        private List<TrafficScheduleEntry> list;

        public int lenght { get { return list.Count; } }

        public TrafficQue()
        {
            list = new List<TrafficScheduleEntry>();
        }

        public void addEntry(TrafficScheduleEntry newEntry)
        {
            bool entered = false;
            foreach (TrafficScheduleEntry entry in list)
            {
                if(entry.appearAt() > newEntry.appearAt())
                {
                    entered = true;
                    list.Insert(list.IndexOf(entry), newEntry);
                    break;
                }
            }

            if (!entered)
            {
                //Place at end
                list.Add(newEntry);
            }
        }


        public List<TrafficScheduleEntry> getCurrentsEntries(float timeNow)
        {
            List<TrafficScheduleEntry> result = new List<TrafficScheduleEntry>();
            List<TrafficScheduleEntry> removeItems = new List<TrafficScheduleEntry>();

            foreach (TrafficScheduleEntry entry in list)
            {
                if(entry.appearAt() < timeNow)
                {
                    result.Add(entry);
                    removeItems.Add(entry);
                }
            }

            foreach (TrafficScheduleEntry entryToBeRemoved in removeItems)
                list.Remove(entryToBeRemoved);

            return result;
        }
    }

    public class BuilderException: Exception
    {
        public BuilderException (string message): base(message) {}
    }


    public class TrafficEntryBuilder
    {
        private TrafficScheduleEntry entry;

        public static TrafficEntryBuilder start()
        {
            return new TrafficEntryBuilder();
        }


        public TrafficEntryBuilder()
        {
            entry = new TrafficScheduleEntry();
        }

        public TrafficEntryBuilder setLane(int lane)
        {
            entry.lane = lane;

            return this;
        }

        public TrafficEntryBuilder setCarType(int carType)
        {
            entry.carType = carType;

            return this;
        }

        public TrafficEntryBuilder appearAt(float time, float speed, bool appearAtBottom = true)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Appear;
            e.time = time;
            e.speed = speed;
            e.appearAtBottom = appearAtBottom;

            entry.events.Add(e);

            return this;
        }

        public TrafficEntryBuilder accelerateAt(float time, float newSpeed, float accelDuration)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Accelerate;
            e.time = time;
            e.speed = newSpeed;
            e.duration = accelDuration;

            entry.events.Add(e);

            return this;
        }

        public TrafficEntryBuilder explodeAt(float time)
        {
            TrafficEvent e = new TrafficEvent();

            e.type = TrafficEvent.types.Explode;
            e.time = time;

            entry.events.Add(e);

            return this;
        }

        public TrafficScheduleEntry get()
        {
            if (!entry.hasAppearance())
            {
                throw new BuilderException("TrafficScheduleEntry must have appear event");
            }

            return entry;
        }

    }


    public class TrafficScheduleEntry
    {
        public TrafficScheduleEntry()
        {
            events = new List<TrafficEvent>();
        }
        
        public int carType { get; set; }
        public int lane { get; set; }

        public List<TrafficEvent> events { get; set; }

        public bool hasAppearance()
        {
            foreach (TrafficEvent e in events)
            {
                if (e.type == TrafficEvent.types.Appear)
                {
                    return true;
                }
            }

            return false;
        }

        public float appearAt()
        {
            float result = -1.0f;

            foreach(TrafficEvent e in events)
            {
                if(e.type == TrafficEvent.types.Appear)
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
        public enum types { Appear, Accelerate, Explode };
        public types type;

        public bool appearAtBottom;
        public float time;
        public float speed;
        public float duration;
    }
}
