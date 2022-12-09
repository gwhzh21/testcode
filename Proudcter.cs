using DotNext.Threading.Channels;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConsoleAppNext
{
    public class Proudcter
    {
        private static async Task Produce(ChannelWriter<decimal> writer)
        {
            for (decimal i = 0M; i < 1000; i++)
            {
                await writer.WriteAsync(i);
            }
        }

        private static int testline = 0;
        private static async Task Consume(ChannelReader<decimal> reader)
        {
            try
            {
                
                while (await reader.WaitToReadAsync())
                {
                    await Task.Delay(100);
                    while (reader.TryRead(out var item))
                    {
                        Console.WriteLine(item);
                        testline += 1;
                    }

                }
                Console.WriteLine(testline);
                Console.WriteLine("f");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(testline);
            }
            
            //Assert.Equal(i, await reader.ReadAsync());
        }

        public static async Task ProduceConsumeConcurrently()
        {
            using (var channel = new MySerializationChannel<decimal>(new PersistentChannelOptions
            { Location="e:/testdb", PartitionCapacity = 100 }))
            {   
                //var consumer = Consume(channel.Reader);
                var producer = Produce(channel.Writer);
                await Task.WhenAll(
                    //consumer
                    //,
                    producer
                    );
            }
            
            using (var channel = new MySerializationChannel<decimal>(new PersistentChannelOptions
            { Location = "e:/testdb", PartitionCapacity = 100 }))
            {
                //var consumer = Consume(channel.Reader);
                var producer = Produce(channel.Writer);
                await Task.WhenAll(
                    //consumer
                    //,
                    producer
                    );
            }

            try
            {
                using (var channel = new MySerializationChannel<decimal>(new PersistentChannelOptions
                { Location = "e:/testdb", PartitionCapacity = 100 }))
                {
                    var consumer = Consume(channel.Reader);
                    //var producer = Produce(channel.Writer);
                    await Task.WhenAll(
                        consumer
                        //,
                        //producer
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                using (var channel = new MySerializationChannel<decimal>(new PersistentChannelOptions
                { Location = "e:/testdb", PartitionCapacity = 100 }))
                {
                    var consumer = Consume(channel.Reader);
                    //var producer = Produce(channel.Writer);
                    await Task.WhenAll(
                        consumer
                        //,
                        //producer
                        );
                }
            }
        }
    }
}
