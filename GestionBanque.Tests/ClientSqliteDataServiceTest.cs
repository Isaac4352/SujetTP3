
using GestionBanque.Models.DataService;
using GestionBanque.Models;
using System.Diagnostics;

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
    public class ClientSqliteDataServiceTest
    {
        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            Client? clientActuel = ds.Get(1);

            // Affirmation
            Assert.Equal(clientAttendu, clientActuel);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Get_ShouldNotBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");
            clientAttendu.Comptes.Add(new Compte(1, "9864", 831.76, 1));
            //clientAttendu.Comptes.Add(new Compte(2, "2370", 493.04, 1));

            // Exécution
            Client? clientActuel = ds.Get(1);
            Console.WriteLine(clientActuel);

            // Affirmation
            Assert.NotEqual(clientAttendu, clientActuel);
        }


        [Fact]
        [AvantApresDataService(CheminBd)]
        public void GetAll_ShouldBeValid() //a faire
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = new Client(1, "Amar", "Quentin", "quentin@gmail.com");

            // Exécution
            IEnumerable<Client> clientsActuel = ds.GetAll();

            Debug.WriteLine(clientsActuel);  
            Console.WriteLine(clientsActuel);

            // Affirmation
            Assert.True(clientsActuel.Any());
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void RecuperComptes_ShouldNotBeValid()
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = ds.Get(1);

            // Exécution
            
            ds.RecupererComptes(clientAttendu);


            // Affirmation
            Assert.NotEmpty(clientAttendu.Comptes);
        }


        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Update_ShouldNotBeValid() //a faire
        {
            // Préparation
            ClientSqliteDataService ds = new ClientSqliteDataService(CheminBd);
            Client clientAttendu = ds.Get(1);

            // Exécution

            ds.RecupererComptes(clientAttendu);


            // Affirmation
            Assert.True(ds.Update(clientAttendu)) ;
        }
    }
}
