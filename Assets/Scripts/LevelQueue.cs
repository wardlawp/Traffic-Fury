using Traffic;


static class LevelQue
{
    public const float SEQUENCE_1_START = 14f;
    public const float SEQUENCE_2_START = 28.4f;
    public const float SEQUENCE_3_START = 41.7f;

    /// <summary>
    /// The vehicle sequence for the entire level
    /// </summary>
    /// <param name="skipForwardTime">
    /// Optional parameter to skip some of the scedule for fast fowarding to a checkpoint
    /// </param>
    /// <returns>
    /// Traffic/Queue
    /// </returns>
    public static Queue get(float skipForwardTime = 0f)
    {
        Queue queue = new Queue();

        if(shouldScheduleSequence(skipForwardTime, SEQUENCE_2_START))
        {
            addSequence1(ref queue,  SEQUENCE_1_START - skipForwardTime);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_3_START))
        {
            addSequence2(ref queue, SEQUENCE_2_START - skipForwardTime);
        }

        addSequence3(ref queue, SEQUENCE_3_START - skipForwardTime);
        
        return queue;
    }


    /// <summary>
    /// Get the time to skip forward to based off the players time of death
    /// </summary>
    /// <param name="playerTimeOfDeath"></param>
    /// <returns>float</returns>
    static public float findCheckpointTime(float playerTimeOfDeath)
    {
        if(playerTimeOfDeath < SEQUENCE_2_START)
        {
            return SEQUENCE_1_START;
        }
        else if (playerTimeOfDeath < SEQUENCE_3_START)
        {
            return SEQUENCE_2_START;
        }

        return SEQUENCE_3_START;
    }

    /// <summary>
    /// Determine if the sequence should be scheduled based on the current skipForwardTime 
    /// </summary>
    /// <param name="skipForwardTime"></param>
    /// <param name="nextSequenceStart"></param>
    /// <returns>bool</returns>
    static private bool shouldScheduleSequence(float skipForwardTime, float nextSequenceStart)
    {
        return skipForwardTime < nextSequenceStart;
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