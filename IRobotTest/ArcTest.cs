using IRobotCreate.Tracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IRobotTest
{
    
    
    /// <summary>
    ///This is a test class for ArcTest and is intended
    ///to contain all ArcTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ArcTest
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
        ///A test for getYDistance
        ///</summary>
        [TestMethod()]
        public void getYDistanceTest()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 / 4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 10F;
            double actual;
            actual = target.getYDistance();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getXDistance
        ///</summary>
        [TestMethod()]
        public void getXDistanceTest()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 / 4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 10F;
            double actual;
            actual = target.getXDistance();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getDistanceTravelledZeroTest()
        {
            Arc target = new Arc();
            target.setVelocity((int) (Math.PI * 20));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0,0,1));
            double expected = 0F;
            double actual;
            actual = target.getStraightLineDistance();
            Assert.AreEqual(expected, actual, 1);
        }


        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getDistanceTravelled90Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20/4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = Math.Sqrt(Math.Pow(10, 2) * 2);
            double actual;
            actual = target.getStraightLineDistance();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getDistanceTravelled270Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 *3 / 4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = Math.Sqrt(Math.Pow(10, 2) * 2);
            double actual;
            actual = target.getStraightLineDistance();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getDistanceTravelled180Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 / 2));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = Math.Sqrt(Math.Pow(10, 2) * 2);
            double actual;
            actual = target.getStraightLineDistance();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getAngleZeroTest()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 200));
            target.setRadius(100);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 0F;
            double actual;
            actual = target.getArcAngleInDegrees();
            //wrap it around
            if (actual < 361 && actual > 358)
            {
                actual = 0F;
            }
            Assert.AreEqual(expected, actual, 1);
        }


        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getAngle90Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 / 4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 90F;
            double actual;
            actual = target.getArcAngleInDegrees();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getAngle270Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 * 3 / 4));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 270F;
            double actual;
            actual = target.getArcAngleInDegrees();
            Assert.AreEqual(expected, actual, 1);
        }

        /// <summary>
        ///A test for getDistanceTravelled
        ///</summary>
        [TestMethod()]
        public void getAngle180Test()
        {
            Arc target = new Arc();
            target.setVelocity((int)(Math.PI * 20 / 2));
            target.setRadius(10);
            target.setTimeElapsed(new TimeSpan(0, 0, 1));
            double expected = 180F;
            double actual;
            actual = target.getArcAngleInDegrees();
            Assert.AreEqual(expected, actual, 3);
        }
    }
}
