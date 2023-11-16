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
            var carCheckedInTimes = "2023-05-31 06:20,2023-05-31 06:45,2023-05-31 07:10";

            var amountToPay = SkattKalkylator.RäknaTotalBelopp(carCheckedInTimes);

            Assert.That(amountToPay, Is.EqualTo(18));
        }

    }
}