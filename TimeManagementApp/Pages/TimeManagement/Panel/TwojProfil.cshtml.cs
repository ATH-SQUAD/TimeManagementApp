using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TimeManagementApp.App.Auth;
using TimeManagementApp.App.ViewModels;
using TimeManagementApp.Database.Models;
using TimeManagementApp.Pages.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MailKit;
using MimeKit;
using System.Reflection;
using System.Collections;
using MailKit.Net.Smtp;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;

namespace TimeManagementApp.Pages.UserPanel
{
    [AllowAnonymous]
    public class TwojProfilModel : BasePageModel
    {
        public class UpdateUserViewModel
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        [Parameter]
        public UpdateUserViewModel UpdateModel { get; set; }
        public object Store { get; private set; }
        public PasswordValidator<ApplicationUser> PasswordValidator { get; }
        [TempData]
        public string StatusMessage { get; set; }


        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DbContext context;
        private readonly IWebHostEnvironment _env;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ILogger<ApplicationUser> _logger;

        public TwojProfilModel(
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment env,
            IPasswordHasher<ApplicationUser> passwordHasher,
            ILogger<ApplicationUser> logger) : base(signInManager)
        {
            _signInManager = signInManager;
            _emailSender = emailSender;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public ApplicationUser ApUser { get; set; }
    }
}