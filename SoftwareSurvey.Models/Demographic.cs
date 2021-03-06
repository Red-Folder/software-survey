﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Demographic
    {
        [Required(ErrorMessage = "Please provide company size")]
        [DisplayName("Company Size")]
        [JsonProperty(PropertyName = "companySize")]
        public string CompanySize { get; set; }

        [Required(ErrorMessage = "Please provide your job seniority")]
        [DisplayName("Job Seniority")]
        [JsonProperty(PropertyName = "jobSeniority")]
        public string JobSeniority { get; set; }

        [DisplayName("Job Title (optional)")]
        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Please provide if your business is UK Based")]
        [DisplayName("Is your business UK based?")]
        [JsonProperty(PropertyName = "ukBased")]
        public string UKBased { get; set; }
    }
}
