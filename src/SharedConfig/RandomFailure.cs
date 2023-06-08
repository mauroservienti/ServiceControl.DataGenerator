public static class RandomFailure
{
    static Random rnd = new Random();
    public static void MaybeItFails()
    {
        var number = rnd.Next(0, 20);
        if (number == 6)
        {
            throw new InvalidOperationException("Oooops, random error");
        }
    }
}
