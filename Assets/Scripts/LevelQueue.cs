using Traffic;

static class LevelQue
{
    public static Queue get()
    {
        Queue queue = new Queue();
        float time = 1f;

        //0:00

        //~0:14 Stationary cars
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
               .appearAt(time + 0.6f, 0f)
               .accelerateAt(time + 2.1f, 0.7f, 2.2f)
               .accelerateAt(time + 4.1f, 1.5f, 1f)
               .explodeAt(time + 13.0f)
               .get()
           );

        queue.addEntry(
           ScheduleEntryBuilder.start()
               .setLane(4)
               .appearAt(time + 2f, 0.3f, false)
               .accelerateAt(time + 2.1f, 0.7f, 2.2f)
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
              .accelerateAt(time + 6f, +0.3f, 1.5f)
              .accelerateAt(time + 11f, -0.3f, 2.5f)
              .explodeAt(time + 14.0f)
              .get()
          );

        queue.addEntry(
         ScheduleEntryBuilder.start()
             .setLane(2)
             .appearAt(time + 7f, 1.8f)
             .accelerateAt(time + 13f, 0.8f, 1f)
             .explodeAt(time + 16.0f)
             .get()
         );

        queue.addEntry(
        ScheduleEntryBuilder.start()
            .setLane(2)
            .appearAt(time + 12f, 1.8f)
            .accelerateAt(time + 13f, 0.8f, 1f)
            .explodeAt(time + 19.0f)
            .get()
        );


        //~0:30 Moving column
        time += 15f;

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

        //~0:44
        time += 11.5f;

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

        return queue;
    }
}