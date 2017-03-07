using Traffic;


static class LevelQue
{
    /// <summary>
    /// The vehicle sequence for the entire level
    /// </summary>
    /// <returns>
    /// Traffic/Queue
    /// </returns>
    public static Queue get(float timeNow)
    {
        Queue queue = new Queue();
        float time = timeNow + 1f;

        //0:00

        //~0:14 Stationary cars
        addSequence1(ref queue, time);

        //~0:30 Moving column
        time += 14.4f;
        addSequence2(ref queue, time);

        //~0:41.5
        time += 13.3f;
        addSequence3(ref queue, time);
        
        return queue;
    }

    static void addSequence1(ref Queue queue, float time)
    {
        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.2f, 0f)
                .accelerateAt(time + 1.1f, 0.5f, 1.9f)
                .explodeAt(time + 3.0f)
                .get()
            );

        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.4f, 0f)
                .accelerateAt(time + 1.0f, 0.5f, 4f)
                .explodeAt(time + 4.0f)
                .get()
            );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(6)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 1.0f, 0.6f, 3.2f)
               .explodeAt(time + 5.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 0.6f, .6f)
               .accelerateAt(time + 2.7f, 0.55f, 1.5f)
               .explodeAt(time + 4.5f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 1f, .6f)
               .accelerateAt(time + 3.0f, 0.55f, 1.5f)
               .explodeAt(time + 7.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 2f, .6f)
               .accelerateAt(time + 3f, 0.7f, 1.4f)
               .accelerateAt(time + 5.4f, -0.2f, 0.5f)
               .explodeAt(time + 7.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.2f, 0f)
               .accelerateAt(time + 1.8f, 0.68f, 2.2f)
               .accelerateAt(time + 3.7f, 1.5f, 1f)
               .explodeAt(time + 7.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 1.8f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.5f, 1f)
               .explodeAt(time + 6.8f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 2f, 0.3f)
               .accelerateAt(time + 2.0f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.3f, 1f)
               .explodeAt(time + 9.0f)
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
              .explodeAt(time + 15.0f)
              .get()
          );

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(2)
             .appearAt(time + 7f, 3.5f, false)
             .accelerateAt(time + 8.5f, -1f, .9f)
             .explodeAt(time + 12.5f)
             .get()
         );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 12f, 2.0f)
            .accelerateAt(time + 13f, 0.4f, 1f)
            .explodeAt(time + 16.5f)
            .get()
        );
    }

    static void addSequence2(ref Queue queue, float time)
    {
        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time, 4.5f, false)
              .accelerateAt(time + 0.5f, 0.4f, .5f)
              .accelerateAt(time + 3f, -0.2f, 1f)
              .explodeAt(time + 4f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.4f, 4.5f, false)
              .accelerateAt(time + 2f, 0.5f, 0.8f)
              .accelerateAt(time + 3f, -0.3f, 1.5f)
              .explodeAt(time + 7.5f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 0.7f, 4.5f, false)
              .accelerateAt(time + 6f, 0.5f, 0.8f)
              .explodeAt(time + 11f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time + 1.4f, 4.5f, false)
              .accelerateAt(time + 8.7f, 1.2f, 0.8f)
              .accelerateAt(time + 12.3f, -.7f, 2f)
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
              .explodeAt(time + 3f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time, 2.4f)
              .explodeAt(time + 3.9f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time, 2.5f)
              .accelerateAt(time + 3.5f, .6f, 1f)
              .explodeAt(time + 5.7f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time + 1.0f, 2.5f)
              .accelerateAt(time + 6f, .6f, 3f)
              .explodeAt(time + 9f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(5)
              .appearAt(time + 7f, 1.9f)
              .accelerateAt(time + 8.8f, .8f, 2f)
              .explodeAt(time + 12f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time + 8f, 2.0f)
              .accelerateAt(time + 11.5f, 1f, 1f)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(5)
              .appearAt(time + 10.5f, 2.1f)
              .explodeAt(time + 18f)
              .get()
          );

    }
}