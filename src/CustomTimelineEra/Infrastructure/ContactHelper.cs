using System;
using System.Drawing.Imaging;
using System.IO;
using CustomTimelineEra.Properties;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.Tracking;

namespace CustomTimelineEra.Infrastructure
{
  public static class ContactHelper
  {
    public static bool ContactIsIdentified()
    {
      return Tracker.Current.Contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.Known;
    }

    public static void IdentifyContact()
    {
      Tracker.Current.Session.Identify(Constants.ContactEmail);
    }

    public static void UpdateContactInformation()
    {
      var contact = Tracker.Current.Contact;
      UpdatePersonalInfo(contact);
      UpdateEmailAddress(contact);
      UpdatePhoneNumber(contact);
      UpdateAddress(contact);
      UpdatePicture(contact);
    }

    private static void UpdatePersonalInfo(Contact contact)
    {
      var personalInfo = contact.GetFacet<IContactPersonalInfo>(Facets.ContactPersonalInfo.Personal);
      personalInfo.Title = "Mr.";
      personalInfo.FirstName = "Bruce";
      personalInfo.Surname = "Wayne";
      personalInfo.JobTitle = "Chief Executive Officer";
      personalInfo.BirthDate = new DateTime(1939, 5, 27);
      personalInfo.Gender = "Male";
    }

    private static void UpdateEmailAddress(Contact contact)
    {
      var emailAddresses = contact.GetFacet<IContactEmailAddresses>(Facets.ContactEmailAddresses.Emails);
      var homeEmail = GetOrCreateDictionaryValue(emailAddresses.Entries, Facets.ContactEmailAddresses.Keys.Home);

      homeEmail.SmtpAddress = Constants.ContactEmail;

      emailAddresses.Preferred = Facets.ContactEmailAddresses.Keys.Home;
    }

    private static void UpdatePhoneNumber(Contact contact)
    {
      var phoneNumbers = contact.GetFacet<IContactPhoneNumbers>(Facets.ContactPhoneNumbers.PhoneNumbers);
      var homePhone = GetOrCreateDictionaryValue(phoneNumbers.Entries, Facets.ContactPhoneNumbers.Keys.Home);

      homePhone.CountryCode = "+1";
      homePhone.Number = "555-867-5309";

      phoneNumbers.Preferred = Facets.ContactPhoneNumbers.Keys.Home;
    }

    private static void UpdateAddress(Contact contact)
    {
      var addresses = contact.GetFacet<IContactAddresses>(Facets.ContactAddresses.Addresses);
      var homeAddress = GetOrCreateDictionaryValue(addresses.Entries, Facets.ContactAddresses.Keys.Home);

      homeAddress.StreetLine1 = "1007 Mountain Drive";
      homeAddress.City = "Gotham City";
      homeAddress.StateProvince = "NJ";
      homeAddress.PostalCode = "53556";
      homeAddress.Country = "United States";
      homeAddress.Location.Latitude = 39.399544f;
      homeAddress.Location.Longitude = -74.886244f;

      addresses.Preferred = Facets.ContactAddresses.Keys.Home;
    }

    private static T GetOrCreateDictionaryValue<T>(IElementDictionary<T> dictionary, string key)
      where T : class, IElement
    {
      var entry = dictionary.Contains(key) ? dictionary[key] : dictionary.Create(key);
      return entry;
    }

    private static void UpdatePicture(Contact contact)
    {
      var picture = contact.GetFacet<IContactPicture>(Facets.ContactPicture.Picture);
      picture.Picture = LoadPicture();
      picture.MimeType = "image/jpeg";
    }

    private static byte[] LoadPicture()
    {
      using (var memoryStream = new MemoryStream())
      {
        var image = Resources.BruceWayne;
        image.Save(memoryStream, ImageFormat.Jpeg);
        return memoryStream.ToArray();
      }
    }
  }
}
