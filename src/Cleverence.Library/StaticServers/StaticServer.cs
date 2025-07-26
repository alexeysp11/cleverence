namespace Cleverence.Library.StaticServers;

public static class StaticServer
{
    private static int _count;
    private static readonly ReaderWriterLockSlim _rwLock;

    static StaticServer()
    {
        _count = 0;
        _rwLock = new ReaderWriterLockSlim();
    }
    
    public static int GetCount()
    {
        _rwLock.EnterReadLock();
        try
        {
            return _count;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        _rwLock.EnterWriteLock();
        try
        {
            _count = _count + value;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }
}
