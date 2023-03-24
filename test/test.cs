namespace TestJSON
{
    [TestClass]
    public class TestJSON
    {
        [TestMethod]
        public void onlyValue()
        {
            Assert.AreEqual(JSON.Parser.parse("45").get().Long, 45);
            Assert.AreEqual(JSON.Parser.parse("-4445").get().Long, -4445);

            Assert.AreEqual(JSON.Parser.parse("45.453").get().Double, 45.453);
            Assert.AreEqual(JSON.Parser.parse("-945.453").get().Double, -945.453);

            Assert.AreEqual(JSON.Parser.parse("true").get().Bool, true);
            Assert.AreEqual(JSON.Parser.parse("null").get().Null, true);
        }

        [TestMethod]
        public void onlyMultivariable()
        {
            Assert.AreEqual(JSON.Parser.parse("[ 34  ]").get(0).get().Long, 34);
            Assert.AreEqual(JSON.Parser.parse("[  34, { \"test\": \"ok\", \"num\": 98  }  ]").get(1).get("test").get().String, "ok");
        }
    }
}
