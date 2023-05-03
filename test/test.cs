namespace TestJSON
{
    [TestClass]
    public class TestJSON
    {
        [TestMethod]
        public void onlyValue()
        {
            Assert.AreEqual(JSON.Parser.parse("45").get_number(), 45);
            Assert.AreEqual(JSON.Parser.parse("-4445").get_number(), -4445);

            Assert.AreEqual(JSON.Parser.parse("45.453").get_number(), 45.453);
            Assert.AreEqual(JSON.Parser.parse("-945.453").get_number(), -945.453);

            Assert.AreEqual(JSON.Parser.parse("true").get_bool(), true);
            Assert.AreEqual(JSON.Parser.parse("null").get(), true);
        }

        [TestMethod]
        public void onlyMultivariable()
        {
            Assert.AreEqual(JSON.Parser.parse("[ 34  ]").get(0).get_number(), 34);
            Assert.AreEqual(JSON.Parser.parse("[  34, { \"test\": \"ok\", \"num\": 98  }  ]").get(1).get("test").get_string(), "ok");
        }
    }
}
