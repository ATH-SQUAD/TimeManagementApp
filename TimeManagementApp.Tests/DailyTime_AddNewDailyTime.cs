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
                //Assert.IsNotNull();
            }
        }
    }
}