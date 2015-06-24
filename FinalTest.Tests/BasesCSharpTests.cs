using System;
using NFluent;
using NUnit.Framework;

namespace FinalTest.Tests
{
    [TestFixture]
    public class BasesCSharpTests
    {
        [Test]
        public void CeTestDoitPasserSiEnvironnementOK()
        {
            Check.That(true).IsTrue();
        }

        //[Test]
        //public void DéfinirUnTypeValeur()
        //{
        //    var valeur1 = new TypeValeur(12);
        //    var valeur2 = new TypeValeur(12);

        //    Check.That(valeur1).IsEqualTo(valeur2);
        //    Check.That(typeof(TypeValeur).IsValueType).IsTrue();
        //}

        //[Test]
        //public void DéfinirUnTypeRéférenceAvecEgalitéDeuxInstancesAyantLesMêmesPropriétés()
        //{
        //    var valeur1 = new TypeReference(12);
        //    var valeur2 = new TypeReference(12);

        //    Check.That(valeur1).IsEqualTo(valeur2);
        //    Check.That(typeof(TypeReference).IsValueType).IsTrue();
        //}

        //[Test]
        //public void DéfinirUneClasseRealisantUneMultiplication()
        //{
        //    var multiplication = new Multiplication();

        //    Check.That(multiplication.PeutCalculer("2*3")).IsTrue();
        //    Check.That(multiplication.PeutCalculer("2+3")).IsFalse();
        //    Check.That(multiplication.Calculer("2*3")).IsEqualTo(6);
        //}

        //[Test]
        //public void DéfinirUneClasseRealisantUneSomme()
        //{
        //    var somme = new Somme();

        //    Check.That(somme.PeutCalculer("2+3")).IsTrue();
        //    Check.That(somme.PeutCalculer("2*3")).IsFalse();
        //    Check.That(somme.Calculer("2+3")).IsEqualTo(5);
        //}

        //[Test]
        //public void DéfinirUneInterfaceStrategieAvec2ImplémentationsPrécédentesPasséesEnParamètreDUneClasseCliente()
        //{
        //    var multiplication = new Multiplication();
        //    var somme = new Somme();

        //    // La classe Calculatrice ne doit pas analyser l'opération reçue dans la méthode Calculer, elle doit s'appuyer sur les 2 implémentations passées en paramètre du constructeur
        //    var calculatrice = new Calculatrice(new IOperation[] { multiplication, somme });
        //    var resultatProduit = calculatrice.Calculer("1+2");
        //    var resultatSomme = calculatrice.Calculer("2*3");

        //    Check.That(resultatProduit).IsEqualTo(multiplication.Calculer());
        //    Check.That(resultatSomme).IsEqualTo(somme.Calculer());
        //}
    }
}
