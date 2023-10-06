namespace Redelisten.Data.Extensions
{

    public static class SpanExtensions
    {

        public static void Replace(this Span<char> replaceIn, char replaceThis, char toThis)
        {

            for (int i = 0; i < replaceIn.Length; i++)
            {
                if (replaceIn[i] != replaceThis) continue;

                replaceIn[i] = toThis;
            }

        }

    }

}