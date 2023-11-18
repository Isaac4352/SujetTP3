
using GestionBanque.Models.DataService;
using GestionBanque.Models;
using System.Diagnostics;
using GestionBanque.ViewModels.Interfaces;
using Moq;
using GestionBanque.ViewModels;

namespace GestionBanque.Tests
{
    // Ce décorateur s'assure que toutes les classes de tests ayant le tag "Dataservice" soit
    // exécutées séquentiellement. Par défaut, xUnit exécute les différentes suites de tests
    // en parallèle. Toutefois, si nous voulons forcer l'exécution séquentielle entre certaines
    // suites, nous pouvons utiliser un décorateur avec un nom unique. Pour les tests sur les DataService,
    // il est important que cela soit séquentiel afin d'éviter qu'un test d'une classe supprime la 
    // bd de tests pendant qu'un test d'une autre classe utilise la bd. Bref, c'est pour éviter un


    [Collection("Dataservice")]
    public class BanqueViewModelTest
    {
        // accès concurrent à la BD de tests!

        private readonly Mock<IInteractionUtilisateur> _interUtilMock = new Mock<IInteractionUtilisateur>();
        private readonly Mock<IDataService<Client>> _interDataClientMock = new Mock<IDataService<Client>>();
        private readonly Mock<IDataService<Compte>> _interDataCompteMock = new Mock<IDataService<Compte>>();


        private List<Client> ListeClientsAttendues()
        {
            return new List<Client>()
            {
                new Client(1, "TestNom", "TestPrenom", "TestPrenom@gmail.com"),
                new Client(2, "Salomon", "Isaac", "Isaac.Salomon@cshawi.ca"),
                new Client(3, "Troudeau", "Justun", "Justun154@gov.ca")
            };
        }



        private List<Compte> ListeComptesAttendues()
        {
            return new List<Compte>()
            {
                new Compte(1, "Compte1", 20.32, 1),
                new Compte(2, "Compte2", 154.22, 3),
                new Compte(3, "Compte3", 3.43, 2)
            };
        }

        private const string CheminBd = "test.bd";

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Constructor_ShouldBeValid()
        {
            // Préparation
            _interDataClientMock.Setup(DCM => DCM.GetAll()).Returns(ListeClientsAttendues());
            _interDataCompteMock.Setup(DCM => DCM.GetAll()).Returns(ListeComptesAttendues());
            

            // Exécution
            BanqueViewModel bvm = new BanqueViewModel(_interUtilMock.Object,_interDataClientMock.Object,_interDataCompteMock.Object);

            // Affirmation
            Assert.Equal(ListeClientsAttendues(), bvm.Clients);
            Assert.Empty(bvm.Nom);
            Assert.Empty(bvm.Prenom);
            Assert.Null(bvm.CompteSelectionne);
        }


        [Fact]
        [AvantApresDataService(CheminBd)]
        public void ClientSelectionne_ShouldBeValid() //a faire
        {
            // Préparation
            _interDataClientMock.Setup(DCM => DCM.GetAll()).Returns(ListeClientsAttendues());
            _interDataCompteMock.Setup(DCM => DCM.GetAll()).Returns(ListeComptesAttendues());

            BanqueViewModel bvm = new BanqueViewModel(_interUtilMock.Object, _interDataClientMock.Object, _interDataCompteMock.Object);

            // Exécution
            bvm.ClientSelectionne = ListeClientsAttendues()[0];

            // Affirmation
            Assert.Equal(bvm.ClientSelectionne, ListeClientsAttendues()[0]);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Modifier_ShouldBeValid()
        {
            // Préparation
            _interDataClientMock.Setup(DCM => DCM.GetAll()).Returns(ListeClientsAttendues());
            _interDataCompteMock.Setup(DCM => DCM.GetAll()).Returns(ListeComptesAttendues());

            BanqueViewModel bvm = new BanqueViewModel(_interUtilMock.Object, _interDataClientMock.Object, _interDataCompteMock.Object);
            bvm.ClientSelectionne = ListeClientsAttendues()[1];
            string NomAttendu = "NouveauNomTest";
            string PrenomAttendu = "PrenomAttendu";
            string CourrielAttendu = "NouveauCourriel@cshawi.ca";

            bvm.Nom = NomAttendu;
            bvm.Prenom = PrenomAttendu;
            bvm.Courriel = CourrielAttendu;

            // Exécution

            bvm.Modifier(bvm.ClientSelectionne);


            // Affirmation
            Assert.Equal(bvm.ClientSelectionne.Nom, NomAttendu);
            Assert.Equal(bvm.ClientSelectionne.Prenom, PrenomAttendu);
            Assert.Equal(bvm.ClientSelectionne.Courriel, CourrielAttendu);
        }


        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Retirer_ShoulBeValid() //a faire
        {
            // Préparation
            _interDataClientMock.Setup(DCM => DCM.GetAll()).Returns(ListeClientsAttendues());
            _interDataCompteMock.Setup(DCM => DCM.GetAll()).Returns(ListeComptesAttendues());

            BanqueViewModel bvm = new BanqueViewModel(_interUtilMock.Object, _interDataClientMock.Object, _interDataCompteMock.Object);

            BanqueViewModel bvm2 = new BanqueViewModel(_interUtilMock.Object, _interDataClientMock.Object, _interDataCompteMock.Object);
            bvm.CompteSelectionne = ListeComptesAttendues()[1];

            // Exécution

            bvm.CompteSelectionne = ListeComptesAttendues()[1];
            bvm.Retirer(ListeComptesAttendues()[1]);


            // Affirmation
            Assert.NotEqual(bvm.CompteSelectionne,bvm2.CompteSelectionne);
        }

        [Fact]
        [AvantApresDataService(CheminBd)]
        public void Deposer_ShoulBeValid() //a faire
        {
            // Préparation
            _interDataClientMock.Setup(DCM => DCM.GetAll()).Returns(ListeClientsAttendues());
            _interDataCompteMock.Setup(DCM => DCM.GetAll()).Returns(ListeComptesAttendues());

            BanqueViewModel bvm = new BanqueViewModel(_interUtilMock.Object, _interDataClientMock.Object, _interDataCompteMock.Object);
            bvm.CompteSelectionne = ListeComptesAttendues()[0];
            float montantAttendu = 30.32f;
            bvm.MontantTransaction = montantAttendu;

            // Exécution
            bvm.Deposer(montantAttendu);


            // Affirmation
            Assert.Equal(bvm.CompteSelectionne.Balance, ListeComptesAttendues()[0].Balance + montantAttendu);
        }
    }
}
