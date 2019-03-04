using IRobotCreate.Tracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace IRobotTest
{
    
    
    /// <summary>
    ///This is a test class for TrackerMatrixTest and is intended
    ///to contain all TrackerMatrixTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TrackerMatrixTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for rotate
        ///</summary>
        [TestMethod()]
        public void rotateBoxTest()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float angle = 90F; // TODO: Initialize to an appropriate value
            float distance = 100F;
            target.goForward(distance);
            target.rotate(angle);
            Assert.AreEqual(target.getX(), 0F);
            Assert.AreEqual(target.getY(), 100F);
            target.goForward(distance);
            target.rotate(angle);
            Assert.IsTrue(withinTolerance(target.getX(), -100F, .001F));
            Assert.IsTrue(withinTolerance(target.getY(), 100F, .001F));
            target.goForward(distance);
            target.rotate(angle);
            Assert.IsTrue(withinTolerance(target.getX(), -100F, .001F));
            Assert.IsTrue(withinTolerance(target.getY(), 0F, .001F));
            target.goForward(distance);
            target.rotate(angle);
            Assert.IsTrue(withinTolerance(target.getX(), 0F, .001F));
            Assert.IsTrue(withinTolerance(target.getY(), 0F, .001F));
            Assert.AreEqual(0, target.getAngle());
        }
        private bool withinTolerance(float value, float target, float tolerance)
        {
            if(value < target + tolerance && value > target - tolerance)
                return true;
            return false;
        }

        [TestMethod()]
        public void rotatedBoxTest()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float angle = 90F; // TODO: Initialize to an appropriate value
            float distance = 100F;
            target.rotate(45F);
            target.goForward(distance);
            target.rotate(angle);
            target.goForward(distance);
            target.rotate(angle);
            Assert.IsTrue(withinTolerance(target.getX(),-(float)Math.Sqrt(Math.Pow(100, 2)*2), .001F));
            Assert.IsTrue(withinTolerance(target.getY(),0F, .001F));
            target.goForward(distance);
            target.rotate(angle);
            target.goForward(distance);
            target.rotate(angle);
            Assert.AreEqual(45, target.getAngle());
        }

        [TestMethod()]
        public void AngleTest()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float angle = 50F; // TODO: Initialize to an appropriate value
            target.rotate(angle);
            Assert.AreEqual(target.getAngle(), angle);
        }

        [TestMethod()]
        public void AngleTestAbove()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float angle = 370F; // TODO: Initialize to an appropriate value
            target.rotate(angle);
            Assert.AreEqual(target.getAngle(), 10F);
        }

        [TestMethod()]
        public void AngleTestBelow()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float angle = -10F; // TODO: Initialize to an appropriate value
            target.rotate(angle);
            Assert.AreEqual(target.getAngle(), 350F);
        }

        [TestMethod()]
        public void BackupTest()
        {
            TrackerMatrix target = new TrackerMatrix(); // TODO: Initialize to an appropriate value
            float distance = -50F; // TODO: Initialize to an appropriate value
            target.goForward(distance);
            Assert.AreEqual(target.getX(), 0F, .001);
            Assert.AreEqual(target.getY(), -50F, .001);
        }
    }
}
