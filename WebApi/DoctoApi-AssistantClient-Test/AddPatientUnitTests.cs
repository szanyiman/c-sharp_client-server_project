namespace DoctorApiAssistantClientTest
{
    using System;
    using System.Threading;
    using DoctorApi_AssistantClient;
    using DoctorApiCommon.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddPatientUnitTests
    {
        [TestMethod]
        public void ValidatePatient_WithValidArguements_ComeBackWithTrue()
        {
            SemaphoreSlim ss = new SemaphoreSlim(1);

            Thread thread = new Thread(() =>
            {
                // Verify
                Assert.IsTrue(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA);

                // Arrange
                var patient = new Patient();

                // Act
                patient.Name = "Kis Mariska";
                patient.Address = "Pipacs utca 214.";
                patient.SocialSecurityNumber = "323 123 534";
                patient.Complaint = "Fájó fej";

                var currentPatient = new AddPatient(patient);
                bool isTrue = currentPatient.ValidatePatient();

                // Assert
                Assert.AreEqual(true, isTrue);

                ss.Release();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Console.WriteLine("All done!");
        }
    }
}