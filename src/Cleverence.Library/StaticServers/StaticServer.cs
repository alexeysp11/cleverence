namespace Cleverence.Library.StaticServers;

public static class StaticServer
{
    private static int _count;
    
    public static int GetCount()
    {
        return _count;
    }

    public static void AddToCount(int value)
    {
        _count = _count + value;
    }
}
