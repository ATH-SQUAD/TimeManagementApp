using System;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database;
using TimeManagementApp.Database.Data.EFCore;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Tests.Utility;

namespace TimeManagementApp.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void DailyTime_AddNewDailyTime()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var timeFrom = new DateTime(2021,12,31,8,0,0);
                var timeTo = new DateTime(2021,12,31,16,0,0);

                var dailyRepo = new EfCoreDailyRepo(db);
                var dailyTimeViewModel = new DailyTimeViewModel();
                var dailyTime = new DailyTime()
                {
                    Id = Guid.NewGuid(),
                    Person = "master",
                    Date = "2021-12-31",
                    Job = "Testing",
                    From = timeFrom.ToString(CultureInfo.InvariantCulture),
                    To = timeTo.ToString(CultureInfo.InvariantCulture),
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                    WorkTime = dailyTimeViewModel.CalculateWorkTime(timeFrom.TimeOfDay, timeTo.TimeOfDay).ToString(CultureInfo.InvariantCulture) + "h"
                };

                // Act
                dailyRepo.Add(dailyTime);
                
                // Assert
                Assert.AreEqual((timeTo.TimeOfDay.Hours - timeFrom.TimeOfDay.Hours) + "h", dailyRepo.Get(dailyTime.Id).Result.WorkTime);
                Assert.AreEqual(timeFrom.ToString(CultureInfo.InvariantCulture), dailyRepo.Get(dailyTime.Id).Result.From);
            }
        }
        
        [Test]
        public void VacationTime_AddNewVacationTime()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                DateTime dateFrom = new DateTime(2021,12,01);
                DateTime dateTo = new DateTime(2021,12,31);
                
                var vacationRepo = new EfCoreVacationRepo(db);
                var vacationTimeViewModel = new VacationTimeViewModel();
                var vacationTime = new VacationTime()
                {
                    Id = Guid.NewGuid(),
                    Person = "master",
                    DateFrom = dateFrom.ToString(CultureInfo.InvariantCulture),
                    DateTo = dateTo.ToString(CultureInfo.InvariantCulture),
                    Reason = "Testing vacation",
                    CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                    UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture),
                    TotalDays = vacationTimeViewModel.CalculateDays(dateFrom, dateTo).ToString(CultureInfo.InvariantCulture) + " dni"
                };

                // Act
                vacationRepo.Add(vacationTime);
                
                // Assert
                Assert.AreEqual((dateTo - dateFrom).TotalDays + " dni", vacationRepo.Get(vacationTime.Id).Result.TotalDays);
                Assert.AreEqual(vacationTime.Reason, vacationRepo.Get(vacationTime.Id).Result.Reason);
                Assert.IsNotNull(vacationRepo.Get(vacationTime.Id).Result.Person);
            }
        }
    }
}