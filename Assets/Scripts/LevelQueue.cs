using Traffic;


static class LevelQue
{
    public const float SEQUENCE_1_START = 14f;
    public const float SEQUENCE_2_START = 28.4f;
    public const float SEQUENCE_3_START = 41.7f;

    /// <summary>
    /// The vehicle sequence for the entire level
    /// </summary>
    /// <returns>
    /// Traffic/Queue
    /// </returns>
    public static Queue get(float timeNow, float skipForwardTime = -1f)
    {
        if(skipForwardTime != -1f)
        {
            timeNow -= skipForwardTime;
        }

        Queue queue = new Queue();

        if(shouldScheduleSequence(skipForwardTime, SEQUENCE_2_START))
        {
            addSequence1(ref queue, timeNow + SEQUENCE_1_START);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_3_START))
        {
            addSequence2(ref queue, timeNow + SEQUENCE_2_START);
        }

        addSequence3(ref queue, timeNow + SEQUENCE_3_START);
        
        return queue;
    }


    static private bool shouldScheduleSequence(float skipForwardTime, float nextSequenceStart)
    {
        return skipForwardTime < nextSequenceStart;
    }

    static public float findLatestSequence(float currentLevelTime)
    {
        if(currentLevelTime < SEQUENCE_2_START)
        {
            return SEQUENCE_1_START;
        }
        else if (currentLevelTime < SEQUENCE_3_START)
        {
            return SEQUENCE_2_START;
        }

        return SEQUENCE_3_START;
    }

    /// <summary>
    /// The first jummping sequence takes the player from the train on the right hand side
    /// to the second sequence on the left hand side
    /// </summary>
    /// <param name="queue"></param>
    /// <param name="time"></param>
    static void addSequence1(ref Queue queue, float time)
    {
        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.14f, 0f)
                .accelerateAt(time + 1.1f, 0.5f, 1.9f)
                .explodeAt(time + 4.0f)
                .get()
            );

        queue.addEntry(
            ScheduleEntryBuilder.start()
                .setLane(6)
                .appearAt(time + 0.4f, 0f)
                .accelerateAt(time + 1.0f, 0.48f, 2f)
                .explodeAt(time + 5.0f)
                .get()
            );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(5)
               .appearAt(time + 0.15f, .6f)
               .accelerateAt(time + 1.5f, 0.5f, .8f)
               .accelerateAt(time + 4f, 0.5f, 0.5f)
               .explodeAt(time + 6.5f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 0.2f, 0f)
               .accelerateAt(time + 1.0f, 1f, 1.3f)
               .explodeAt(time + 7.0f)
               .get()
           );


        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(3)
              .appearAt(time + 0.2f, 0.3f)
              .accelerateAt(time + 1.0f, 1f, 1.0f)
              .accelerateAt(time + 3.9f, -0.3f, 1f)
              .accelerateAt(time + 6f, 1f, 1f)
              .explodeAt(time + 9.3f)
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
            .explodeAt(time + 17.5f)
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