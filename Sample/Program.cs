namespace Sample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ObjectToStringExample objectToStringExample = new ObjectToStringExample();
            objectToStringExample.RunExample();

            StringToObjectExample stringToObjectExample = new StringToObjectExample();
            stringToObjectExample.RunExample();
        }
    }
}