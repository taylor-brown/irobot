using HW3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IRobotCreate;
using System.Drawing;

namespace IRobotTest
{
    
    
    /// <summary>
    ///This is a test class for BugTwoTest and is intended
    ///to contain all BugTwoTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BugTwoTest
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
        ///A test for traverseGoal
        ///</summary>
        //[TestMethod()]
        //public void traverseGoalTest()
        //{
        //    Robot robot = null; // TODO: Initialize to an appropriate value
        //    BugTwo target = new BugTwo(robot); // TODO: Initialize to an appropriate value
        //    int distance = 0; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.traverseGoal(distance);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for DriveMLine
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HW3.exe")]
        public void DriveMLineTest()
        {
            //PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            //BugTwo_Accessor target = new BugTwo_Accessor(param0); // TODO: Initialize to an appropriate value
            //Point goal = new Point(); // TODO: Initialize to an appropriate value
            //target.DriveMLine(goal);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for WaitWallFollow
        ///</summary>
        [TestMethod()]
        [DeploymentItem("HW3.exe")]
        public void WaitWallFollowTest()
        {
            //PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            //BugTwo_Accessor target = new BugTwo_Accessor(param0); // TODO: Initialize to an appropriate value
            //Point goal = new Point(); // TODO: Initialize to an appropriate value
            //target.WaitWallFollow(goal);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        [DeploymentItem("HW3.exe")]
        public void headingTest()
        {
            Moq.Mock<IRobot> mockRobot = new Moq.Mock<IRobot>();
            //Robot r = new Robot("COM19");
            mockRobot.Setup(s => s.Open()).Returns(true);
            BugTwo_Accessor target = new BugTwo_Accessor(mockRobot.Object);
            float actual = target.getGoalHeading(new Point(0, 50), new Point(0, 0));
            Assert.AreEqual(0, actual, 2);
            actual = target.getGoalHeading(new Point(-50, 0), new Point(0, 0));
            Assert.AreEqual(90, actual, 1);
            actual = target.getGoalHeading(new Point(0, -50), new Point(0, 0));
            Assert.AreEqual(180, actual, 1);
            actual = target.getGoalHeading(new Point(50, 0), new Point(0, 0));
            Assert.AreEqual(270, actual, 1);
        }
    }
}
