using GestionBanque.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBanque.Tests
{
    // Ce décorateur s'assure que toutes les classes de tests ayant le tag "Dataservice" soit
    // exécutées séquentiellement. Par défaut, xUnit exécute les différentes suites de tests
    // en parallèle. Toutefois, si nous voulons forcer l'exécution séquentielle entre certaines
    // suites, nous pouvons utiliser un décorateur avec un nom unique. Pour les tests sur les DataService,
    // il est important que cela soit séquentiel afin d'éviter qu'un test d'une classe supprime la 
    // bd de tests pendant qu'un test d'une autre classe utilise la bd. Bref, c'est pour éviter un
    // accès concurrent à la BD de tests!
    [Collection("Dataservice")]
    public class CompteTest
    {
        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Retirer_ShouldBeValid()
        {
            // Préparation
            double montantDebut = 23.53;
            Compte compte = new Compte(1, "23", montantDebut, 1);
            double montantEnleve = 12.23;

            // Exécution
            compte.Retirer(montantEnleve);  

            // Affirmation
            Assert.Equal(compte.Balance, montantDebut - montantEnleve);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Retirer_ShouldNotBeValid()
        {
            // Préparation
            double montantDebut = 23.53;
            Compte compte = new Compte(1, "23", montantDebut, 1);
            double montantEnleve = 12.23;

            // Exécution
            compte.Retirer(montantEnleve);

            // Affirmation
            Assert.NotEqual(compte.Balance, montantDebut);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Deposer_ShouldBeValid()
        {
            // Préparation
            double montantDebut = 23.53;
            Compte compte = new Compte(1, "23", montantDebut, 1);
            double montantDepose = 12.23;
            Console.WriteLine(compte.Balance);
            Debug.WriteLine(compte.Balance);
            // Exécution
            compte.Deposer(montantDepose);

            // Affirmation
            Assert.Equal(compte.Balance, montantDebut + montantDepose);
        }

    
    }
}
