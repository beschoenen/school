namespace ALGA
{
    public class Bubblesort
    {
        public static void bubblesort(ISortList list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count - i - 1; j++)
                {
                    if (list.compare(j, j + 1) <= 0) continue;

                    list.swap(j, j + 1);
                }
            }
        }
    }
}
