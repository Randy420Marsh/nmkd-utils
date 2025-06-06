﻿

using System.Diagnostics;

namespace NmkdUtils
{
    public class CodeUtils
    {
        /// <summary> Checks the failure condition <paramref name="failureCondition"/>. Executes the action <paramref name="failureAction"/> and returns true if the condition is met. </summary>
        public static bool Assert(Func<bool> failureCondition, Action failureAction = null)
        {
            if (failureCondition())
            {
                failureAction?.Invoke();
                return true;
            }

            return false;
        }

        /// <inheritdoc cref="Assert(Func{bool}, Action)"/>
        public static bool Assert(bool failureCondition, Action failureAction = null)
        {
            return Assert(() => failureCondition, failureAction);
        }

        // Function to set a variable via ref only if a given condition is met
        public static void SetIf<T>(ref T variable, T value, Func<bool> condition)
        {
            if (condition())
                variable = value;
        }

        public static void SetIfNotNull<T>(ref T variable, object? value)
        {
            if (value != null)
                variable = (T)value;
        }

        /// <summary>
        /// Shortcut for a while loop that has a time limit, a sleep interval, and an optional break condition.
        /// </summary>
        public static void LimitedWhile(int timeoutMs, int intervalMs, Func<bool>? breakCondition = null, Action? action = null)
        {
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (breakCondition?.Invoke() == true)
                    break;

                action?.Invoke();
                Thread.Sleep(intervalMs);
            }
        }
    }
}
