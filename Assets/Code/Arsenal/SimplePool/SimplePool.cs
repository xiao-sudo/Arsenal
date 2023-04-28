using System;
using System.Collections.Generic;
using System.Text;
using Code.Arsenal.Utils;
using UnityEngine;

namespace Code.Arsenal.SimplePool
{
    public static class SimplePool
    {
        public static T Acquire<T>() where T : class, new()
        {
            return SimplePool<T>.Acquire();
        }

        public static void Release<T>(T elem) where T : class, new()
        {
            SimplePool<T>.Release(elem);
        }

        #region StringBuilder

        public static StringBuilder AcquireStringBuilder()
        {
            var builder = SimplePool<StringBuilder>.Acquire();
            Asserts.Assert(0 == builder.Length, $"A pooled {nameof(StringBuilder)} is not empty");

            return builder;
        }

        public static void Release(StringBuilder builder)
        {
            builder.Length = 0;
            SimplePool<StringBuilder>.Release(builder);
        }

        public static string ReleaseToString(this StringBuilder builder)
        {
            var rs = builder.ToString();
            Release(builder);
            return rs;
        }

        #endregion

        #region Disposable

        public static class Disposable
        {
            public static SimplePool<T>.Disposable Acquire<T>(out T elem) where T : class, new()
            {
                return new SimplePool<T>.Disposable(out elem);
            }
        }

        #endregion
    }

    public static class SimplePool<T> where T : class, new()
    {
        private static readonly List<T> ELEMS = new List<T>();

        public static int Count
        {
            get => ELEMS.Count;
            set
            {
                var count = ELEMS.Count;
                if (count < value)
                {
                    if (ELEMS.Capacity < value)
                        ELEMS.Capacity = Mathf.NextPowerOfTwo(value);

                    do
                    {
                        ELEMS.Add(new T());
                        ++count;
                    } while (count < value);
                }
                else if (count > value)
                {
                    ELEMS.RemoveRange(value, count - value);
                }
            }
        }

        public static int Capacity
        {
            get => ELEMS.Capacity;
            set
            {
                if (ELEMS.Count > value)
                    ELEMS.RemoveRange(value, ELEMS.Count - value);

                ELEMS.Capacity = value;
            }
        }

        public static T Acquire()
        {
            var count = ELEMS.Count;
            if (0 == count)
            {
                return new T();
            }

            --count;
            var elem = ELEMS[count];
            ELEMS.RemoveAt(count);

            return elem;
        }

        public static void Release(T elem)
        {
            Asserts.Assert(null != elem, $"Null object must not be released into {nameof(SimplePool<T>)}");
            ELEMS.Add(elem);
        }


        #region Disposable

        public readonly struct Disposable : IDisposable
        {
            private readonly T m_Elem;

            private readonly Action<T> m_OnRelease;

            public Disposable(out T elem, Action<T> on_release = null)
            {
                m_Elem = elem = Acquire();
                m_OnRelease = on_release;
            }

            public void Dispose()
            {
                m_OnRelease?.Invoke(m_Elem);
                Release(m_Elem);
            }
        }

        #endregion
    }
}