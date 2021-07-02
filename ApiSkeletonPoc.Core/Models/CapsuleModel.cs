using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSkeletonPoc.Core.Models
{
    public partial class Party
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("team")]
        public object Team { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("about")]
        public object About { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("organisation")]
        public object Organisation { get; set; }

        [JsonProperty("lastContactedAt")]
        public object LastContactedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("fields")]
        public Field[] Fields { get; set; }

        [JsonProperty("phoneNumbers")]
        public PhoneNumber[] PhoneNumbers { get; set; }

        [JsonProperty("addresses")]
        public Address[] Addresses { get; set; }

        [JsonProperty("websites")]
        public object[] Websites { get; set; }

        [JsonProperty("emailAddresses")]
        public EmailAddress[] EmailAddresses { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("definition")]
        public Definition Definition { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("tagId")]
        public string TagId { get; set; }
    }

    public partial class Definition
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dataTag")]
        public bool DataTag { get; set; }
    }
    public partial class Address
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
    public partial class PhoneNumber
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }
    public partial class EmailAddress
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }
}

