using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektProgramowanieObiektowe;
using ProjektProgramowanieObjektowe;

namespace ProjektProgramowanieObiektowe.Tests
{
    [TestClass]
    public class ProjektProgramowanieObiektoweTests
    {
        [TestMethod]
        public void TestowaniePoprawnoscipodawanychdanych()
        {
            bool czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("przykładzik");


            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("0");
            Assert.IsTrue(czySpelnia);



            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("3414324,2");
            Assert.IsTrue(czySpelnia);


            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia(">>>>>>>>>>>>>>>>");
            Assert.IsTrue(czySpelnia);

            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("}}}}}}{{{{");
            Assert.IsTrue(czySpelnia);


            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("");
            Assert.IsTrue(czySpelnia);


            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia(string.Empty);
            Assert.IsTrue(czySpelnia);



            czySpelnia = MainWindow.CzyDaneSpelniajaZalozenia("373845039545");
            Assert.IsFalse(czySpelnia);


        }

        [TestMethod]
        public void PoprawnoscLiczby()
        {
            bool czySpelnia = MainWindow.sprawdzanieDanych("3");
            Assert.IsTrue(czySpelnia);

            czySpelnia = MainWindow.sprawdzanieDanych("3353");
            Assert.IsTrue(czySpelnia);


            czySpelnia = MainWindow.sprawdzanieDanych("3,45");
            Assert.IsFalse(czySpelnia);

            czySpelnia = MainWindow.sprawdzanieDanych("3.45");
            Assert.IsFalse(czySpelnia);



            czySpelnia = MainWindow.sprawdzanieDanych("ssfdsff");
            Assert.IsFalse(czySpelnia);



            czySpelnia = MainWindow.sprawdzanieDanych("*$(%");
            Assert.IsFalse(czySpelnia);

            czySpelnia = MainWindow.sprawdzanieDanych(string.Empty);
            Assert.IsFalse(czySpelnia);



        }
    }
}
