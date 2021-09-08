using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeManagementApp.App.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Tworzenie Raportów", "Tworzenie Raportów"),
            new Claim("Edycja Raportów","Edycja Raportów"),
            new Claim("Usuwanie Raportów","Usuwanie Raportów"),
            new Claim("Zarządzanie użytkownikami","Zarządzanie użytkownikami")
        };
    }
}
