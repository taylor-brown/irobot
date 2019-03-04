using IRobotCreate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;
using System;
namespace IRobotTest
{
    
    
    /// <summary>
    ///This is a test class for RobotTest and is intended
    ///to contain all RobotTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RobotTest
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

        private bool withinTolerance(float value, float target)
        {
            float tolerance = .001F;
            if (value < target + tolerance && value > target - tolerance)
                return true;
            return false;
        }

        /// <summary>
        ///A test for TurnRight
        ///</summary>
        [TestMethod()]
        public void TurnRightTest()
        {
            //var mock = new Mock<IRobotInterface>();
            //Robot target = new Robot(mock.Object);
            Robot target = new Robot("Com19");
            target.Open();
            target.TurnRight();
            System.Threading.Thread.Sleep(1000);
            target.DriveStop();
            Assert.AreEqual(299F, target.getCurrentHeading(), 2);
            Assert.AreEqual(new Point(0, 0), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for TurnRight
        ///</summary>
        [TestMethod()]
        public void TurnRightCircleTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            //Robot target = new Robot("Com19");
            target.Open();
            target.TurnRight();
            System.Threading.Thread.Sleep(5463);
            target.DriveStop();
            Assert.AreEqual(0, target.getCurrentHeading(), 2);
            Assert.AreEqual(new Point(0, 0), target.getCurrentLocation());
        }
        /// <summary>
        ///A test for TurnLeft
        ///</summary>
        [TestMethod()]
        public void TurnLeftTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.TurnLeft();
            System.Threading.Thread.Sleep(1000);
            target.DriveStop();
            Assert.AreEqual(61F, target.getCurrentHeading(), 2);
            Assert.AreEqual(new Point(0, 0), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for DriveLeft
        ///</summary>
        [TestMethod()]
        public void DriveLeftTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveLeft();
            System.Threading.Thread.Sleep(1000);
            target.DriveStop();
            Assert.AreEqual(42F, target.getCurrentHeading(), 2);
            Assert.AreEqual(new Point(-102, 109), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for DriveRight
        ///</summary>
        [TestMethod()]
        public void DriveRightTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveRight();
            System.Threading.Thread.Sleep(1000);
            target.DriveStop();
            Assert.AreEqual(317F, target.getCurrentHeading(), 2);
            Assert.AreEqual(new Point(102, 109), target.getCurrentLocation());
        }
        /// <summary>
        ///A test for DriveRight
        ///</summary>
        [TestMethod()]
        public void DriveRightCircle()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveRight();
            System.Threading.Thread.Sleep((int)(Math.PI * 400/150));
            target.DriveStop();
            float actual = target.getCurrentHeading();
            if (actual < 360 && actual > 358)
            {
                actual = 0;
            }
            Assert.AreEqual(0F, actual, 2);
            Assert.AreEqual(new Point(0, 2), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for DriveRight
        ///</summary>
        [TestMethod()]
        public void DriveRightCircleDisplaced()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveStraight();
            System.Threading.Thread.Sleep(2000);
            target.DriveStop();
            target.DriveRight();
            System.Threading.Thread.Sleep((int)(Math.PI * 400 / 150));
            target.DriveStop();
            float actual = target.getCurrentHeading();
            if (actual < 360 && actual > 358)
            {
                actual = 0;
            }
            Assert.AreEqual(0F, actual, 2);
            Assert.AreEqual(new Point(0, 299), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for DriveBack
        ///</summary>
        [TestMethod()]
        public void DriveBackTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveBack();
            System.Threading.Thread.Sleep(1001);
            target.DriveStop();
            Assert.IsTrue(withinTolerance(0, target.getCurrentHeading())); 
            Assert.AreEqual(new Point(0, -50), target.getCurrentLocation());
        }

        /// <summary>
        ///A test for DriveStraight
        ///</summary>
        [TestMethod()]
        public void DriveStraightTest()
        {
            var mock = new Mock<IRobotInterface>();
            Robot target = new Robot(mock.Object);
            target.DriveStraight();
            System.Threading.Thread.Sleep(1001);
            target.DriveStop();
            Assert.IsTrue(withinTolerance(0, target.getCurrentHeading())); 
            Assert.AreEqual( new Point(0, 150),target.getCurrentLocation());
        }
    }
}
