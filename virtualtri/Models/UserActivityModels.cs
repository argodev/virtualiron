﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using virtualtri.Entities;

namespace virtualtri.Models
{
    public class MyActivitiesViewModel
    {
        public ApplicationUser Participant { get; set; }

        public List<Activity> Activities { get; set; }

        public Activity NewActivity { get; set; }

        public float TotalDistance { get; set; }

        public double PercentComplete { get; set; }

        //public string 

        public string GravatarLink
        { 
            get
            {
                if ((Participant != null) && (Participant.EmailAddress != null))
                {

                    var hash = GetMd5Hash(Participant.EmailAddress);
                    return string.Format("http://www.gravatar.com/avatar/{0}", hash);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes 
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data  
                // and format each one as a hexadecimal string. 
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string. 
                return sBuilder.ToString();
            }
        }
    }
}