using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AsyncBridge.Tests
{
    [TestClass]
    public class WhenAnyTests
    {
        [TestMethod]
        public async Task GenericArrayWithSomeContents()
        {
            List<TaskCompletionSource<int>> taskCompletionSources = new List<TaskCompletionSource<int>>
                                                                    {
                                                                        new TaskCompletionSource<int>(),
                                                                        new TaskCompletionSource<int>(),
                                                                        new TaskCompletionSource<int>()
                                                                    };
            Task<int> whenAnyTask = TaskUtils.WhenAny(taskCompletionSources.Select(tcs => tcs.Task).ToArray());
            taskCompletionSources[1].SetResult(2);
            int result = await whenAnyTask;
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task GenericIEnumerableWithSomeContents()
        {
            List<TaskCompletionSource<int>> taskCompletionSources = new List<TaskCompletionSource<int>>
                                                                    {
                                                                        new TaskCompletionSource<int>(),
                                                                        new TaskCompletionSource<int>()
                                                                    };
            Task<int> whenAnyTask = TaskUtils.WhenAny(taskCompletionSources.Select(tcs => tcs.Task));
            taskCompletionSources[1].SetResult(2);
            int result = await whenAnyTask;
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public async Task EnumerableWithSomeContents()
        {
            List<TaskCompletionSource<int>> taskCompletionSources = new List<TaskCompletionSource<int>>
                                                                    {
                                                                        new TaskCompletionSource<int>(),
                                                                        new TaskCompletionSource<int>()
                                                                    };
            Task whenAnyTask = TaskUtils.WhenAny(taskCompletionSources.Select(tcs => (Task)tcs.Task));
            Assert.IsFalse(whenAnyTask.IsCompleted);
            taskCompletionSources[1].SetResult(2);
            await whenAnyTask;
        }

    }
}