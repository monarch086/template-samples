using UID.ConsoleUtils;
using UID.Persistance;

namespace UID.App;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var watch = new StopWatch();

        var ulids = new List<Ulid>();
        var guids = new List<Guid>();

        watch.Start();
        for (int i = 0; i < 10000; i++)
        {
            ulids.Add(Ulid.NewUlid());
            guids.Add(Guid.NewGuid());
        }
        watch.Stop();
        watch.Print("uids generating");

        var repo = new Repository();

        watch.Start();
        await repo.InsertBulk(ulids);
        watch.Stop();
        watch.Print("ulids saving");

        watch.Start();
        await repo.InsertBulk(guids);
        watch.Stop();
        watch.Print("guids saving");
    }
}