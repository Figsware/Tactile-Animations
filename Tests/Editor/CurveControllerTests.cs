using System;
using NUnit.Framework;

namespace Tactile.Animations.Tests.Editor
{
    public class CurveControllerTests
    {
        [Test]
        public void CannotSetValueLessThanZero()
        {
            var cc = new CurveController();
            cc.T = -1f;
            Assert.IsTrue(cc.T is >= 0 and <= 1);
        }
        
        [Test]
        public void CannotSetValueGreaterThanZero()
        {
            var cc = new CurveController();
            cc.T = 2f;
            Assert.IsTrue(cc.T is >= 0 and <= 1);
        }

        [Test]
        public void OnNewValueIsInvoked()
        {
            var wasCalled = false;
            var cc = new CurveController();
            cc.OnNewValue.AddListener(_ => wasCalled = true);
            cc.T = 0.5f;
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void CannotConstructWithNullCurve()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var cc = new CurveController(null);
            });
        }

        [Test]
        public void ValueMatchesCurveValue()
        {
            var cc = new CurveController();
            var iterations = 10;
            for (var i = 0; i < iterations; i++)
            {
                var t = (float)i / iterations;
                cc.T = t;
                Assert.AreEqual(cc.Curve.Evaluate(t), cc.Value);
            }
        }
    }
}