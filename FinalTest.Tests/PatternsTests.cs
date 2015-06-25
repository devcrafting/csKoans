using System;
using NFluent;
using NUnit.Framework;

namespace FinalTest.Tests
{
    [TestFixture]
    public class PatternsTests
    {
        private readonly string _numéroDeCompte = Guid.NewGuid().ToString();

        //[Test]
        //public void OuvrirUnCompteBancaireProduitUnEvénement()
        //{
        //    var autorisationDeCrédit = 0;
        //    var evenements = CompteBancaire.Ouvrir(_numéroDeCompte, autorisationDeCrédit); // retourne un IEnumerable<IEvenementMetier> contenant l'événement CompteCréé

        //    Check.That(evenements).ContainsExactly(new CompteCréé(_numéroDeCompte, autorisationDeCrédit));
        //}

        //[Test]
        //public void EtantDonnéUnCompteBancaireFaireUnDepotProduitUnEvenement()
        //{
        //    var compteBancaire = new CompteBancaire(new CompteCréé(_numéroDeCompte, 0)); // Event Sourcing avec un seul événement
        //    var montantDepot = new Montant(10);
        //    var dateDepot = DateTime.Now;
        //    var evenements = compteBancaire.FaireUnDepot(montantDepot, dateDepot); // retourne un IEnumerable<IEvenementMetier> contenant l'événement DepotRealisé

        //    Check.That(evenements).ContainsExactly(new DépotRéalisé(_numéroDeCompte, montantDepot, dateDepot));
        //}

        //[Test]
        //public void EtantDonnéUnCompteBancaireFaireUnRetraitAvecProvisionSuffisanteProduitUnEvenement()
        //{
        //    var compteBancaire = new CompteBancaire(new CompteCréé(_numéroDeCompte, 0), new DépotRéalisé(_numéroDeCompte, new Montant(100), DateTime.Now)); // Event Sourcing avec une liste d'événements (params IEvénementMétier>[])
        //    var montantRetrait = new Montant(10);
        //    var dateRetrait = DateTime.Now;
        //    var evenements = compteBancaire.FaireUnRetrait(montantRetrait, dateRetrait); // retourne un IEnumerable<IEvénémentMétier> contenant l'événement RetraitRealisé

        //    Check.That(evenements).ContainsExactly(new RetraitRéalisé(_numéroDeCompte, montantRetrait, dateRetrait));
        //}

        //[Test]
        //public void EtantDonnéUnCompteBancaireNonApprovisionnéFaireUnRetraitSansDépasserSonAutorisationDeCreditSuffisanteProduitDeuxEvenements()
        //{
        //    var compteBancaire = new CompteBancaire(new CompteCréé(_numéroDeCompte, 10), new DépotRéalisé(_numéroDeCompte, new Montant(5), DateTime.Now));
        //    var montantRetrait = new Montant(10);
        //    var dateRetrait = DateTime.Now;
        //    var evenements = compteBancaire.FaireUnRetrait(montantRetrait, dateRetrait); // retourne un IEnumerable<IEvénémentMétier> contenant l'événement RetraitRealisé

        //    Check.That(evenements).ContainsExactly<IEvénementMétier>(new RetraitRéalisé(_numéroDeCompte, montantRetrait, dateRetrait), new BalanceNégativeDétectée(_numéroDeCompte, new Montant(5), dateRetrait));
        //}

        //[Test]
        //[ExpectedException(typeof(RetraitNonAutorisé))]
        //public void EtantDonnéUnCompteBancaireInitialiséViaEventSourcingFaireUnRetraitEnDehorsDeLAutorisationDeCreditLèveUneException()
        //{
        //    var compteBancaire = new CompteBancaire(new CompteCréé(_numéroDeCompte, 10), new DépotRéalisé(_numéroDeCompte, new Montant(5), DateTime.Now));
        //    var montantRetrait = new Montant(30);
        //    var dateRetrait = DateTime.Now;
        //    var evenements = compteBancaire.FaireUnRetrait(montantRetrait, dateRetrait);

        //    Check.That(evenements).IsEmpty();
        //}

        //[Test]
        //public void EtantDonnéLaSynthèseDuCompteQuandUnEvénementRetraitRéaliséLaSynthèseEstModifiée()
        //{
        //    var debits = 145;
        //    var credits = 120;
        //    var synthèseDuCompte = new SynthèseCompteBancaire(_numéroDeCompte, debits, credits); // utilisé une classe avec implémentation de Equals 
        //    var repository = new FakeRepository();
        //    repository.Synthèses.Add(synthèseDuCompte);

        //    var projection = new SynthèseCompteBancaireProjection(repository); // /!\ bien utilisé l'interface et non la classe Fake dans la signature du constructeur
        //    var retraitRéalisé = new RetraitRéalisé(_numéroDeCompte, new Montant(15), DateTime.Now);
        //    projection.Handle(retraitRéalisé);

        //    Check.That(repository.Synthèses).ContainsExactly(new SynthèseCompteBancaire(_numéroDeCompte, 160, credits));
        //}

        //public class FakeRepository : ISynthèseCompteBancaireRepository
        //{
        //    public List<SynthèseCompteBancaire> Synthèses = new List<SynthèseCompteBancaire>();

        //    public SynthèseCompteBancaire Get(string numeroDeCompte) // c'est la seule méthode à mettre dans l'interface
        //    {
        //        return Synthèses.First(x => x.NuméroDeCompte == numeroDeCompte);
        //    }
        //}
    }
}
