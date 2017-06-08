using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCAssignment.Models; // added model to get db name

namespace MVCAssignment.Controllers
{
    public class PersonLiteController : Controller
    {
        Community_AssistEntities db = new Community_AssistEntities(); // creating db connection
        // GET: PersonLite
        public ActionResult Index()
        {
            var peeps = from p in db.People // creating query
                        from a in p.PersonAddresses
                        from c in p.Contacts
                        where c.ContactTypeKey == 1 // excludes anyone with home number

                        select new
                        {
                            p.PersonLastName,
                            p.PersonFirstName,
                            p.PersonEmail,
                            a.PersonAddressApt,
                            a.PersonAddressStreet,
                            a.PersonAddressCity,
                            a.PersonAddressState,
                            a.PersonAddressZip,
                            c.ContactNumber
                        };

            //need to loop through results and assign a class - use a list
            List<PersonLite> personList = new List<PersonLite>();

            foreach(var pers in peeps) // looping through list and creating new person
            {
                PersonLite pl = new Models.PersonLite(); //created new instance of PersonLite
                pl.LastName = pers.PersonLastName;
                pl.FirstName = pers.PersonFirstName;
                pl.Email = pers.PersonEmail;
                pl.Apartment = pers.PersonAddressApt;
                pl.Street = pers.PersonAddressStreet;
                pl.City = pers.PersonAddressCity;
                pl.State = pers.PersonAddressStreet;
                pl.Zipcode = pers.PersonAddressZip;
                pl.HomePhone = pers.ContactNumber;
                personList.Add(pl); // every person added to list

            }

            return View(personList); // return - passes list to view 
        }
    }
}