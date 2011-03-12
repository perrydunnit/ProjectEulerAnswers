namespace ProjectEuler.Solutions.Problem4
{
    public struct ProductPalendrome
    {
        public readonly int First;
        public readonly int Second;
        public readonly int Product;
        public ProductPalendrome(int first, int second)
        {
            First = first;
            Second = second;
            Product = first*second;
        }
    }
}