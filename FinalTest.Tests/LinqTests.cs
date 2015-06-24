using System.Collections.Generic;
using NFluent;
using NUnit.Framework;

namespace FinalTest.Tests
{
    [TestFixture]
    public class LinqTests
    {
        private IEnumerable<KeyValuePair<string, int>> keyValuePairs;
        private IEnumerable<KeyValuePair<string, int>> otherKeyValuePairs;

        [SetUp]
        public void Setup()
        {
            keyValuePairs = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("un", 1),
                new KeyValuePair<string, int>("sept", 7),
                new KeyValuePair<string, int>("trois", 3),
                new KeyValuePair<string, int>("quatre", 4),
                new KeyValuePair<string, int>("deux", 2),
                new KeyValuePair<string, int>("cinq", 5),
                new KeyValuePair<string, int>("huit", 8),
                new KeyValuePair<string, int>("six", 6),
                new KeyValuePair<string, int>("neuf", 9)
            };
            otherKeyValuePairs = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("dix", 10),
                new KeyValuePair<string, int>("dix-sept", 17),
                new KeyValuePair<string, int>("soixante-quatorze", 74),
                new KeyValuePair<string, int>("vingt-huit", 28),
                new KeyValuePair<string, int>("cinquante et un", 51),
                new KeyValuePair<string, int>("quatre-vingt-neuf", 89),
                new KeyValuePair<string, int>("soixante-six", 66),
                new KeyValuePair<string, int>("vingt-trois", 23),
                new KeyValuePair<string, int>("quatre-vingt-douze", 92)
            };
        }

        //[Test]
        //public void RécupérerLesNombresPairs()
        //{
        //    var nombres = new Nombres(keyValuePairs);
        //    var nombresPairs = nombres.NombresPairs; // getter seulement qui retourne un IEnumerable<int> en utilisant Linq (i.e sans utiliser de boucles) -> Where + Select

        //    Check.That(nombresPairs).ContainsExactly(4, 2, 8, 6);
        //}

        //[Test]
        //public void RécupérerLesNombresPairsBis()
        //{
        //    var nombres = new Nombres(otherKeyValuePairs);
        //    var nombresPairs = nombres.NombresPairs; // même propriété

        //    Check.That(nombresPairs).ContainsExactly(10, 74, 28, 66, 92);
        //}

        //[Test]
        //public void RécupérerUneChaîneContenantLesNombresImpairsAuFormatTextuel()
        //{
        //    var nombres = new Nombres(keyValuePairs);
        //    var texteNombresImpairs = nombres.TexteNombresImpairs; // retourne une string en utilisant Linq (i.e sans utiliser de boucles) -> Aggregate + OrderBy

        //    Check.That(texteNombresImpairs).IsEqualTo("un, trois, cinq, sept, neuf");
        //}

        //[Test]
        //public void RécupérerUneChaîneContenantLesNombresImpairsAuFormatTextuelBis()
        //{
        //    var nombres = new Nombres(otherKeyValuePairs);
        //    var texteNombresImpairs = nombres.TexteNombresImpairs;  // meme propriété

        //    Check.That(texteNombresImpairs).IsEqualTo("dix-sept, vingt-trois, cinquante et un, quatre-vingt-neuf");
        //}

        //[Test]
        //public void RécupérerLePremierNombreDontLeTexteContientPlusDe5Caractères()
        //{
        //    var nombres = new Nombres(keyValuePairs);
        //    var premierNombreDontLeTexteContientPlusDe5Caractères = nombres.PremierNombreDontLeTexteContientPlusDe5Caractères; // retourne une string en utilisant Linq (i.e sans utiliser de boucles) -> First

        //    Check.That(premierNombreDontLeTexteContientPlusDe5Caractères).IsEqualTo("quatre");
        //}


        //[Test]
        //public void RécupérerLePremierNombreDontLeTexteContientPlusDe5CaractèresBis()
        //{
        //    var nombres = new Nombres(otherKeyValuePairs);
        //    var premierNombreDontLeTexteContientPlusDe5Caractères = nombres.PremierNombreDontLeTexteContientPlusDe5Caractères;  // meme propriété

        //    Check.That(premierNombreDontLeTexteContientPlusDe5Caractères).IsEqualTo("dix-sept");
        //}

        //[Test]
        //public void RécupérerLes4NombresSupérieursA3()
        //{
        //    var nombres = new Nombres(keyValuePairs);
        //    var quatreNombresSupérieursSuivant3 = nombres.QuatreNombresSupérieursSuivant3; // retourne un IEnumerable<int> en utilisant Linq (i.e sans utiliser de boucles) -> OrderBy + Skip + Take

        //    Check.That(quatreNombresSupérieursSuivant3).ContainsExactly(4, 5, 6, 7);
        //}

        //[Test]
        //public void RécupérerLes4NombresSupérieursA3Bis()
        //{
        //    var nombres = new Nombres(otherKeyValuePairs);
        //    var quatreNombresSupérieursSuivant3 = nombres.QuatreNombresSupérieursSuivant3; // meme propriété

        //    Check.That(quatreNombresSupérieursSuivant3).ContainsExactly(10, 17, 23, 28);
        //}
    }
}
