using Traffic;


static class LevelQue
{
    public const float SEQUENCE_1_START = 14f;
    public const float SEQUENCE_2_START = 29.3f;
    public const float SEQUENCE_3_START = 42.7f;
    public const float SEQUENCE_4_START = 58.2f;
    public const float SEQUENCE_5_START = 70.2f;
    public const float SEQUENCE_6_START = 86.0f;

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

        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_2_START))
        {
            addSequence1(ref queue, SEQUENCE_1_START - skipForwardTime);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_3_START))
        {
            addSequence2(ref queue, SEQUENCE_2_START - skipForwardTime);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_4_START))
        {
            addSequence3(ref queue, SEQUENCE_3_START - skipForwardTime);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_5_START))
        {
            addSequence4(ref queue, SEQUENCE_4_START - skipForwardTime);
        }
        if (shouldScheduleSequence(skipForwardTime, SEQUENCE_6_START))
        {
            addSequence5(ref queue, SEQUENCE_5_START - skipForwardTime);
        }

        addSequence6(ref queue, SEQUENCE_6_START - skipForwardTime);

        return queue;
    }


    /// <summary>
    /// Get the time to skip forward to based off the players time of death
    /// </summary>
    /// <param name="playerTimeOfDeath"></param>
    /// <returns>float</returns>
    static public float findCheckpointTime(float playerTimeOfDeath)
    {
        if (playerTimeOfDeath < SEQUENCE_2_START)
        {
            return SEQUENCE_1_START;
        }
        else if (playerTimeOfDeath < SEQUENCE_3_START)
        {
            return SEQUENCE_2_START;
        }
        else if (playerTimeOfDeath < SEQUENCE_4_START)
        {
            return SEQUENCE_3_START;
        }
        else if (playerTimeOfDeath < SEQUENCE_5_START)
        {
            return SEQUENCE_4_START;
        }
        else if (playerTimeOfDeath < SEQUENCE_6_START)
        {
            return SEQUENCE_5_START;
        }

        return SEQUENCE_6_START;
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
            .explodeAt(time + 18.5f)
            .get()
        );
    }

    static void addSequence2(ref Queue queue, float time)
    {
        ScheduleEntry first = ScheduleEntryBuilder.start()
              .setLane(1)
              .appearAt(time, 4.5f, false)
              .accelerateAt(time + 0.5f, 0.4f, .5f)
              .accelerateAt(time + 3f, -0.2f, 1f)
              .explodeAt(time + 4f)
              .get();

        queue.addEntry(first);

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearRelative(time + 0.15f, 4.5f, first, 1.1f)
              .accelerateAt(time + 2f, 0.5f, 0.8f)
              .accelerateAt(time + 3f, -0.3f, 1.5f)
              .explodeAt(time + 7.5f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearRelative(time + 0.3f, 4.5f, first, 2.3f)
              .accelerateAt(time + 6f, 0.5f, 0.8f)
              .explodeAt(time + 11f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(1)
              .appearRelative(time + 0.5f, 4.5f, first, 3.4f)
              .accelerateAt(time + 8.7f, 1.2f, 0.8f)
              .accelerateAt(time + 12.3f, -.7f, 2f)
              .explodeAt(time + 16f)
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
              .accelerateAt(time + 3.5f, 1f, 1f)
              .explodeAt(time + 7f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(5)
              .appearAt(time + 1.0f, 2.5f)
              .accelerateAt(time + 6.2f, .7f, 3f)
              .explodeAt(time + 8.5f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(6)
              .appearAt(time + 7f, 1.9f)
              .accelerateAt(time + 8.8f, .8f, 2f)
              .explodeAt(time + 12f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(5)
              .appearAt(time + 8f, 2.0f)
              .accelerateAt(time + 11.5f, 1f, 1f)
              .explodeAt(time + 15f)
              .get()
          );

        queue.addEntry(
          ScheduleEntryBuilder.start()
              .setLane(4)
              .appearAt(time + 10.5f, 2.1f)
              .accelerateAt(time + 16f, .2f, 1f)
              .explodeAt(time + 17f)
              .get()
          );

    }

    static void addSequence4(ref Queue queue, float time)
    {
        ScheduleEntry first = ScheduleEntryBuilder.start()
             .setLane(3)
             .appearAt(time, 4f, false)
             .accelerateAt(time + 1f, -1.0f, 2f)
             .explodeAt(time + 3.5f)
             .get();

        queue.addEntry(first);

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(4)
             .appearRelative(time + 1f, 4f, first, 2.4f)
             .accelerateAt(time + 2.5f, -1.0f, 2.5f)
             .accelerateAt(time + 3f, .5f, 2.5f)
             .explodeAt(time + 4.8f)
             .get()
         );

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(5)
             .appearRelative(time + 2.5f, 4f, first, 2.4f)
             .accelerateAt(time + 3.5f, -1.0f, 2.5f)
             .explodeAt(time + 5.8f)
             .get()
         );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearRelative(time + 3.5f, 4f, first, 2.4f)
            .accelerateAt(time + 4.5f, -1.0f, 2.5f)
            .explodeAt(time + 6.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(5)
            .appearAt(time + 4.5f, 4f, false)
            .accelerateAt(time + 5.5f, -1.0f, 2.5f)
            .explodeAt(time + 7.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearAt(time + 5.5f, 4f, false)
            .accelerateAt(time + 6.5f, -1.5f, 2f)
            .explodeAt(time + 7.8f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 6.5f, 4f, false)
            .accelerateAt(time + 7.5f, -1.5f, 2f)
            .explodeAt(time + 9.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearAt(time + 7.5f, 4f, false)
            .accelerateAt(time + 8.5f, -1.5f, 2f)
            .explodeAt(time + 10.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(5)
            .appearAt(time + 8.5f, 4f, false)
            .accelerateAt(time + 9.5f, -1.5f, 2f)
            .explodeAt(time + 11.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearAt(time + 9.5f, 4f, false)
            .accelerateAt(time + 10.5f, -1.5f, 2f)
            .explodeAt(time + 12.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(5)
            .appearAt(time + 10.5f, 4f, false)
            .accelerateAt(time + 11.5f, -1.5f, 2f)
            .explodeAt(time + 13.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(6)
            .appearAt(time + 11.5f, 4f, false)
            .accelerateAt(time + 12.5f, -1.0f, 0.5f)
            .accelerateAt(time + 13f, 1f, 1.7f)
            .explodeAt(time + 15.5f)
            .get()
        );

    }

    static void addSequence5(ref Queue queue, float time)
    {

        ScheduleEntry first = ScheduleEntryBuilder.start()
            .setLane(5)
            .appearAt(time, 2f)
            .accelerateAt(time + 2f, 1.4f, 1f)
            .explodeAt(time + 4f)
            .get();

        queue.addEntry(first);


        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearRelative(time, 2f, first, -0.7f)
            //.accelerateAt(time + 3.5f, 1f, 1f)
            .explodeAt(time + 6f)
            .get()
        );

        time += 1.5f;

        ScheduleEntry third = ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 2f, 4f, false)
            .explodeAt(time + 5)
            .accelerateAt(time + 3f, 0.4f, 1f)
            .get();

        queue.addEntry(third);

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearRelative(time + 2.2f, 4f, third, 0.5f)
            .explodeAt(time + 6f)
            .accelerateAt(time + 3f, 0.3f, 1.2f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearRelative(time + 2.5f, 4f, third, 2.3f)
            .appearAt(time + 2.8f, 4f, false)
            .explodeAt(time + 6f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearRelative(time + 2.7f, 4f, third, 2.6f)
            .explodeAt(time + 6f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(1)
            .appearAt(time + 3.5f, 5.5f, false)
            .explodeAt(time + 7f)
            .accelerateAt(time + 3.6f, 0.3f, 2f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(1)
            .appearAt(time + 3.6f, 5.5f, false)
            .explodeAt(time + 8f)
            .accelerateAt(time + 3.7f, 0.3f, 2f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(1)
            .appearAt(time + 4f, 5.5f, false)
            .explodeAt(time + 10f)
            .accelerateAt(time + 5.4f, 0.7f, 2f)
            .accelerateAt(time + 7.5f, -0.7f, 2f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 3.7f, 6.2f, false)
            .explodeAt(time + 6.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
           .setLane(2)
           .appearAt(time + 7f, 7f, false)
           .explodeAt(time + 11f)
           .accelerateAt(time + 9.5f, -2f, 1f)
           .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
           .setLane(3)
           .appearAt(time + 7.5f, 7f, false)
           .accelerateAt(time + 10f, -2f, 1f)
           .explodeAt(time + 12f)
           .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
           .setLane(4)
           .appearAt(time + 8.5f, 7f, false)
           .accelerateAt(time + 10.5f, -2f, 1f)
           .explodeAt(time + 13.5f)
           .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
           .setLane(5)
           .appearAt(time + 10f, 7f, false)
           .accelerateAt(time + 11f, -2f, 1f)
           .explodeAt(time + 14f)
           .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
           .setLane(6)
           .appearAt(time + 11f, 7f, false)
           .accelerateAt(time + 11.5f, -2f, 1f)
           .accelerateAt(time + 15.5f, -1f, 1f)
           .explodeAt(time + 18f)
           .get()
        );

    }

    static void addSequence6(ref Queue queue, float time)
    {
        ScheduleEntry first = ScheduleEntryBuilder.start()
            .setLane(5)
            .appearAt(time, 6f, false)
            .explodeAt(time + 3.7f)
            .get();

        queue.addEntry(first);

        queue.addEntry(
           ScheduleEntryBuilder.start()
              .setLane(4)
              .appearRelative(time, 6f,first, 0.3f)
              .explodeAt(time + 4.3f)
              .get()
        );

        queue.addEntry(
           ScheduleEntryBuilder.start()
              .setLane(1)
              .appearRelative(time, 6f, first, 0.3f)
              .accelerateAt(time + 3f, -1f, .4f)
              .accelerateAt(time + 5f, -.5f, .7f)
              .explodeAt(time + 8f)
              .get()
        );

        queue.addEntry(
          ScheduleEntryBuilder.start()
             .setLane(3)
             .appearRelative(time, 6f, first, 0.3f)
             .explodeAt(time + 5.3f)
             .get()
       );

        queue.addEntry(
          ScheduleEntryBuilder.start()
             .setLane(3)
             .appearRelative(time, 6f, first, 1.3f)
             .accelerateAt(time + 5.3f, -3f, 1f)
             .accelerateAt(time + 6.3f, +2.2f, 1f)
             .explodeAt(time + 7f)
             .get()
       );

        queue.addEntry(
            ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 5.3f , 5.5f, false)
            .explodeAt(time + 8f)
            .get()
        );

        queue.addEntry(
           ScheduleEntryBuilder.start()
           .setLane(2)
           .appearAt(time + 5.3f, 5.5f, false)
           .accelerateAt(time + 7f, -1.0f, 2f)
           .explodeAt(time + 8.5f)
           .get()
       );

        //
        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(1)
            .appearAt(time + 6.5f, 6f, false)
            .accelerateAt(time + 8f, -1.0f, 2.5f)
            .explodeAt(time + 9.8f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 6.8f, 5.5f, false)
            .accelerateAt(time + 8.5f, 1.2f, 2f)
            .accelerateAt(time + 9.5f, -1.5f, 2f)
            .explodeAt(time + 10.5f)
            .get()
        );
       
        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 7.3f, 5.5f, false)
            .accelerateAt(time + 8.5f, 1.2f, 2f)
            .accelerateAt(time + 10f, -1.5f, 2f)
            .explodeAt(time + 11.6f)
            .get()
        );
        
        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 8.1f, 5.9f, false)
            .accelerateAt(time + 9.3f, 1.2f, 2f)
            .accelerateAt(time + 10.7f, -1.4f, 2f)
            .explodeAt(time + 12.4f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 8.5f, 5f, false)
            .accelerateAt(time + 9f, 1.4f, 2f)
            .explodeAt(time + 14.2f)
            .get()
        );
        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 10f, 5f, false)
            .accelerateAt(time + 10.2f, 2f, 2f)
            .explodeAt(time + 14.5f)
            .get()
        );

       queue.addEntry(
       ScheduleEntryBuilder.start()
           .setLane(1)
           .appearAt(time + 10.5f, 5f, false)
           .accelerateAt(time + 10.7f, 2.2f, 2f)
           .explodeAt(time + 15.1f)
           .get()
       );

        ScheduleEntry train = ScheduleEntryBuilder.start()
           .setLane(0)
           .appearAt(time + 13f, 11.5f, false)
           .get();

        train.carId = 1;
        train.xOverride = -3.86f;
        queue.addEntry(train);
        /*
        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 8.5f, 5f, false)
            .accelerateAt(time + 9.5f, -1.5f, 2f)
            .explodeAt(time + 11.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(4)
            .appearAt(time + 9.5f, 5f, false)
            .accelerateAt(time + 10.5f, -1.5f, 2f)
            .explodeAt(time + 12.5f)
            .get()
        );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(3)
            .appearAt(time + 10.5f, 5f, false)
            .accelerateAt(time + 11.5f, -1.5f, 2f)
            .explodeAt(time + 13.5f)
            .get()
        );*/



    }
}