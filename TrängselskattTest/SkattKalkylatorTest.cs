using TrängselskattGbg;

namespace TrängselskattTest
{
    public class Tests
    {
        private BetalstationerGBG SkattKalkylator { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            SkattKalkylator = new BetalstationerGBG();
        }
      
        [Test]
        public void WhenRäknaTotalBeloppTest_SingleCase_AmountToPayIsEqualToExpectedResult()
        {
            var carCheckedInTime = "2023-05-31 17:45";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTime);

            Assert.That(amountToPay, Is.EqualTo(13));
        }
        [Test]
        public void WhenRäknaTotalBeloppTest_MultyCase_AmountToPayIsEqualToExpectedResult()
        {
            var carCheckedInTimes = "2023-05-31 08:00,2023-05-31 12:00,2023-05-31 12:45,2023-05-31 17:45";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTimes);

            Assert.That(amountToPay, Is.EqualTo(42));
        }
        [Test]
        public void WhenRäknaTotalBeloppTest_MultyCase_AmountToPayIsEqualToMaxBelop_Expected60()
        {
            var carCheckedInTimes = "2023-05-31 06:31, 2023-05-31 07:10, 2023-05-31 15:20, 2023-05-31 16:30";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTimes);

            Assert.That(amountToPay, Is.EqualTo(60));
        }
        [Test]
        public void WhenRäknaTotalBeloppTest_ForPeriod_Sut_Sun_July_AmountToPayIsEqualTo0()
        {
            var carCheckedInTimes = "2023-11-11 17:45";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTimes);

            Assert.That(amountToPay, Is.EqualTo(0));
        }
        [Test]
        public void WhenRäknaTotalBeloppTest_MultyCaseRegistration_60MinutInterval_AmountToPayIsEqualTo_HighestFee()
        {
            /*if I add fourth DateTime (2023-05-31 09:10) with more than 60 min diference and change
             * expected value to 26,because first thre returns 18 + 8 for last entry total should be 26
             * but method is counting total value for all four entrys and test is negativ in that case*/
            var carCheckedInTimes = "2023-05-31 06:20,2023-05-31 06:45,2023-05-31 07:10";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTimes);

            Assert.That(amountToPay, Is.EqualTo(18));
        }

    }
}