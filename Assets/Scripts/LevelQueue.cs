using Traffic;

static class LevelQue
{
    /// <summary>
    /// The vehicle sequence for the entire level
    /// </summary>
    /// <returns>
    /// Traffic/Queue
    /// </returns>
    public static Queue get()
    {
        Queue queue = new Queue();
        float time = 1f;

        //0:00

        //~0:14 Stationary cars
        addSequence1(ref queue, time);

        //~0:30 Moving column
        time += 15f;
        addSequence2(ref queue, time);

        //~0:44
        time += 11.5f;
        addSequence3(ref queue, time);
        
        return queue;
    }

    static void addSequence1(ref Queue queue, float time)
    {
        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.2f, 0f)
                .explodeAt(time + 5.0f)
                .get()
            );

        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.4f, 0f)
                .accelerateAt(time + 1.0f, 0.5f, 4f)
                .explodeAt(time + 6.0f)
                .get()
            );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(6)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 1.0f, 0.6f, 3.2f)
               .explodeAt(time + 7.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 0.6f, .6f)
               .accelerateAt(time + 2.7f, 0.55f, 1.5f)
               .explodeAt(time + 11.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 1f, .6f)
               .accelerateAt(time + 3.0f, 0.55f, 1.5f)
               .explodeAt(time + 11.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 2f, .6f)
               .accelerateAt(time + 3f, 0.7f, 1.4f)
               .accelerateAt(time + 5.4f, -0.2f, 0.5f)
               .explodeAt(time + 12.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.2f, 0f)
               .accelerateAt(time + 1.8f, 0.68f, 2.2f)
               .accelerateAt(time + 3.7f, 1.5f, 1f)
               .explodeAt(time + 13.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 1.8f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.5f, 1f)
               .explodeAt(time + 13.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 2f, 0.3f)
               .accelerateAt(time + 2.0f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.3f, 1f)
               .explodeAt(time + 14.0f)
               .get()
           );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time + 0.6f, 0f)
              .accelerateAt(time + 1.9f, 1.2f, 2f)
              .accelerateAt(time + 3.9f, -0.3f, 1f)
              .explodeAt(time + 9.0f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time + 9.5f, 1.4f)
              .accelerateAt(time + 10f, 0.4f, 1f)
              .accelerateAt(time + 12.5f, 2f, 1f)
              .explodeAt(time + 16.0f)
              .get()
          );

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(2)
             .appearAt(time + 7f, 3.5f, false)
             .accelerateAt(time + 8.5f, -1f, .9f)
             .explodeAt(time + 13.0f)
             .get()
         );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 12f, 2.0f)
            .accelerateAt(time + 13f, 0.4f, 1f)
            .explodeAt(time + 17.0f)
            .get()
        );
    }

    static void addSequence2(ref Queue queue, float time)
    {
        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.4f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.7f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 1.4f, 4.5f, false)
              .explodeAt(time + 15f)
              .get()
          );
    }

    static void addSequence3(ref Queue queue, float time)
    {
        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(2)
              .appearAt(time, 2.5f)
              .accelerateAt(time + 5f, 1, 0.5f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(2)
              .appearAt(time + 1.0f, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 5.1f, 1, 0.6f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(40.0f, 2.5f)
              .accelerateAt(46.0f, 1, 0.5f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 7.1f, 1, 0.6f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time, 2.5f)
              .accelerateAt(time + 5f, -1, 0.3f)
              .accelerateAt(time + 5.4f, +1, 1.3f)
              .explodeAt(time + 19f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time + 1.0f, 2.4f)
              .explodeAt(time + 19f)
              .accelerateAt(time + 5.1f, 1, 0.7f)
              .get()
          );

    }
}