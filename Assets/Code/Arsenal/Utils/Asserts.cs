using UnityEngine.Assertions;

namespace Code.Arsenal.Utils
{
    public static class Asserts
    {
        [System.Diagnostics.Conditional(Strings.kAssertions)]
        public static void Assert(bool condition, object message)
        {
#if UNITY_ASSERTIONS
            if (!condition)
                throw new AssertionException(null != message ? message.ToString() : "Assertion failed.", null);
#endif
        }
    }
}