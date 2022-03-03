using System.Diagnostics;
using Utilities;

namespace MonkeyPatcher.MonkeyPatch.Concrete;

public static class MonkeyPatcherFactory
{
    private static bool _available = true;
    private static readonly object Lock = new();

    private const long TimeOut = 5000;
    private const int MaxScanningDepth = 5;

    /// <summary>
    /// Generates an instance of the monkey patcher.
    /// maxScanningDepth allows you to specify how deep the analyzer should scan for methods to override.
    /// A shallow scan will execute very fast but
    /// you must make sure that your method to override is not called in a deeper layer of your method, or the mapping will be faulty
    /// </summary>
    /// <param name="sut"></param>
    /// <param name="maxScanningDepth"></param>
    /// <returns></returns>
    public static MonkeyPatch GetMonkeyPatch(Delegate sut, int maxScanningDepth)
        => InternalGetPatcher(sut, maxScanningDepth, TimeOut);

    /// <summary>
    /// Generates an instance of the monkey patcher.
    /// Set the timeout for the factory to return the patcher. (synchronization checks are done twice, the timeout is reset for each).
    /// </summary>
    /// <param name="sut"></param>
    /// <param name="maxScanningDepth"></param>
    /// <param name="timeOut"></param>
    /// <returns></returns>
    public static MonkeyPatch GetMonkeyPatch(Delegate sut, int maxScanningDepth, long timeOut)
        => InternalGetPatcher(sut, maxScanningDepth, timeOut);

    /// <summary>
    /// Generates an instance of the monkey patcher.
    /// maxScanningDepth allows you to specify how deep the analyzer should scan for methods to override.
    /// A shallow scan will execute very fast but
    /// you must make sure that your method to override is not called in a deeper layer of your method, or the mapping will be faulty.
    /// Set the timeout for the factory to return the patcher. (synchronization checks are done twice, the timeout is reset for each).
    /// </summary>
    /// <param name="sut"></param>
    /// <param name="timeOut"></param>
    /// <returns></returns>
    public static MonkeyPatch GetMonkeyPatch(Delegate sut, long timeOut)
        => InternalGetPatcher(sut, MaxScanningDepth, timeOut);

    /// <summary>
    /// Generates an instance of the monkey patcher.
    /// </summary>
    /// <param name="sut"></param>
    /// <returns></returns>
    public static MonkeyPatch GetMonkeyPatch(Delegate sut)
        => InternalGetPatcher(sut, MaxScanningDepth, TimeOut);


    private static MonkeyPatch InternalGetPatcher(Delegate sut, int maxScanningDepth, long timeOut)
    {
        //Double check to make sure the thread is aware of the state of the object upon entering the lock.
        WaitForAccess(timeOut);
        lock (Lock)
        {
            WaitForAccess(timeOut);
            _available = false;
            return new MonkeyPatch(Disposed, sut.Method, maxScanningDepth);
        }
    }



    private static void WaitForAccess(long timeOut)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        while (!_available)
        {
             /* Wait for the previous test to complete */
             (stopWatch.ElapsedMilliseconds < timeOut)
                 .ThrowIfAssumptionFailed($@"
Timed out at {(double)timeOut/1000} seconds
This error is often caused by the patcher not being disposed.
Make sure you prepend all of your MonkeyPatch instances with 'using'");
        }
        stopWatch.Stop();
    }


    private static void Disposed(ref bool disposed)
    {
        _available = disposed;
    }
}