using GestionBanque.Models.DataService;
using GestionBanque.Models;
using System;
using System.Collections.Generic;
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
    public class ClientTest
    {
        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void SetNom_ShouldBeValid()
        {
            // Préparation
            Client client = new Client(1, "test123", "client", "client@test.com");
            string nomAttendu = "test124";
        
            //clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            //clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            //Client? clientActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(client.Nom, nomAttendu);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Retirer_ShouldNotBeValid()
        {

        }
    }
}
