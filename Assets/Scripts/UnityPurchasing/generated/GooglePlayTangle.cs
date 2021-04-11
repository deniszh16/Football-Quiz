#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("5X/Sg1SUV4b9e7Pxg/rp5uJZnM9OhZ8xiegGq+K54Z1GVKzzlv58vhsRj7+xAOrx4zhkRbmI8bUhUcOHj+bsyYVwOxBVYxwpff8CoB1jXRiRVwlB3g59oZBWjSmLcqA9z/tIGFWR6mynEECr/KMcBUCTF23jqkVs8lUvsIQ3pNCy/oHvmAQSuCidKwulD3zgYBQW2B1A9EPdnufN96C8CCTNicDrOFEN/tQlBUfT7YbsRQ2kftjVirXRlm5Brk1piFCX+udHnFvBQkxDc8FCSUHBQkJDw6ZRkhjQ33PBQmFzTkVKacULxbROQkJCRkNA8ef+Mzvn1rNUkU93evStW48DTSJjZz/f8JoR1HxYiBRKXucILD1gGERQjJtrigyq3kFAQkNC");
        private static int[] order = new int[] { 3,8,11,7,13,5,6,11,9,13,13,11,13,13,14 };
        private static int key = 67;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
