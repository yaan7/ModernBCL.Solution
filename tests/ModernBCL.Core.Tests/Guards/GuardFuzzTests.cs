using System;
using System.Collections;
using Xunit;
using ModernBCL.Core.Guards;

namespace ModernBCL.Core.Tests.Guards
{
    public class GuardFuzzTests
    {
        private readonly Random _rnd = new Random();

        // ===========================================================
        // 1. CLASSIC GUARD: STRING VALIDATION
        // ===========================================================

        [Fact]
        public void Fuzz_String_NullEmptyWhitespace()
        {
            for (int i = 0; i < 2000; i++)
            {
                int type = _rnd.Next(4);
                string value;

                switch (type)
                {
                    case 0:     // NULL → ArgumentException (because string.IsNullOrWhiteSpace(null) == true)
                        value = null;
                        Assert.Throws<ArgumentException>(() =>
                            Guard.AgainstNullOrWhiteSpace(value, "name"));
                        break;

                    case 1:     // EMPTY → ArgumentException
                        value = "";
                        Assert.Throws<ArgumentException>(() =>
                            Guard.AgainstNullOrWhiteSpace(value, "name"));
                        break;

                    case 2:     // WHITESPACE → ArgumentException
                        value = new string(' ', _rnd.Next(1, 8));
                        Assert.Throws<ArgumentException>(() =>
                            Guard.AgainstNullOrWhiteSpace(value, "name"));
                        break;

                    default:    // VALID → no throw
                        value = RandomString();
                        Guard.AgainstNullOrWhiteSpace(value, "name");
                        break;
                }
            }
        }

        // ===========================================================
        // 2. FLUENT OBJECT GUARD (Null + WhiteSpace)
        // ===========================================================

        [Fact]
        public void Fuzz_FluentObject_String()
        {
            for (int i = 0; i < 2000; i++)
            {
                string value;
                int type = _rnd.Next(4);

                switch (type)
                {
                    case 0: // NULL → Null() throws ArgumentNullException
                        value = null;
                        Assert.Throws<ArgumentNullException>(() =>
                            Guard.Against(value, "str").Null());
                        break;

                    case 1: // EMPTY → WhiteSpace() throws ArgumentException
                        value = "";
                        Assert.Throws<ArgumentException>(() =>
                            Guard.Against(value, "str")
                                 .Null()        // passes because not null
                                 .WhiteSpace()); // throws here
                        break;

                    case 2: // WHITESPACE → ArgumentException
                        value = "   ";
                        Assert.Throws<ArgumentException>(() =>
                            Guard.Against(value, "str")
                                 .Null()
                                 .WhiteSpace());
                        break;

                    default: // VALID → no exception
                        value = RandomString();
                        Guard.Against(value, "str").Null().WhiteSpace();
                        break;
                }
            }
        }

        // ===========================================================
        // 3. NUMERIC FUZZ — LessThan
        // ===========================================================

        [Fact]
        public void Fuzz_Numeric_LessThan()
        {
            for (int i = 0; i < 3000; i++)
            {
                int threshold = _rnd.Next(-1000, 1000);
                int value = _rnd.Next(-5000, 5000);

                if (value < threshold)
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        Guard.Against(value, "num").LessThan(threshold));
                }
                else
                {
                    Guard.Against(value, "num").LessThan(threshold);
                }
            }
        }

        // ===========================================================
        // 4. NUMERIC FUZZ — GreaterThan
        // ===========================================================

        [Fact]
        public void Fuzz_Numeric_GreaterThan()
        {
            for (int i = 0; i < 3000; i++)
            {
                int threshold = _rnd.Next(-1000, 1000);
                int value = _rnd.Next(-5000, 5000);

                if (value > threshold)
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        Guard.Against(value, "num").GreaterThan(threshold));
                }
                else
                {
                    Guard.Against(value, "num").GreaterThan(threshold);
                }
            }
        }

        // ===========================================================
        // 5. NUMERIC FUZZ — Between
        // ===========================================================

        [Fact]
        public void Fuzz_Numeric_Between()
        {
            for (int i = 0; i < 3000; i++)
            {
                int min = _rnd.Next(-2000, 0);
                int max = _rnd.Next(0, 2000);
                int value = _rnd.Next(-5000, 5000);

                if (value < min || value > max)
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        Guard.Against(value, "num").Between(min, max));
                }
                else
                {
                    Guard.Against(value, "num").Between(min, max);
                }
            }
        }

        // ===========================================================
        // 6. DEFAULT VALUE FUZZ (Guid, int)
        // ===========================================================

        [Fact]
        public void Fuzz_DefaultValues()
        {
            for (int i = 0; i < 1500; i++)
            {
                // GUID
                Guid guid = (i % 2 == 0) ? Guid.Empty : Guid.NewGuid();
                if (guid == Guid.Empty)
                {
                    Assert.Throws<ArgumentException>(() =>
                        Guard.AgainstDefault(guid, "id"));
                }
                else
                {
                    Guard.AgainstDefault(guid, "id");
                }

                // INT default = 0
                int number = _rnd.Next(0, 6) == 0 ? 0 : _rnd.Next(1, 5000);
                if (number == 0)
                {
                    Assert.Throws<ArgumentException>(() =>
                        Guard.AgainstDefault(number, "num"));
                }
                else
                {
                    Guard.AgainstDefault(number, "num");
                }
            }
        }

        // ===========================================================
        // 7. COLLECTION FUZZ — Empty vs non-empty
        // ===========================================================

        [Fact]
        public void Fuzz_Collections()
        {
            for (int i = 0; i < 2000; i++)
            {
                int len = _rnd.Next(0, 4);

                int[] arr = (len == 0)
                    ? new int[] { }  // empty → must throw
                    : new int[] { 1, 2, 3 };

                if (len == 0)
                {
                    Assert.Throws<ArgumentException>(() =>
                        Guard.AgainstEmptyEnumerable(arr, "arr"));
                }
                else
                {
                    Guard.AgainstEmptyEnumerable(arr, "arr");
                }
            }
        }

        // ===========================================================
        // 8. UTILITY: Random strings
        // ===========================================================

        private string RandomString()
        {
            int length = _rnd.Next(1, 16);
            var chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                int type = _rnd.Next(3);

                switch (type)
                {
                    case 0: chars[i] = (char)_rnd.Next(48, 58); break;  // digits
                    case 1: chars[i] = (char)_rnd.Next(65, 91); break; // uppercase
                    default: chars[i] = (char)_rnd.Next(97, 123); break; // lowercase
                }
            }

            return new string(chars);
        }
    }
}
