using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using ModernBCL.Core.Guards;

namespace ModernBCL.Core.Tests.Guards
{
    public class GuardTests
    {
        // ===========================================================
        // CLASSIC GUARD METHODS
        // ===========================================================

        [Fact]
        public void AgainstNull_ShouldThrow_WhenNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Guard.AgainstNull(null, "value"));
        }

        [Fact]
        public void AgainstNull_ShouldNotThrow_WhenNotNull()
        {
            Guard.AgainstNull("hello", "value");
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_ShouldThrow_WhenEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.AgainstNullOrWhiteSpace("", "name"));
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_ShouldThrow_WhenWhitespace()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.AgainstNullOrWhiteSpace("   ", "name"));
        }

        [Fact]
        public void AgainstNullOrWhiteSpace_ShouldNotThrow_WhenValid()
        {
            Guard.AgainstNullOrWhiteSpace("Alice", "name");
        }

        [Fact]
        public void AgainstOutOfRange_ShouldThrow_WhenValueTooSmall()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.AgainstOutOfRange(-1, 0, 10, "num"));
        }

        [Fact]
        public void AgainstOutOfRange_ShouldThrow_WhenValueTooLarge()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.AgainstOutOfRange(11, 0, 10, "num"));
        }

        [Fact]
        public void AgainstOutOfRange_ShouldNotThrow_WhenInsideRange()
        {
            Guard.AgainstOutOfRange(5, 0, 10, "num");
        }

        [Fact]
        public void AgainstDefault_ShouldThrow_WhenDefault()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.AgainstDefault(Guid.Empty, "id"));
        }

        [Fact]
        public void AgainstDefault_ShouldNotThrow_WhenNotDefault()
        {
            Guard.AgainstDefault(Guid.NewGuid(), "id");
        }

        [Fact]
        public void AgainstNegative_ShouldThrow_WhenNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.AgainstNegative(-5, "num"));
        }

        [Fact]
        public void AgainstNegative_ShouldNotThrow_WhenZero()
        {
            Guard.AgainstNegative(0, "num");
        }

        [Fact]
        public void AgainstZero_ShouldThrow_WhenZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.AgainstZero(0, "num"));
        }

        [Fact]
        public void AgainstZero_ShouldNotThrow_WhenNonZero()
        {
            Guard.AgainstZero(5, "num");
        }

        [Fact]
        public void AgainstEmptyEnumerable_ShouldThrow_WhenEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.AgainstEmptyEnumerable(new int[] { }, "list"));
        }

        [Fact]
        public void AgainstEmptyEnumerable_ShouldNotThrow_WhenNotEmpty()
        {
            Guard.AgainstEmptyEnumerable(new[] { 1, 2 }, "list");
        }


        // ===========================================================
        // FLUENT OBJECT GUARDS
        // ===========================================================

        [Fact]
        public void Fluent_Null_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Guard.Against((object)null, "value").Null());
        }

        [Fact]
        public void Fluent_Null_ShouldNotThrow()
        {
            Guard.Against((object)"hello", "value").Null();
        }

        [Fact]
        public void Fluent_WhiteSpace_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.Against("   ", "name").WhiteSpace());
        }

        [Fact]
        public void Fluent_WhiteSpace_ShouldNotThrow()
        {
            Guard.Against("Alice", "name").WhiteSpace();
        }

        [Fact]
        public void Fluent_EmptyEnumerable_ShouldThrow()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.Against((IEnumerable)new int[] { }, "list").EmptyEnumerable());
        }

        [Fact]
        public void Fluent_EmptyEnumerable_ShouldNotThrow()
        {
            Guard.Against((IEnumerable)new[] { 1 }, "list").EmptyEnumerable();
        }

        [Fact]
        public void Fluent_Default_Object_ShouldThrow_ForDefaultT()
        {
            Assert.Throws<ArgumentException>(() =>
                Guard.Against((object)Guid.Empty, "id").Default<Guid>());
        }

        [Fact]
        public void Fluent_Default_Object_ShouldNotThrow()
        {
            Guard.Against((object)Guid.NewGuid(), "id").Default<Guid>();
        }


        // ===========================================================
        // FLUENT NUMERIC GUARDS
        // ===========================================================

        [Fact]
        public void Fluent_LessThan_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.Against(5, "amount").LessThan(10)); // 5 < 10 → throw
        }

        [Fact]
        public void Fluent_LessThan_ShouldNotThrow()
        {
            Guard.Against(10, "amount").LessThan(10); // 10 >= 10 → ok
        }

        [Fact]
        public void Fluent_GreaterThan_ShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.Against(20, "amount").GreaterThan(10)); // 20 > 10 → throw
        }

        [Fact]
        public void Fluent_GreaterThan_ShouldNotThrow()
        {
            Guard.Against(10, "amount").GreaterThan(10); // 10 <= 10 → ok
        }

        [Fact]
        public void Fluent_Between_ShouldThrow_BelowMin()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.Against(4, "value").Between(5, 10));
        }

        [Fact]
        public void Fluent_Between_ShouldThrow_AboveMax()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Guard.Against(11, "value").Between(5, 10));
        }

        [Fact]
        public void Fluent_Between_ShouldNotThrow_WhenInsideRange()
        {
            Guard.Against(7, "value").Between(5, 10);
        }


        // ===========================================================
        // COMBINED FLUENT RULES
        // ===========================================================

        [Fact]
        public void Fluent_MultipleRules_ShouldThrowOnFirstInvalid()
        {
            Assert.Throws<ArgumentNullException>(() =>
                Guard.Against((object)null, "user")
                     .Null()
                     .WhiteSpace() // Never reached
            );
        }

        [Fact]
        public void Fluent_MultipleNumericRules_ShouldWork()
        {
            Guard.Against(50, "amount")
                 .LessThan(0)        // 50 >= 0
                 .GreaterThan(1000)  // 50 <= 1000
                 .Default();         // not default(int)
        }
    }
}
