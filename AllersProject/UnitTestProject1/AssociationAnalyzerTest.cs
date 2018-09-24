using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AssociationAnalyzer alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario1/"), 3, 0.4, 0.4, 2);
            Item[] items = new Item[3];
            items[0] = alv.data.mapFromItemCodeToItem["1"];
            items[1] = alv.data.mapFromItemCodeToItem["2"];
            items[2] = alv.data.mapFromItemCodeToItem["3"];
            Assert.IsNotNull(items[0]);
            System.Collections.Generic.List<Item[]> resultado = alv.GenerateFrequentItemSets(items);
            Item[] freq = new Item[3];
            freq[0] = new Item("1", "CONOS DE GUTAPERCHA 2ª SERIE 45 / 80 DENTSPLY");
            freq[1] = new Item("2", "PROTAMINA 1000 (5.000 UI/5 ml) Uso Institucional");
            //freq[2] = new Item("1", "CONOS DE GUTAPERCHA 2ª SERIE 45 / 80 DENTSPLY");
            string result = freq[0].ItemCode;
            string oRes = resultado[0][0].ItemCode;
            Assert.AreEqual(resultado[0],freq);
            
        }
    }
}
