﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Automile.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Automile.Net.Tests
{

    [TestClass]
    public class UnitTest1
    {
        private AutomileClient client;
        private int testVehicleId = 33553;
        private int testTripId = 31331100;
        private int testIMEIConfigId = 28288;
        private int testContactId = 2;
        private int testGeofenceId = 3276;
        private int testPlaceId = 2245;
        private int testCompanyId = 1;
        private int testTaskMessageId = 7194;
        private string dateperiod = "2014";
        private int vehicleId = 19;
        private int expenseReportId = 2077;


        [TestInitialize]
        public void Initialize()
        {
            //jens test pahome
            client = new AutomileClient(@"c:\temp\tokenavinash.json");

            // jens test paoffice
            //client = new AutomileClient(@"c:\temp\token_dev.json");

            // prod
            // client = new AutomileClient(@"c:\temp\token.json");



        }

        //[TestMethod]
        //public void TestSignup()
        //{
        //   var saveThisResponse = AutomileClient.SignUp("hello.developer5@automile.com");
        //   var myClient = new AutomileClient(saveThisResponse);
        //   Assert.IsNotNull(myClient);
        //}

        [TestMethod]
        public void TestGetVehicles()
        {
            IEnumerable<Vehicle2Model> vehicles = client.GetVehicles();
            Assert.IsNotNull(vehicles);

            Vehicle2DetailModel vehicle = client.GetVehicleById(vehicles.First().VehicleId);
            Assert.IsNotNull(vehicle);
        }

        [TestMethod]
        public void TestGetStatusForVehicles()
        {
            IEnumerable<VehicleStatusModel> status = client.GetStatusForVehicles();
            Assert.IsNotNull(status);
        }

        [TestMethod]
        public void TestGetTrips()
        {
            IEnumerable<TripModel> tripsLastDay = client.GetTrips(100);
            Assert.IsNotNull(tripsLastDay);
        }

        [TestMethod]
        public void TestGetTrip()
        {
            TripDetailModel trip = client.GetTripById(testTripId);
            Assert.IsNotNull(trip);
        }

        [TestMethod]
        public void TestGetTripStartStop()
        {
            TripStartEndGeoModel tripStartStop = client.GetTripStartStopLatitudeLongitude(testTripId);
            Assert.IsNotNull(tripStartStop);
        }

        [TestMethod]
        public void TestGetTripSpeed()
        {
            IEnumerable<VehicleSpeedModel> speed = client.GetTripSpeed(testTripId);
            Assert.IsNotNull(speed);
        }

        [TestMethod]
        public void TestGetTripRPM()
        {
            IEnumerable<RPMModel> rpm = client.GetTripRPM(testTripId);
            Assert.IsNotNull(rpm);
        }

        [TestMethod]
        public void TestGetTripAmbientTemperature()
        {
            IEnumerable<AmbientAirTemperatureModel> temp = client.GetTripAmbientTemperature(testTripId);
            Assert.IsNotNull(temp);
        }

        [TestMethod]
        public void TestGetFuel()
        {
            IEnumerable<FuelLevelInputModel> fuel = client.GetTripFuelLevel(testTripId);
            Assert.IsNotNull(fuel);
        }

        [TestMethod]
        public void TestGetEngineCoolantTemperature()
        {
            IEnumerable<EngineCoolantTemperatureModel> temp = client.GetTripEngineCoolantTemperature(testTripId);
            Assert.IsNotNull(temp);
        }

        [TestMethod]
        public void TestGetRawPIDs()
        {
            IEnumerable<PIDModel> pidData = client.GetTripPIDRaw(testTripId, 70);
            Assert.IsNotNull(pidData);
        }

        [TestMethod]
        public void TestGetTripLatitudeLongitude()
        {
            IEnumerable<TripGeoModel> geo = client.GeoTripLatitudeLongitude(testTripId, 1);
            Assert.IsNotNull(geo);
        }

        [TestMethod]
        public void TestGetTripDetails()
        {
            TripConcatenation tripCat = client.GetCompletedTripDetails(testTripId);
            Assert.IsNotNull(tripCat);
        }

        [TestMethod]
        public void TestGetTripDetailsAdvanced()
        {
            TripConcatenation tripCat = client.GetCompletedTripDetailsAdvanced(testTripId);
            Assert.IsNotNull(tripCat);
        }

        [TestMethod]
        public void TestGetContacts()
        {
            IEnumerable<Contact2Model> drivers = client.GetContacts();
            Assert.IsNotNull(drivers);

            Contact2DetailModel driver = client.GetContactById(drivers.First().ContactId);
            Assert.IsNotNull(driver);
        }

        [TestMethod]
        public void TestGetMe()
        {
            Contact2DetailModel driver = client.GetMe();
            Assert.IsNotNull(driver);
        }

        [TestMethod]
        public void TestEditTrip()
        {
            client.EditTrip(testTripId, new TripEditModel()
            {
                TripTags = new List<string> { "my notes" },
                TripType = ApiTripType.Business,
            });
        }

        [TestMethod]
        public void TestSetDriverOnTrip()
        {
            client.SetDriverOnTrip(testTripId, testContactId);
        }

        [TestMethod]
        public void TestGetGeofences()
        {
            IEnumerable<GeofenceModel> geofences = client.GetGeofences();
            Assert.IsNotNull(geofences);

            GeofenceModel geofence = client.GetGeofenceById(geofences.First().GeofenceId);
            Assert.IsNotNull(geofence);
        }

        [TestMethod]
        public void CreateGeofence()
        {
            GeofenceModel newGeofence = null;
            {
                var coordinates = new List<GeofencePolygon.GeographicPosition>();
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.44666232, Longitude = -122.16905397 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.4536707, Longitude = -122.16150999 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.44873066, Longitude = -122.15365648 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.4416096, Longitude = -122.16112375 });

                newGeofence = client.CreateGeofence(new GeofenceCreateModel()
                {
                    Name = "My Palo Alto geofence",
                    Description = "Outside main offfice",
                    VehicleId = 33553,
                    GeofencePolygon = new GeofencePolygon(coordinates),
                    GeofenceType = ApiGeofenceType.Outside,
                    Schedules = null // if you want to add a specific schedule
                });

                Assert.IsNotNull(newGeofence);
            }

            {
                var coordinates = new List<GeofencePolygon.GeographicPosition>();
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.44666232, Longitude = -122.16905397 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.4536707, Longitude = -122.16150999 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.44873066, Longitude = -122.15365648 });
                coordinates.Add(new GeofencePolygon.GeographicPosition() { Latitude = 37.4416096, Longitude = -122.16112375 });

                client.EditGeofence(newGeofence.GeofenceId, new GeofenceEditModel()
                {
                    Name = "My Palo Alto geofence",
                    Description = "Outside main offfice",
                    GeofencePolygon = new GeofencePolygon(coordinates),
                    GeofenceType = ApiGeofenceType.Outside,
                    Schedules = null // if you want to add a specific schedule
                });
            }

            client.DeleteGeofence(newGeofence.GeofenceId);
        }

        [TestMethod]
        public void TestGetNotifications()
        {
            IEnumerable<TriggerModel> notifications = client.GetNotifications();
            Assert.IsNotNull(notifications);

            var notification = client.GetNotificationById(notifications.First().TriggerId);
            Assert.IsNotNull(notification);
        }


        [TestMethod]
        public void TestCreateNotification()
        {
            var newNotification = client.CreateNotification(new TriggerCreateModel()
            {
                IMEIConfigId = testIMEIConfigId,
                TriggerType = ApiTriggerType.Accident,
                DestinationType = ApiDestinationType.Sms,
                DestinationData = "+14158320378"
            });

            Assert.AreEqual("+14158320378", newNotification.DestinationData);

            client.EditNotification(newNotification.TriggerId, new TriggerEditModel()
            {
                IMEIConfigId = testIMEIConfigId,
                TriggerType = ApiTriggerType.Accident,
                DestinationType = ApiDestinationType.Sms,
                DestinationData = "+14158320378"
            });

            client.MuteNotification(newNotification.TriggerId, 60 * 60);

            client.UnmuteNotification(newNotification.TriggerId);

            client.DeleteNotification(newNotification.TriggerId);
        }

        [TestMethod]
        public void TestGetPlaces()
        {
            IEnumerable<PlaceModel> places = client.GetPlaces();
            Assert.IsNotNull(places);

            PlaceModel place = client.GetPlaceById(places.First().PlaceId);
            Assert.IsNotNull(place);
        }

        [TestMethod]
        public void TestCreatePlace()
        {
            PlaceModel place = client.CreatePlace(new PlaceCreateModel()
            {
                Name = "My place",
                Description = "My home",
                PositionPoint = new PositionPointModel() { Latitude = 37.445368, Longitude = -122.166608 },
                Radius = 100,
                TripType = ApiTripType.Business,
                TripTypeTrigger = ApiTripTypeTrigger.Start,
                VehicleId = 33553
            });
            Assert.IsNotNull(place);

            client.EditPlace(place.PlaceId, new PlaceEditModel()
            {
                Name = "My place",
                Description = "My home",
                PositionPoint = new PositionPointModel() { Latitude = 37.445368, Longitude = -122.166608 },
                Radius = 100,
                TripType = ApiTripType.Business,
                TripTypeTrigger = ApiTripTypeTrigger.End
            });

            client.DeletePlace(place.PlaceId);
        }


        [TestMethod]
        public void TestGetDevices()
        {
            IEnumerable<IMEIConfigModel> devices = client.GetDevices();
            Assert.IsNotNull(devices);

            IMEIConfigDetailModel device = client.GetDeviceById(devices.First().IMEIConfigId);
            Assert.IsNotNull(device);
        }

        [TestMethod]
        public void TestCreateDevice()
        {
            IMEIConfigDetailModel newDevice = client.CreateDevice(new IMEIConfigCreateModel()
            {
                IMEI = "353466073376499",
                SerialNumber = "7011261106",
                VehicleId = 33553,
                IMEIDeviceType = null // no need if you register a box
            });
            Assert.IsNotNull(newDevice);

            client.EditDevice(newDevice.IMEIConfigId, new IMEIConfigEditModel()
            {
                VehicleId = 33553
            });

            client.DeleteDevice(newDevice.IMEIConfigId);
        }


        [TestMethod]
        public void TestGetFleets()
        {
            IEnumerable<CompanyModel> fleets = client.GetFleets();
            Assert.IsNotNull(fleets);

            CompanyDetailModel fleetDetail = client.GetFleetById(fleets.First().CompanyId);
            Assert.IsNotNull(fleetDetail);
        }

        [TestMethod]
        public void TestCreateFleet()
        {
            var newFleet = client.CreateFleet(new CompanyCreateModel()
            {
                CreateRelationshipToContactId = 2,
                Description = "Some good description for the fleet",
                RegisteredCompanyName = "My new fleet"
            });

            Assert.IsNotNull(newFleet);

            client.EditFleet(newFleet.CompanyId, new CompanyEditModel()
            {
                Description = "Test",
                RegisteredCompanyName = "Automile Palo Alto Fleet"
            });

            client.DeleteFleet(newFleet.CompanyId);
        }


        [TestMethod]
        public void TestGetNotificationMessages()
        {
            IEnumerable<TriggerMessageHistoryModel> messages = client.GetNotificationMessages();
            Assert.IsNotNull(messages);

            IEnumerable<TriggerMessageHistoryModel> messagesForNotification = client.GetNotificationMessagesByNotificationId(messages.First().TriggerId);
            Assert.IsNotNull(messagesForNotification);
        }


        [TestMethod]
        public void TestGetVehicleGeofences()
        {
            IEnumerable<VehicleGeofenceModel> vehicleGeofences = client.GetVehicleGeofencesByGeofenceId(testGeofenceId);
            Assert.IsNotNull(vehicleGeofences);

            VehicleGeofenceModel vehicleGeofence = client.GetVehicleGeofenceById(vehicleGeofences.First().VehicleGeofenceId);
            Assert.IsNotNull(vehicleGeofence);
        }

        [TestMethod]
        public void TestCreateVehicleGeofence()
        {
            VehicleGeofenceModel newVehicleGeofence = client.CreateVehicleGeofence(new VehicleGeofenceCreateModel()
            {
                GeofenceId = testGeofenceId,
                VehicleId = testVehicleId,
                ValidFrom = null,
                ValidTo = null
            });

            Assert.IsNotNull(newVehicleGeofence);

            client.EditVehicleGeofence(newVehicleGeofence.VehicleGeofenceId, new VehicleGeofenceEditModel()
            {
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow.AddDays(30)
            });

            client.DeleteVehicleGeofence(newVehicleGeofence.VehicleGeofenceId);
        }


        [TestMethod]
        public void TestGetVehiclePlaces()
        {
            IEnumerable<VehiclePlaceModel> vehiclePlaces = client.GetVehiclePlacesByPlaceId(testPlaceId);
            Assert.IsNotNull(vehiclePlaces);

            VehiclePlaceModel vehiclePlace = client.GetVehiclePlaceById(vehiclePlaces.First().VehiclePlaceId);
            Assert.IsNotNull(vehiclePlace);
        }

        [TestMethod]
        public void TestCreateVehiclePlace()
        {
            VehiclePlaceModel newVehiclePlace = client.CreateVehiclePlace(new VehiclePlaceCreateModel()
            {
                PlaceId = testPlaceId,
                VehicleId = testVehicleId,
                Description = "Some description",
                Radius = 100,
                TripType = ApiTripType.Business,
                TripTypeTrigger = ApiTripTypeTrigger.Start
            });

            Assert.IsNotNull(newVehiclePlace);

            client.EditVehiclePlace(newVehiclePlace.VehiclePlaceId, new VehiclePlaceEditModel()
            {
                Description = "Some description",
                Radius = 100,
                TripType = ApiTripType.Business,
                TripTypeTrigger = ApiTripTypeTrigger.Start
            });

            client.DeleteVehiclePlace(newVehiclePlace.VehiclePlaceId);
        }


        [TestMethod]
        public void TestGetDeviceEvents()
        {
            var events = client.GetDeviceEvents().ToList();
            Assert.IsNotNull(events);

            var first = events.First(i => i.EventType == "status");

            var ev = client.GetDeviceEventStatusById(first.IMEIEventId);
            Assert.IsNotNull(ev);
        }

        [TestMethod]
        public void TestGetFleetContacts()
        {
            IEnumerable<CompanyContactDetailModel> fleetContacts = client.GetFleetContacts();
            Assert.IsNotNull(fleetContacts);
            CompanyContactDetailModel details = client.GetFleetContactById(fleetContacts.First().CompanyContactId);
            Assert.IsNotNull(details);
            IEnumerable<CompanyContactDetailModel> fleetContactsForFleet = client.GetFleetContactsByFleetId(fleetContacts.First().CompanyId);
            Assert.IsNotNull(fleetContactsForFleet);
        }

        [TestMethod]
        public void TestCreateEditDeleteFleetContacts()
        {
            var newFleetContact = client.CreateFleetContact(new CompanyContactCreateModel()
            {
                CompanyId = testCompanyId,
                ContactId = 23
            });

            client.EditFleetContact(newFleetContact.CompanyContactId, new CompanyContactEditModel()
            {
                CompanyId = testCompanyId,
                ContactId = 23
            });

            client.DeleteFleetContact(newFleetContact.CompanyContactId);
        }

        [TestMethod]
        public void TestGetTaskMessage()
        {
            TaskMessageModel TaskMessage = client.GetByTaskMessageId(testTaskMessageId);
            Assert.IsNotNull(TaskMessage);
        }

        [TestMethod]
        public void TestCreateTaskMessage()
        {
            TaskMessageCreateModel newTaskMessage = client.CreateTaskMessage(new TaskMessageCreateModel()
            {
                TaskId = 1546,
                MessageText = "Hello World",
                Position = new PositionModel
                {
                    Latitude = 37.44,
                    Longitude = -122.143
                }
            });
            Assert.IsNotNull(newTaskMessage);
            TaskMessageModel TaskMessage = client.GetByTaskMessageId(testTaskMessageId);
            client.EditTaskMessage(TaskMessage.TaskMessageId, new TaskMessageEditModel()
            {
                IsRead = false
            });
        }

        [TestMethod]
        public void TestGetTripSummaryReport()
        {
            IEnumerable<TripSummaryReportModel> TripSummaryReport = client.GetTripSummaryReport(dateperiod);
            Assert.IsNotNull(TripSummaryReport);
        }
        [TestMethod]
        public void TestGetTripSummaryReportByVehicleId()
        {
            IEnumerable<TripSummaryReportModel> TripSummaryReportByVehicleId = client.GetTripSummaryReportByVehicleId(dateperiod, vehicleId);
            Assert.IsNotNull(TripSummaryReportByVehicleId);
        }

        [TestMethod]
        public void TestGetVehiclesSummaryReport()
        {
            VehiclesSummaryModel VehiclesSummary = client.GetVehiclesSummaryReport(dateperiod);
            Assert.IsNotNull(VehiclesSummary);
        }

        [TestMethod]
        public void TestGetVehicleSummaryReportByVehicleId()
        {
            VehicleSummaryModel VehicleSummary = client.GetVehicleSummaryReportByVehicleId(dateperiod, vehicleId);
            Assert.IsNotNull(VehicleSummary);
        }

        [TestMethod]
        public void TestEmailTripReport()
        {
           var StatusCode= client.EmailTripReport(new EmailTripReportModel()
            {
                VehicleId = 19,
                Period = 201401,
                ToEmail="avinash.oruganti@automile.com",
                ISO639LanguageCode="en",
                ExcludeDetailsForPersonalTrips=true,
                ExcludeEnvironmentalAndFuelData=true
            });

            Assert.AreEqual(System.Net.HttpStatusCode.OK, StatusCode);
          
        }
        [TestMethod]
        public void TestGetExpenseReport()
        {
            IEnumerable<ExpenseReportModel> ExpenseReports = client.GetExpenseReports();
            Assert.IsNotNull(ExpenseReports);

            ExpenseReportModel ExpenseReport = client.GetExpenseReportById(ExpenseReports.First().ExpenseReportId);
            Assert.IsNotNull(ExpenseReport);
        }

        [TestMethod]
        public void TestCreateDeleteExpenseReport()
        {
            ExpenseReportModel newExpenseReport = client.CreateExpenseReport(new ExpenseReportCreateModel()
            {
               ContactId=19971,
               VehicleId=40160,
               TripId= null,
               ExpenseReportDateUtc=DateTime.UtcNow.AddDays(-2),
               ExpenseReportRows=new List<ExpenseReportRowCreateModel>
               {
                  new ExpenseReportRowCreateModel { AmountInCurrency=20,
                      VATInCurrency =2,
                      ISO4217CurrencyCode ="USD",
                      ExpenseReportRowDateUtc =DateTime.UtcNow.AddDays(-2),
                      Category=ApiCategoryType.Fuel,
                      ExpenseReportRowContent=new List<ExpenseReportRowContentCreateModel>
                      {
                          new ExpenseReportRowContentCreateModel
                          {
                              ExpenseReportRowId=294,
                              Data="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",
                              ContentType=ApiContentType.Image
                          }
                      }



                  }
               }


            });

            Assert.IsNotNull(newExpenseReport);
            //var StatusCode = client.DeleteExpenseReport(newExpenseReport.ExpenseReportId);
            //Assert.AreEqual(System.Net.HttpStatusCode.OK, StatusCode);

            ExpenseReportModel newExpenseReportwImage = client.CreateExpenseReport(new ExpenseReportCreateModelwImage()
            {
                ContactId = 19971,
                VehicleId = 40160,
                TripId = null,
                ExpenseReportDateUtc = DateTime.UtcNow.AddDays(-2),
                ExpenseReportRows = new List<ExpenseReportRowCreateModelwImage>
               {
                  new ExpenseReportRowCreateModelwImage { AmountInCurrency=20,
                      VATInCurrency =2,
                      ISO4217CurrencyCode ="USD",
                      ExpenseReportRowDateUtc =DateTime.UtcNow.AddDays(-2),
                      Category=ApiCategoryType.Fuel,
                      ExpenseReportRowContent=new List<ExpenseReportRowContentCreateModelwImage>
                      {
                          new ExpenseReportRowContentCreateModelwImage
                          {
                              ExpenseReportRowId=294,
                              ImagePath=@"C:\Users\avoru\Pictures\Saved Pictures\920x920.jpg",
                              ContentType=ApiContentType.Image
                          }
                      }
                  }
               }


            });

            Assert.IsNotNull(newExpenseReportwImage);

        }

        [TestMethod]
        public void TestEmailExpenseReport()
        {
            var StatusCode = client.EmailExpenseReport(expenseReportId, new EmailExpenseReportModel()
            {
                ToEmail= "avinash.oruganti@automile.com",
                ISO639LanguageCode= "en"
            });

            Assert.AreEqual(System.Net.HttpStatusCode.OK, StatusCode);

            var StatusCode2 = client.EmailExpenseReports( new EmailExpenseReportsModel()
            {
                VehicleId=15195,
                FromDate= Convert.ToDateTime("2017-03-01T19:02:11.348Z"),
                ToDate= Convert.ToDateTime("2017-03-02T19:02:11.348Z"),
                ToEmail = "avinash.oruganti@automile.com",
                ISO639LanguageCode = "en"
            });

            Assert.AreEqual(System.Net.HttpStatusCode.OK, StatusCode2);
        }
    }
}
