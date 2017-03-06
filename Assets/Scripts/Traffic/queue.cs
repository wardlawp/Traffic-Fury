using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traffic
{
    class Queue
    {
        private List<ScheduleEntry> list;

        public int lenght { get { return list.Count; } }

        public Queue()
        {
            list = new List<ScheduleEntry>();
        }

        public void addEntry(ScheduleEntry newEntry)
        {
            bool entered = false;
            foreach (ScheduleEntry entry in list)
            {
                if (entry.appearAt() > newEntry.appearAt())
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


        public List<ScheduleEntry> getCurrentEntries(float timeNow)
        {
            List<ScheduleEntry> result = new List<ScheduleEntry>();
            List<ScheduleEntry> removeItems = new List<ScheduleEntry>();

            foreach (ScheduleEntry entry in list)
            {
                if (entry.appearAt() < timeNow)
                {
                    result.Add(entry);
                    removeItems.Add(entry);
                }
            }

            foreach (ScheduleEntry entryToBeRemoved in removeItems)
                list.Remove(entryToBeRemoved);

            return result;
        }
    }
}
