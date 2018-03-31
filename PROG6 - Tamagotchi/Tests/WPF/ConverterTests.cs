using System;
using System.Collections.Generic;
using System.Globalization;
using WPF.TamagotchiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPF.Converter;

namespace Tests.WPF
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void TestStatusConverter()
        {
            var converter = new StatusConverter();

            Assert.AreEqual(string.Empty, converter.Convert(null, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture));

            Assert.AreEqual("Deceased", converter.Convert(new Tamagotchi {Deceased = true}, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture));

            Assert.AreEqual("Sleep\r\nHug", converter.Convert(new  Tamagotchi
            {
                Statuses = new List<string> {"Sleep", "Hug"}
            }, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture));

            Assert.AreEqual(string.Empty, converter.Convert(new Tamagotchi {Statuses = new List<string>()}, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestStatusConverterBackFails()
        {
            var converter = new StatusConverter();

            converter.ConvertBack(null, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void TestNeedsImageConverterBackFails()
        {
            var converter = new NeedsImageConverter();

            converter.ConvertBack(null, typeof(Tamagotchi), new object(), CultureInfo.CurrentCulture);
        }
    }
}
