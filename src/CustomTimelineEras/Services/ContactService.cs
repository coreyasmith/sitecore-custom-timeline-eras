using System;
using System.Drawing.Imaging;
using System.IO;
using CustomTimelineEras.Properties;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using XConnectContact = Sitecore.XConnect.Contact;
using DataAccessConstants = Sitecore.Analytics.XConnect.DataAccess.Constants;

namespace CustomTimelineEras.Services
{
  public class ContactService
  {
    private readonly ContactManager _contactManager;

    public ContactService(ContactManager contactManager)
    {
      _contactManager = contactManager ?? throw new ArgumentNullException(nameof(contactManager));
    }

    public bool ContactIsIdentified()
    {
      return Tracker.Current.Contact.IdentificationLevel == ContactIdentificationLevel.Known;
    }

    public void IdentifyContact()
    {
      Tracker.Current.Session.IdentifyAs("custom-timeline-era", Constants.ContactEmail);
    }

    public void UpdateContactInformation()
    {
      var trackerContact = Tracker.Current.Contact;
      if (trackerContact.IsNew)
      {
        trackerContact.ContactSaveMode = ContactSaveMode.AlwaysSave;
        _contactManager.SaveContactToCollectionDb(trackerContact);
      }

      var contactExpandOptions = GetContactExpandOptions();
      var trackerIdentifier = new IdentifiedContactReference(DataAccessConstants.IdentifierSource, trackerContact.ContactId.ToString("N"));
      using (var client = SitecoreXConnectClientConfiguration.GetClient())
      {
        var xConnectContact = client.Get(trackerIdentifier, contactExpandOptions);
        if (xConnectContact == null) throw new InvalidOperationException($"Could not retrieve contact with identifier {trackerIdentifier.Identifier} from xConnect.");

        UpdateFacets(client, xConnectContact);
        client.Submit();
      }

      ReloadTrackerContact(trackerContact.ContactId);
    }

    private static ContactExpandOptions GetContactExpandOptions()
    {
      return new ContactExpandOptions(
        PersonalInformation.DefaultFacetKey,
        EmailAddressList.DefaultFacetKey,
        PhoneNumberList.DefaultFacetKey,
        AddressList.DefaultFacetKey,
        Avatar.DefaultFacetKey);
    }

    private static void UpdateFacets(IXdbContext client, XConnectContact contact)
    {
      UpdatePersonalInfo(client, contact);
      UpdateEmailAddress(client, contact);
      UpdatePhoneNumber(client, contact);
      UpdateAddress(client, contact);
      UpdatePicture(client, contact);
    }

    private static void UpdatePersonalInfo(IXdbContext client, XConnectContact contact)
    {
      var personalInfo = contact.Personal() ?? new PersonalInformation();
      personalInfo.Title = "Mr.";
      personalInfo.FirstName = "Bruce";
      personalInfo.LastName = "Wayne";
      personalInfo.JobTitle = "Chief Executive Officer";
      personalInfo.Birthdate = new DateTime(1939, 5, 27);
      personalInfo.Gender = "Male";
      client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfo);
    }

    private static void UpdateEmailAddress(IXdbContext client, XConnectContact contact)
    {
      var homeEmail = new EmailAddress(Constants.ContactEmail, true);
      var emailAddresses = contact.Emails() ?? new EmailAddressList(homeEmail, Facets.PreferredEmail);
      emailAddresses.PreferredEmail = homeEmail;
      client.SetFacet(contact, EmailAddressList.DefaultFacetKey, emailAddresses);
    }

    private static void UpdatePhoneNumber(IXdbContext client, XConnectContact contact)
    {
      var homePhone = new PhoneNumber("+1", "(555) 867-5309");
      var phoneNumbers = contact.PhoneNumbers() ?? new PhoneNumberList(homePhone, Facets.PreferredPhoneNumber);
      phoneNumbers.PreferredPhoneNumber = homePhone;
      client.SetFacet(contact, PhoneNumberList.DefaultFacetKey, phoneNumbers);
    }

    private static void UpdateAddress(IXdbContext client, XConnectContact contact)
    {
      var homeAddress = new Address
      {
        AddressLine1 = "1007 Mountain Drive",
        City = "Gotham City",
        StateOrProvince = "NJ",
        PostalCode = "53556",
        CountryCode = "US",
        GeoCoordinate = new GeoCoordinate(39.399544f, -74.886244f)
      };
      var addresses = contact.Addresses() ?? new AddressList(homeAddress, Facets.PreferredAddress);
      addresses.PreferredAddress = homeAddress;
      client.SetFacet(contact, AddressList.DefaultFacetKey, addresses);
    }

    private static void UpdatePicture(IXdbContext client, XConnectContact contact)
    {
      var picture = LoadPicture();
      var avatar = contact.Avatar() ?? new Avatar("image/jpeg", picture);
      avatar.Picture = picture;
      client.SetFacet(contact, Avatar.DefaultFacetKey, avatar);
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

    private void ReloadTrackerContact(Guid contactId)
    {
      _contactManager.RemoveFromSession(contactId);
      Tracker.Current.Session.Contact = _contactManager.LoadContact(contactId);
    }
  }
}
