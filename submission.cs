using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;

/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        // Urls for XML and XSD files hosted on Github 
        public static string xmlURL = "https://nik02033.github.io/cse445/Hotels.xml";
        public static string xmlErrorURL = "https://nik02033.github.io/cse445/HotelsErrors.xml";
        public static string xsdURL = "https://nik02033.github.io/cse445/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            // List to store errors
            List<string> validationErrors = new List<string>();
             // set up XML reader settings with XSD schema
            XmlReaderSettings xmlSettings = new XmlReaderSettings();
            xmlSettings.Schemas.Add(null, xsdUrl);
            xmlSettings.ValidationType = ValidationType.Schema;

            // Add validation errors to list
            xmlSettings.ValidationEventHandler += (object obj, ValidationEventArgs args) =>
            {
                validationErrors.Add(args.Message);
            };

            try
            {
                // Create XML Reader with settings
                using (XmlReader xmlReader = XmlReader.Create(xmlUrl, xmlSettings))
                {
                    while (xmlReader.Read())
                    {
                        // Reading XML for validation
                    }
                }
            }
            catch (Exception exception)
            {
                validationErrors.Add(exception.Message);
            }

            // Return result of "No error" if no error is found otherwise return the list of the errors.
            return validationErrors.Count == 0 ? "No Error" : string.Join(Environment.NewLine, validationErrors);


        }

        public static string Xml2Json(string xmlUrl)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            // Convert XML to JSON
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
