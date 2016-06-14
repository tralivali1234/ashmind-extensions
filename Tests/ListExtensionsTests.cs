﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using Xunit.Extensions;

namespace AshMind.Extensions.Tests {
    public class ListExtensionsTests {
        public delegate IList<int> ListFactory(params int[] values);

        [Theory]
        [PropertyData(nameof(Lists))]
        public void InsertRange_WorksCorrectly(Expression<ListFactory> factory) {
            var list = factory.Compile()(0, 1, 2, 3, 4);
            list.InsertRange(3, new[] { 21, 22, 23 });

            Assert.Equal(
                new[] { 0, 1, 2, 21, 22, 23, 3, 4 },
                list.ToArray()
            );
        }

        [Theory]
        [PropertyData(nameof(Lists))]
        public void RemoveRange_WorksCorrectly(Expression<ListFactory> factory) {
            var list = factory.Compile()(0, 1, 2, 3, 4);
            list.RemoveRange(1, 3);

            Assert.Equal(new[] { 0, 4 }, list.ToArray());
        }

        public static IEnumerable<object[]> Lists {
            get {
                Func<Expression<ListFactory>, object[]> adapt = f => new object[] { f };

                yield return adapt(xs => new List<int>(xs));
                yield return adapt(xs => new Collection<int>(xs.ToList()));
            }
        }
    }
}
