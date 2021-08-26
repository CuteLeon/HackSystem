using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HackSystem.Web.Scheduler.Program.IDGenerator.Tests;

public class PIDGeneratorTests
{
    [Fact()]
    public void GetAvailablePIDTest()
    {
        IPIDGenerator generator = new PIDGenerator();
        var pidPool = new HashSet<int>();

        Parallel.For(0, 10000, x =>
        {
            var pid = generator.GetAvailablePID();
            lock (pidPool)
            {
                Assert.DoesNotContain(pid, pidPool);
                pidPool.Add(pid);
            }
        });

        Assert.Equal(10000, pidPool.Count);
    }
}
